namespace coreCodeFirstApproachProject.Models
{
    public class Brand
    {
        public int BrandId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime BrandAddedDate { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
