using System.ComponentModel.DataAnnotations;

namespace Service.Models.Bookings
{
    public class BookingViewModelCreate
    {
        [Display(Name = "Placa do veículo:")]
        [Required(ErrorMessage = "{0} deve ser preenchido")]
        [RegularExpression(@"^[a-zA-Z]{3}[0-9][A-Za-z0-9][0-9]{2}$", ErrorMessage = "Formato de placa não permitido (XXX9999 ou XXX9X99)!")]
        public string LicensePlate { get; set; }

        [Display(Name = "Data de entrada:")]
        [Required(ErrorMessage = "{0} deve ser preenchido")]
        public DateTime StartDate { get; set; }
    }
}
