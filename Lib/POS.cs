using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    //用了 POS 來代表結帳時的所有計算邏輯；
    //目前商店有多少活動正在舉行中，應該是店家控制的，因此 Rules 掛在 POS 底下，是它的 properties 之一
    public class POS
    {
        public readonly List<RuleBase> ActivedRules = new List<RuleBase>();

        public bool CheckoutProcess(CartContext cart)
        {
            // reset cart
            cart.AppliedDiscounts.Clear();

            cart.TotalPrice = cart.PurchasedItems.Select(p => p.Price).Sum();
            foreach (var rule in this.ActivedRules)
            {
                var discounts = rule.Process(cart);
                cart.AppliedDiscounts.AddRange(discounts);

                #region 折扣排除
                if (rule.ExclusiveTag != null)
                {
                    foreach (var d in discounts)
                    {
                        foreach (var p in d.Products) p.Tags.Add(rule.ExclusiveTag);
                    }
                }
                #endregion

                cart.TotalPrice -= discounts.Select(d => d.Amount).Sum();
            }
            return true;
        }
    }

    //Rule 
    //    List<Discount> Process(車)

    //public class Discount = CheckoutModel.DiscountHistory
    //    public int Id;
    //    public RuleBase Rule;  來源規則
    //    public Product[] Products; 
    //    public decimal Amount; 價差

    //public class CartContext = CheckoutModel.CartResult
    //      List<Item>
    //    List<Discount>

    //POS
    //    List<RuleBase> ActivedRules = new List<RuleBase>();
    //    bool CheckoutProcess(CartContext cart) = 
    //        迭代rule
    //            discounts = rule.Process(cart);
    //            cart.AppliedDiscounts.AddRange(discounts);

    //            cart.TotalPrice 根據折扣異動


    //主程式
    //    pos.ActivedRules.AddRange( LoadRules());
    //    cart.PurchasedItems.AddRange( LoadProducts(...));

    //    pos.CheckoutProcess(cart);
}
