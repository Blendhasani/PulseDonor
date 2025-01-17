using Microsoft.AspNetCore.Mvc;
using PulseDonor.MVC.Testimonials.Command;
using PulseDonor.MVC.Testimonials.Interfaces;

namespace PulseDonor.MVC.Controllers
{
    public class TestimonialsController : Controller
    {
        private readonly ITestimonialsService _service;

        public TestimonialsController(ITestimonialsService service)
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
        public async Task<IActionResult> Add(AddTestimonialAPICommand model)
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
        public async Task<IActionResult> Edit(EditTestimonialAPICommand model)
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
