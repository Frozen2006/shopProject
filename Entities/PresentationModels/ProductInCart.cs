namespace iTechArt.Shop.Entities.PresentationModels
{
    public class ProductInCart
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public double Price { get; set; }
        public string SellByWeight { get; set; }
        public double AverageWeight { get; set; }
        public string UnitOfMeasure { get; set; }
        public double Count { get; set; }
        public double TotalPrice { get; set; }
    }
}
