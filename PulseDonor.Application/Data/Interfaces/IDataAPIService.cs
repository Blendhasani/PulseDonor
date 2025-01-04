using Microsoft.AspNetCore.Mvc.Rendering;
using PulseDonor.Application.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.Data.Interfaces
{
	public interface IDataAPIService
	{
		Task<List<DropdownDataDto>> GetBloodTypes();
		Task<List<DropdownDataDto>> GetUsers();
		Task<List<DropdownDataDto>> GetCities();

	}
}
