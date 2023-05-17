using System.ComponentModel.DataAnnotations;

namespace COMP003B.FoodRecipeFinal.Models
{
    public class FoodViewModel
    {
        public int Id { get; set; }

        [Required]
        public string? FoodName { get; set; }

        [Required]
        public string? FoodDescription { get; set; }

        [Required]
        public string? FoodType { get; set;}

        [Required]
        public int TimeToCook { get; set;}
    }
}
