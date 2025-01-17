using Microsoft.AspNetCore.Mvc;
using PulseDonor.MVC.BloodDonationPoint.Commands;
using PulseDonor.MVC.BloodDonationPointsController.Interfaces;
using PulseDonor.MVC.BloodRequest.Command;
using PulseDonor.MVC.BloodRequest.Interfaces;

namespace PulseDonor.MVC.Controllers
{
    public class BloodRequestsController : Controller
    {
        private readonly IBloodRequestService _service;

        public BloodRequestsController(IBloodRequestService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Json(result);
        }

        public IActionResult Add()
        {
            return PartialView("_Add");
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBloodeRequestAPICommand model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _service.AddAsync(model);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return PartialView("_Edit", result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBloodRequestAPICommand model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _service.EditAsync(model);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return RedirectToAction("Index");

            var result = await _service.DeleteAsync(id);

            return RedirectToAction("Index");

        }

    }
}
