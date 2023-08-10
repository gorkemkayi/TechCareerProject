using Microsoft.AspNetCore.Mvc.Rendering;

namespace TechCareerFinalProject.Models
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public int CategoryID { get; set; }
        public SelectList Categories { get; set; }
        public IFormFile Image { get; set; }
    }    
}
