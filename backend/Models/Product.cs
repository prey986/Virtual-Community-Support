namespace backend.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Productname { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
