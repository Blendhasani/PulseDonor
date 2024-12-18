using PulseDonor.MVC.Helper.Interfaces;
using System.Text.Json;
using System.Text;

namespace PulseDonor.MVC.Helper.Services
{
	public class ApiClientHelper : IApiClientHelper
	{
		private readonly HttpClient _httpClient;
		private readonly JsonSerializerOptions _jsonOptions;

		public ApiClientHelper(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_jsonOptions = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			};
		}

		public async Task<TResult> PostAsync<TRequest, TResult>(string url, TRequest request)
		{
			var content = SerializeContent(request);
			var response = await _httpClient.PostAsync(url, content);
			return await DeserializeResponse<TResult>(response);
		}

		public async Task<TResult> PutAsync<TRequest, TResult>(string url, TRequest request)
		{
			var content = SerializeContent(request);
			var response = await _httpClient.PutAsync(url, content);
			return await DeserializeResponse<TResult>(response);
		}

		public async Task<TResult> GetAsync<TResult>(string url)
		{
			var response = await _httpClient.GetAsync(url);
			return await DeserializeResponse<TResult>(response);
		}

		public async Task<TResult> DeleteAsync<TResult>(string url)
		{
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
			response.EnsureSuccessStatusCode(); // Throws if not successful
			var json = await response.Content.ReadAsStringAsync();
			return JsonSerializer.Deserialize<TResult>(json, _jsonOptions);
		}
	}
}
