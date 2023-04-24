namespace coreCodeFirstApproachProject.Models
{
    public class Item
    {
        public Product Product { get; set; }
        public Brand Brand { get; set; }
        public Category Category { get; set; }
        public int Quantity { get; set; }
    }
}
