using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Service.Models.Bookings;
using Service.Services.Bookings;

namespace Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookingService _bookingService;

        public HomeController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public IActionResult Index()
        {
            var bookings = _bookingService.GetAll();

            return View(bookings);
        }

        public IActionResult Parameters()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new BookingViewModelCreate
            {
                StartDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy HH:m"))
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(BookingViewModelCreate viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            if (_bookingService.GetByLicensePlateActive(viewModel.LicensePlate) != null)
            {
                TempData["Error"] = "Já existe um veículo com essa placa que está estacionado!";

                return View();
            }

            var booking = new Booking
            {
                LicensePlate = viewModel.LicensePlate,
                StartDate = viewModel.StartDate
            };

            _bookingService.Create(booking);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Finalize(string id)
        {
            _bookingService.Finalize(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Search(string licensePlate)
        {
            var bookings = _bookingService.GetByLicensePlate(licensePlate);

            ViewBag.LicensePlate = licensePlate;

            if (licensePlate == null)
                return RedirectToAction("Index");

            return View("Index", bookings);
        }
    }
}
