using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using OpenAI_API.Completions;


namespace ChatGPT_CSharp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class OpenAIController : ControllerBase
	{
		private readonly IConfiguration _configuration;

		public OpenAIController(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		[HttpGet("use-chat")]
		public async Task<IActionResult> UseChatGPT(string query)
		{
			string outputResult = "";

			// Initialize OpenAI API with the API Key
			var openai = new OpenAIAPI(_configuration["OpenAI:APIKey"]);

			try
			{
				// Create a chat request for the chat completion endpoint
				var chatRequest = new OpenAI_API.Chat.ChatRequest
				{
					Model = "gpt-4-turbo", // Use the correct chat model
					Messages = new List<OpenAI_API.Chat.ChatMessage>
			{
				new OpenAI_API.Chat.ChatMessage
				{
					Role = OpenAI_API.Chat.ChatMessageRole.System, // Use ChatMessageRole enum
                    Content = "Ju jeni një asistent i dhurimit të gjakut. Përgjigjuni pyetjeve të përdoruesit në mënyrë të qartë dhe të sjellshme. Nëse përdoruesi pyet për përshtatshmërinë, kërkesat ose këshillat shëndetësore në lidhje me dhurimin e gjakut, jepni përgjigje të hollësishme dhe të sakta. Nëse pyetja nuk ka lidhje me dhurimin e gjakut, ridrejtojini me mirësjellje."
				},
				new OpenAI_API.Chat.ChatMessage
				{
					Role = OpenAI_API.Chat.ChatMessageRole.User, // Use ChatMessageRole enum
                    Content = query
				}
			},
					MaxTokens = 3600 // Adjust token limit based on your needs
				};

				// Send the request and get the response
				var chatResponse = await openai.Chat.CreateChatCompletionAsync(chatRequest);

				// Extract the response content
				foreach (var choice in chatResponse.Choices)
				{
					outputResult += choice.Message.Content;
				}
				var result = new
				{
					data = outputResult
				};
				return Ok(result);
			}
			catch (Exception ex)
			{
				// Handle exceptions gracefully
				return BadRequest($"Error: {ex.Message}");
			}
		}

		[HttpGet("weekly-quote")]
		public async Task<IActionResult> WeeklyQuote()
		{
			string outputResult = "";

			// Initialize OpenAI API with the API Key
			var openai = new OpenAIAPI(_configuration["OpenAI:APIKey"]);

			try
			{
				// Create a chat request for the chat completion endpoint
				var chatRequest = new OpenAI_API.Chat.ChatRequest
				{
					Model = "gpt-4-turbo", // Use the correct chat model
					Messages = new List<OpenAI_API.Chat.ChatMessage>
			{
				new OpenAI_API.Chat.ChatMessage
				{
					Role = OpenAI_API.Chat.ChatMessageRole.System, // Use ChatMessageRole enum
                    Content = "Ju jeni një asistent i dhurimit të gjakut."
				},
				new OpenAI_API.Chat.ChatMessage
				{
					Role = OpenAI_API.Chat.ChatMessageRole.User, // Use ChatMessageRole enum
                    Content = "Shkruani një kuotë te shkurter rreth dhurimit te gjakut apo shpetimit te jetes,ktheni rezultatin jo te futur ne thonjeza"
				}
			},
					MaxTokens = 3600 // Adjust token limit based on your needs
				};

				// Send the request and get the response
				var chatResponse = await openai.Chat.CreateChatCompletionAsync(chatRequest);

				// Extract the response content
				foreach (var choice in chatResponse.Choices)
				{
					outputResult += choice.Message.Content;
				}

				var result = new
				{
					data = outputResult
				};
				return Ok(result);
			}
			catch (Exception ex)
			{
				// Handle exceptions gracefully
				return BadRequest($"Error: {ex.Message}");
			}
		}






	}
}
