﻿using Microsoft.AspNetCore.Authorization;
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
			var result = await _accountService.GetAccountOverviewAsync();
			return Ok(result);
		}

		[HttpPut]
		public async Task<IActionResult> EditAccountOverview(EditAccountOverviewCommand cmd)
		{
			var result = await _accountService.EditAccountOverviewAsync(cmd);
			return Ok(result);
		}

		//posts crud

		[HttpGet("user-profile-blood-requests")]
		public async Task<IActionResult> GetMyBloodRequests()
		{
			var result = await _accountService.GetMyBloodRequestsAsync();
			return Ok(result);
		}

		[HttpPost("blood-request")]
		public async Task<IActionResult> CreateBloodRequestPost(AddBloodRequestCommand cmd)
		{
			var result = await _accountService.CreateBloodRequestPostAsync(cmd);
			return Ok(result);
		}

		[HttpGet("blood-request-by-id")]
		public async Task<IActionResult> GetBloodRequestById(int id)
		{
			var result = await _accountService.GetBloodRequestByIdAsync(id);
			return Ok(result);
		}

		[HttpPut("blood-request/{id}")]
		public async Task<IActionResult> EditBloodRequestPost([FromRoute] int id, [FromBody] EditBloodRequestCommand cmd)
		{
			var result = await _accountService.EditBloodRequestPostAsync(id, cmd);
			return Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteBloodRequestPost([FromRoute] int id)
		{
			var result = await _accountService.DeleteBloodRequestPostAsync(id);
			return Ok(result);
		}


		//lista dhurimeve
	}
}
