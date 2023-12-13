using OrganizationEmployeeBaseWebApp.DataLayer.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace OrganizationEmployeeBaseWebApp.Models.EmployeeViewModels
{
    public class CreateEmployeeViewModel
    {
        [Required(ErrorMessage = "Введите имя")]
        [UIHint("FirstName")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Введите фамилию")]
        [UIHint("LastName")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Введите отчество")]
        [UIHint("Patronymic")]
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; } = null!;

        [Required(ErrorMessage = "Введите дату рождения")]
        [DataType(DataType.Date, ErrorMessage = "Введите корректную дату рождения")]
        [UIHint("DateOfBirth")]
        [Display(Name = "Дата рождения")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Введите серию паспорта")]
        [UIHint("PassportSeries")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Серия паспорта должна состоять из 8 цифр")]
        [Display(Name = "Серия паспорта")]
        public string PassportSeries { get; set; } = null!;

        [Required(ErrorMessage = "Введите номер паспорта")]
        [UIHint("PassportNumber")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Номер паспорта должен состоять из 6 цифр")]
        [Display(Name = "Номер паспорта")]
        public string PassportNumber { get; set; } = null!;

        [Required]
        [UIHint("OrganizationId")]
        [Display(Name = "Id организации")]
        public int OrganizationId { get; set; }
    }
}
