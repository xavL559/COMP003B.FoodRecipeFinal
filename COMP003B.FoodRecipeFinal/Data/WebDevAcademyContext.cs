using COMP003B.FoodRecipeFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace COMP003B.FoodRecipeFinal.Data
{
    public class WebDevAcademyContext : DbContext
    {
        public WebDevAcademyContext(DbContextOptions<WebDevAcademyContext> options)
            : base(options)
        {
        }

        public DbSet<FoodViewModel> Food { get; set; }

        public DbSet<RecipeViewModel> Recipe { get; set; }

        public DbSet<FoodRecipeViewModel> FoodRecipe { get; set; }

        
    }
            
    

}