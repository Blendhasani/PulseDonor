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
	}
}