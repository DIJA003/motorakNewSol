

using System.Transactions;

namespace Motorak.BLL.ModelVM.Purchases
{
    public class PurchaseCreateDto
    {
        public string SellerName { get; set; }
        public string PaymentMethod { get; set; }
        public int TotalPrice { get; set; }
        public int CustomerId { get; set; }
        public int CarId { get; set; }
        public TransactionStatus Status { get; set; }
    }
}
