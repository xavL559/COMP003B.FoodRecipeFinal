using System.ComponentModel.DataAnnotations;

namespace COMP003B.FoodRecipeFinal.Models
{
    public class RecipeViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Recipe { get; set; }
    }
}
