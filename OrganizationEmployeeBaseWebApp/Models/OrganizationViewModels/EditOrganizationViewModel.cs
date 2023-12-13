using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace OrganizationEmployeeBaseWebApp.Models.OrganizationViewModels
{
    public class EditOrganizationViewModel
    {
        [Required]
        [UIHint("OrganizationId")]
        [Display(Name = "Id организации")]
        public int OrganizationId { get; set; }

        [Required(ErrorMessage = "Введите название")]
        [UIHint("Name")]
        [Display(Name = "Название организации")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Введите ИНН")]
        [UIHint("INN")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "ИНН должен состоять только из 10 цифр")]
        [Display(Name = "ИНН")]
        public string INN { get; set; } = null!;

        [Required(ErrorMessage = "Введите юр. адрес")]
        [UIHint("LegalAddress")]
        [Display(Name = "Юр. адрес")]
        public string LegalAddress { get; set; } = null!;

        [Required(ErrorMessage = "Введите факт. адрес")]
        [UIHint("ActualAddress")]
        [Display(Name = "Факт. адрес")]
        public string ActualAddress { get; set; } = null!;
    }
}
