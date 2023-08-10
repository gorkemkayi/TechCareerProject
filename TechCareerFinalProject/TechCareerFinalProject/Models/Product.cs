namespace TechCareerFinalProject.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<ListDetail> ListDetails { get; set; }


    }
}
