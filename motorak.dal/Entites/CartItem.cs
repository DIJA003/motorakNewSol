using System.ComponentModel.DataAnnotations.Schema;
using motorak.dal.Entites;
using motorak.dal.Enums.CartEnums;

namespace Motorak.DAL.Entites
{
    public class CartItem
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        [ForeignKey(nameof(CarId))]
        public virtual Car Car { get; set; }

        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        public int Count { get; set; }
        public CartItemType ItemType { get; set; }
        public DateTime? RentStartDate { get; set; }
        public DateTime? RentEndDate { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
