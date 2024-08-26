using Service.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace Service.Models.Parameters
{
    public class ParameterViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Valor inicial (R$):")]
        [Required(ErrorMessage = "{0} deve ser preenchido")]
        public int? InitialValue { get; set; }

        [Display(Name = "Valor incremental (R$):")]
        [Required(ErrorMessage = "{0} deve ser preenchido")]
        public int? IncrementalValue { get; set; }

        [Display(Name = "Data de início:")]
        [Required(ErrorMessage = "{0} deve ser preenchido")]
        public DateOnly? StartDate { get; set; }

        [Display(Name = "Data de fim:")]
        [Required(ErrorMessage = "{0} deve ser preenchido")]
        [DateGreaterThan("StartDate")]
        public DateOnly? EndDate { get; set; }
    }
}
