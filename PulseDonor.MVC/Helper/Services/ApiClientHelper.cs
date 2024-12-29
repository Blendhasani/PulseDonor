﻿using PulseDonor.MVC.Helper.Interfaces;
using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;

namespace PulseDonor.MVC.Helper.Services
{
	public class ApiClientHelper : IApiClientHelper
	{
		private readonly HttpClient _httpClient;
		private readonly JsonSerializerOptions _jsonOptions;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public ApiClientHelper(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
		{
			_httpClient = httpClient;
			_httpContextAccessor = httpContextAccessor;

			_jsonOptions = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			};
		}

		// Attach Token to Request
		private void AttachAuthorizationHeader()
		{
			var token = _httpContextAccessor.HttpContext?.Session.GetString("AuthToken");
			if (!string.IsNullOrEmpty(token))
			{
				token = token.Replace("Bearer ", "");
				_httpClient.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue("Bearer", token);
			}
		}



		public async Task<TResult> LoginAsync<TRequest, TResult>(string url, TRequest request)
		{
			AttachAuthorizationHeader();
			var content = SerializeContent(request);
			var response = await _httpClient.PostAsync(url, content);
			//var json = await response.Content.ReadAsStringAsync();
			//_httpContextAccessor.HttpContext?.Session.SetString("AuthToken", json);

			response.EnsureSuccessStatusCode(); // Ensure the response is successful
			var json = await response.Content.ReadAsStringAsync();

			// Check if TResult is string (raw token)
			if (typeof(TResult) == typeof(string))
			{
				// Save token in session
				_httpContextAccessor.HttpContext?.Session.SetString("AuthToken", json);
				return (TResult)(object)json;
			}

			// Otherwise, deserialize as normal
			return JsonSerializer.Deserialize<TResult>(json, _jsonOptions);

		}
		public async Task<TResult> PostAsync<TRequest, TResult>(string url, TRequest request)
		{
			AttachAuthorizationHeader();
			var content = SerializeContent(request);
			var response = await _httpClient.PostAsync(url, content);
			return await DeserializeResponse<TResult>(response);
		}

		public async Task<TResult> PutAsync<TRequest, TResult>(string url, TRequest request)
		{
			AttachAuthorizationHeader();
			var content = SerializeContent(request);
			var response = await _httpClient.PutAsync(url, content);
			return await DeserializeResponse<TResult>(response);
		}

		public async Task<TResult> GetAsync<TResult>(string url)
		{
			AttachAuthorizationHeader();
			var response = await _httpClient.GetAsync(url);
			return await DeserializeResponse<TResult>(response);
		}

		public async Task<TResult> GetByIdAsync<TResult>(string url, int id)
		{
			AttachAuthorizationHeader();
			var fullUrl = $"{url}?id={id}";
			var response = await _httpClient.GetAsync(fullUrl);
			return await DeserializeResponse<TResult>(response);
		}


		public async Task<TResult> DeleteAsync<TResult>(string url)
		{
			AttachAuthorizationHeader();
			var response = await _httpClient.DeleteAsync(url);
			return await DeserializeResponse<TResult>(response);
		}

		private StringContent SerializeContent<TRequest>(TRequest request)
		{
			var json = JsonSerializer.Serialize(request, _jsonOptions);
			return new StringContent(json, Encoding.UTF8, "application/json");
		}

		private async Task<TResult> DeserializeResponse<TResult>(HttpResponseMessage response)
		{
			var json = await response.Content.ReadAsStringAsync();

			if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
			{
				Console.WriteLine($"Unauthorized Access: {json}");
				throw new UnauthorizedAccessException("Unauthorized: Invalid or expired token.");
			}

			if (!response.IsSuccessStatusCode)
			{
				Console.WriteLine($"Request failed with status code {response.StatusCode}: {json}");
				throw new HttpRequestException($"Request failed with status code {response.StatusCode}: {json}");
			}

			try
			{
				return JsonSerializer.Deserialize<TResult>(json, _jsonOptions)
					   ?? throw new JsonException("Failed to deserialize response.");
			}
			catch (JsonException ex)
			{
				Console.WriteLine($"Deserialization Error: {ex.Message}");
				throw;
			}
		}



	}
}
