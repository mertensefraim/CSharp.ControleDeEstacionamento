using System.ComponentModel.DataAnnotations;

namespace Service.Models.Bookings
{
    public class BookingViewModelFinalize
    {
        [Display(Name = "Placa do veículo:")]
        public string LicensePlate { get; set; }
    }

}
