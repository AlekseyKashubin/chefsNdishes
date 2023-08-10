#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
// Add this using statement to access NotMapped
using System.ComponentModel.DataAnnotations.Schema;
namespace chefsNdishes.Models;
public class Chef
{
    [Key]
    public int ChefId { get; set; }



    [Required]
    [MinLength(2, ErrorMessage = "First name must be min of 2 char")]
    public string ChefFirstName { get; set; }



    [Required]
    [MinLength(2, ErrorMessage = "Last name must be min of 2 char")]
    public string ChefLastName { get; set; }


    [Required(ErrorMessage = "Birthdate is required")]
    [DateMinimumAge(18, ErrorMessage ="Must be 18 or older to sign up")]
    public DateTime Dob { get; set; }




    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;



    public List<Dish> DishesByChefs { get; set; } = new List<Dish>();
}

public class DateMinimumAgeAttribute : ValidationAttribute
{
    public DateMinimumAgeAttribute(int minimumAge)
    {
        MinimumAge = minimumAge;
    }

    public override bool IsValid(object value)
    {
        DateTime date;
        if ((value != null && DateTime.TryParse(value.ToString(), out date)))
        {
            return date.AddYears(MinimumAge) < DateTime.Now;
        }

        return false;
    }

    public int MinimumAge { get; }
}