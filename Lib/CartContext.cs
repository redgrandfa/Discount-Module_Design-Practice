using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    //user影響購物車
    //收納了每個顧客，購物車裡的所有狀態，
    //包含結帳後的發票資訊。
    //這邊我就沒很嚴格的控制狀態了，結帳兩次以最後一次為準。也因此，購買的商品清單，以及總金額跟折扣明細都包含在內。
    public class CartContext
    {
        public readonly List<Product> PurchasedItems = new List<Product>();
        public readonly List<Discount> AppliedDiscounts = new List<Discount>();
        public decimal TotalPrice = 0m;
        public IEnumerable<Product> GetVisiblePurchasedItems(string exclusiveTag)
        {
            if (string.IsNullOrEmpty(exclusiveTag)) return this.PurchasedItems;
            return this.PurchasedItems.Where(p => !p.Tags.Contains(exclusiveTag));
            //foreach(var p in cart.PurchasedItems)
            //foreach (var p in cart.GetVisiblePurchasedItems(this.ExclusiveTag))
        }
        //封裝在框架的開發，
        //完全不影響規則開發
    }
}
