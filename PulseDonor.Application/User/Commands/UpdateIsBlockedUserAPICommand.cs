﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.User.Commands
{
	public class UpdateIsBlockedUserAPICommand
	{
		public string UserId { get; set; }
		public bool IsBlocked { get; set; }
	}
}
