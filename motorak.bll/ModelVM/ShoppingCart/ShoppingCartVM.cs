

namespace motorak.bll.ModelVM.ShoppingCart
{
    public class ShoppingCartVM
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public decimal? DailyRentPrice { get; set; }
        public string ImagePath { get; set; }

        
        public int Count { get; set; }
        public string UserId { get; set; }
    }
}

