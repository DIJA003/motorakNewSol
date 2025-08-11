using motorak.dal.Entites;
using motorak.dal.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Motorak.DAL.Entities
{
    public class Purchase : Transactions
    {
        public string SellerName { get; private set; }

        [ForeignKey(nameof(CarId))]
        public int CarId { get; private set; }
        public virtual Car Car { get; private set; }


        [ForeignKey(nameof(CustomerId))]
        public int CustomerId { get; private set; }
        public virtual Customer Customer { get; private set; }

        public Purchase(string sellerName, string paymentMethod, decimal totalPrice, int customerId , int carId)
            : base(paymentMethod, totalPrice, customerId, carId)
        {
            SellerName = sellerName;
        }

        private Purchase() { }
    }
}
