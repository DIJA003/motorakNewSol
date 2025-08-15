using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using motorak.dal.Enums.CartEnums;

namespace motorak.bll.ModelVM.ShoppingCart
{
    public class AddToCartRequest
    {
        public int CarId { get; set; }
        public CartItemType ItemType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
