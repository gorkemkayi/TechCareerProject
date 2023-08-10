using Microsoft.AspNetCore.Mvc.Rendering;

namespace TechCareerFinalProject.Models
{
    public class AddProductToListViewModel
    {
        public int ListId { get; set; }
        public int SelectedProductId { get; set; }
        public List<SelectListItem> ProductList { get; set; }
        public string ProductDescription { get; set; }
    }
}
