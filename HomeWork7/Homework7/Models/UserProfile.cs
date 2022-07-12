using System.ComponentModel.DataAnnotations;

namespace Homework7.Models;

public enum Sex : byte
{
    Male,
    Female
};

public class UserProfile : BaseModel
{
    [Required(ErrorMessage = "Fill this field!")]
    [MaxLength(30, ErrorMessage = "First Name must be less than 30 symbols")]
    [Display(Name = "Имя")]
    public override string FirstName { get; set; }
        
    [Required(ErrorMessage = "Fill this field!")]
    [MaxLength(30, ErrorMessage = "Last Name must be less than 30 symbols")]
    [Display(Name = "Фамилия")]
    public override string LastName { get; set; }
        
    [Required(ErrorMessage = "Fill this field!")]
    [MaxLength(30, ErrorMessage = "Middle Name must be less than 30 symbols")]
    [Display(Name = "Отчество")]
    public override string? MiddleName { get; set; }
    
    [Range(10, 100, ErrorMessage = "Your age must be in the range from 10 to 100")]
    [Display(Name = "Возраст")]
    public override int Age { get; set; }
        
    [Display(Name = "Пол")]
    public override Sex Sex { get; set; }
} 