using System.ComponentModel.DataAnnotations;

namespace Homework7.Models;

public class BaseModel
{
    public virtual string FirstName { get; set; }
    
    public virtual string LastName { get; set; }
    
    public virtual string? MiddleName { get; set; }
    
    public virtual int Age { get; set; }
    
    public virtual Sex Sex { get; set; }
}