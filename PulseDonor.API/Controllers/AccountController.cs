using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PulseDonor.Application.Account.Commands;
using PulseDonor.Application.Account.Interfaces;

namespace PulseDonor.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]

	public class AccountController : ControllerBase
	{
		private readonly IAccountService _accountService;
		public AccountController(IAccountService accountService)
		{
			_accountService = accountService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAccountOverview()
		{
			var accountOverview = await _accountService.GetAccountOverviewAsync();
			var result = new
			{
				data = accountOverview
			};
			return Ok(result);
		}

		[HttpPut]
		public async Task<IActionResult> EditAccountOverview(EditAccountOverviewCommand cmd)
		{
			var updatedAccountOverview = await _accountService.EditAccountOverviewAsync(cmd);
			var result = new
			{
				data = updatedAccountOverview
			};
			return Ok(result);
		}

		//posts crud

		[HttpGet("user-profile-blood-requests")]
		public async Task<IActionResult> GetMyBloodRequests()
		{
			var myBloodRequest = await _accountService.GetMyBloodRequestsAsync();
			var result = new
			{
				data = myBloodRequest
			};
			return Ok(result);
		}

		[HttpPost("blood-request")]
		public async Task<IActionResult> CreateBloodRequestPost(AddBloodRequestCommand cmd)
		{
			var createdBloodRequest = await _accountService.CreateBloodRequestPostAsync(cmd);
			var result = new
			{
				data = createdBloodRequest
			};
			return Ok(result);
		}

		[HttpGet("blood-request-by-id")]
		public async Task<IActionResult> GetBloodRequestById(int id)
		{
			var bloodRequest = await _accountService.GetBloodRequestByIdAsync(id);
			var result = new
			{
				data = bloodRequest
			};
			return Ok(result);
		}

		[HttpPut("blood-request/{id}")]
		public async Task<IActionResult> EditBloodRequestPost([FromRoute] int id, [FromBody] EditAccountBloodRequestCommand cmd)
		{
			var editedBloodRequest = await _accountService.EditBloodRequestPostAsync(id, cmd);
			var result = new
			{
				data = editedBloodRequest
			};
			return Ok(result);
		}

		[HttpGet("{id}/blood-request-applications")]
		public async Task<IActionResult> GetBloodRequestApplications(int id)
		{
			var bloodRequestApplications = await _accountService.GetBloodRequestApplicationsAsync(id);
			var result = new
			{
				data = bloodRequestApplications
			};
			return Ok(result);
		}


		[HttpPut("confirm-application/{id}")]
		public async Task<IActionResult> ConfirmBloodRequestApplication(int id)
		{
			var confirmedBloodRequest = await _accountService.ConfirmBloodRequestApplicationAsync(id);
			var result = new
			{
				data = confirmedBloodRequest
			};
			return Ok(result);
		}
		[HttpDelete("blood-request/{id}")]
		public async Task<IActionResult> DeleteBloodRequestPost([FromRoute] int id)
		{
			var deletedBloodRequest = await _accountService.DeleteBloodRequestPostAsync(id);
			var result = new
			{
				data = deletedBloodRequest
			};
			return Ok(result);
		}

		[HttpGet("user-profile-applications")]
		public async Task<IActionResult> GetMyApplications()
		{
			var myApplications = await _accountService.GetMyApplicationsAsync();
			var result = new
			{
				data = myApplications
			};
			return Ok(result);
		}

		[HttpGet("application/{id}")]
		public async Task<IActionResult> GetApplicationById(int id)
		{
			var application = await _accountService.GetApplicationByIdAsync(id);
			var result = new
			{
				data = application
			};
			return Ok(result);
		}

		[HttpDelete("application/{id}")]
		public async Task<IActionResult> DeleteApplication(int id)
		{
			var deletedApplication = await _accountService.DeleteApplicationAsync(id);
			var result = new
			{
				data = deletedApplication
			};
			return Ok(result);
		}

	}
}
