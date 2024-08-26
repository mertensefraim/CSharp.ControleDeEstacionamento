using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Service.Models.Parameters;
using Service.Services.Parameters;

namespace Application.Controllers
{
    public class ParameterController : Controller
    {
        private readonly IParameterService _parameterService;

        public ParameterController(IParameterService parameterService)
        {
            _parameterService = parameterService;
        }

        public IActionResult Index()
        {
            var parameters = _parameterService.GetAll();

            return View(parameters);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ParameterViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            if (_parameterService.VerifyDateRange(viewModel.StartDate, viewModel.EndDate, viewModel.Id) == true)
            {
                TempData["Error"] = "Já existe um parâmetro para esse período!";

                return View(viewModel);
            }

            var parameter = new Parameter
            {
                InitialValue = viewModel.InitialValue,
                IncrementalValue = viewModel.IncrementalValue,
                StartDate = viewModel.StartDate,
                EndDate = viewModel.EndDate
            };

            _parameterService.Create(parameter);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var parameter = _parameterService.GetById(id);

            var viewModel = new ParameterViewModel
            {
                InitialValue = parameter.InitialValue,
                IncrementalValue = parameter.IncrementalValue,
                StartDate = parameter.StartDate,
                EndDate = parameter.EndDate
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(ParameterViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            if (_parameterService.VerifyDateRange(viewModel.StartDate, viewModel.EndDate, viewModel.Id) == true)
            {
                TempData["Error"] = "Já existe um parâmetro para esse período!";

                return View(viewModel);
            }

            var parameter = new Parameter
            {
                Id = viewModel.Id,
                InitialValue = viewModel.InitialValue,
                IncrementalValue = viewModel.IncrementalValue,
                StartDate = viewModel.StartDate,
                EndDate = viewModel.EndDate
            };

            _parameterService.Update(parameter);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(ParameterViewModel viewModel)
        {
            _parameterService.Delete(viewModel.Id);

            return RedirectToAction("Index");
        }
    }
}
