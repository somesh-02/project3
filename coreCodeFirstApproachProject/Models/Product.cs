﻿namespace coreCodeFirstApproachProject.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public int AvailableQuantity { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public DateTime ProductAddedDate { get; set; }

        public Category Category { get; set; }
        public Brand Brand { get; set; }
    }
}
