using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace motorak.bll.ModelVM.Payment
{
    public class ProcessPaymentRequest
    {
        public string PaymentMethod { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string CVV { get; set; }
        public string CardHolderName { get; set; }
        public decimal Amount { get; set; }
    }
}
