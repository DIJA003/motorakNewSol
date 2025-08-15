using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace motorak.bll.ModelVM.ShoppingCart
{
    public class CheckoutViewModel
    {
        public List<ShoppingCartVM> CartItems { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string CVV { get; set; }
        public string CardHolderName { get; set; }
    }
}
