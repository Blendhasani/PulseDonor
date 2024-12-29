using PulseDonor.Application.City.DTO;
using PulseDonor.Application.User.Commands;
using PulseDonor.Application.User.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.User.Interfaces
{
	public interface IUserAPIService
	{
		Task<List<UsersAPIDto>> GetUsersAsync();
		Task<string> AddUserAsync(AddUserAPICommand command);
	}
}
