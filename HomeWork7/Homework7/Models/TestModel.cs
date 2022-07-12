using System.ComponentModel.DataAnnotations;

namespace Homework7.Models;

public class TestModel : BaseModel
{
    //without [Display(Name = "Имя")]
    [Required(ErrorMessage = "Fill this field!")]
    [MaxLength(30, ErrorMessage = "First Name must be less than 30 symbols")]
    public override string FirstName { get; set; }
    
    //without [Required(ErrorMessage = "Fill this field!")]
    [MaxLength(30, ErrorMessage = "Last Name must be less than 30 symbols")]
    [Display(Name = "Фамилия")]
    public override string LastName { get; set; }
        
    //without [MaxLength(30, ErrorMessage = "Middle Name must be less than 30 symbols")]
    [Required(ErrorMessage = "Fill this field!")]
    [Display(Name = "Отчество")]
    public override string? MiddleName { get; set; }
    
    //without [Display(Name = "Возраст")]
    [Range(10, 100, ErrorMessage = "Your age must be in the range from 10 to 100")]
    public override int Age { get; set; }
    
    [Display(Name = "Пол")]
    public override Sex Sex { get; set; }
}