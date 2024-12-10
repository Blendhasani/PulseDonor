using Microsoft.AspNetCore.Mvc;

namespace PulseDonor.MVC.Controllers
{
	public class UserController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult GetTableData()
		{
			var data = new List<object>
	{
		new { OrderID = "0006-3629", CarMake = "Land Rover", CarModel = "Range Rover", Color = "Orange", DepositPaid = "22672.60", OrderDate = "2016-11-28" },
		new { OrderID = "66403-315", CarMake = "GMC", CarModel = "Jimmy", Color = "Goldenrod", DepositPaid = "55141.29", OrderDate = "2017-04-29" },
		new { OrderID = "54868-5055", CarMake = "Ford", CarModel = "Club Wagon", Color = "Goldenrod", DepositPaid = "70991.52", OrderDate = "2017-03-16" }
	};

			// Return the data as JSON as-is won't work with KTDatatable:
			// return Json(data);

			// Instead, wrap it in the structure expected by KTDatatable.
			var result = new
			{
				meta = new
				{
					page = 1, // current page
					pages = 1, // total number of pages
					perpage = 10, // number of records per page
					total = data.Count, // total number of records
				},
				data = data
			};

			return Json(result);
		}

	}
}
