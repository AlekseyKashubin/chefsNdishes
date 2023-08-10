#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace chefsNdishes.Models;

public class Dish
{
    [Key]
    public int DishId { get; set; }

    [Required(ErrorMessage = "Must have a Topic")]
    [MinLength(2, ErrorMessage = "Must be min 2 char")]
    [MaxLength(155, ErrorMessage = "Must be max 155 char")]
    public string DishName { get; set; }
    [Required]
    [Range(1, 5)]
    public int Tastiness { get; set; }
    [Required]
    [Range(1, 10000)]
    public int Calories { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;

public int ChefId { get; set; }
// '?'- is setting author to null might need it
public Chef? Creator { get; set; }


}