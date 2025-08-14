namespace Motorak.DAL.Entites
{
    public class CartItem
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public int Quantity { get; set; }
    }
}
