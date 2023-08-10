namespace TechCareerFinalProject.Models
{
    public class ListDetail
    {
        public int ID { get; set; }
        public int ListId { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
