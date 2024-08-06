using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Rules
{
    public class BuyMoreBoxesDiscountRule : RuleBase
    {
        public readonly int BoxCount = 0;
        public readonly int PercentOff = 0;

        public BuyMoreBoxesDiscountRule(int boxes, int percentOff)
        {
            this.BoxCount = boxes;
            this.PercentOff = percentOff;

            this.Name = $"任 {this.BoxCount} 箱結帳 {100 - this.PercentOff} 折!";
            this.Note = "熱銷飲品 限時優惠";
        }

        //public override IEnumerable<Discount> Process(Product[] products)
        //{
        //    List<Product> matched_products = new List<Product>();

        //    foreach (var p in products)
        //    {
        //        matched_products.Add(p);

        //        if (matched_products.Count == this.BoxCount)
        //        {
        //            // 符合折扣
        //            yield return new Discount()
        //            {
        //                Amount = matched_products.Select(p => p.Price).Sum() * this.PercentOff / 100,
        //                Products = matched_products.ToArray(),
        //                RuleName = this.Name,
        //            };
        //            matched_products.Clear();
        //        }
        //    }
        //}

        public override IEnumerable<Discount> Process(CartContext cart)
        {
            List<Product> matched_products = new List<Product>();

            foreach (var p in cart.PurchasedItems)
            {
                matched_products.Add(p);

                if (matched_products.Count == this.BoxCount)
                {
                    // 符合折扣
                    yield return new Discount()
                    {
                        Amount = matched_products.Select(p => p.Price).Sum() * this.PercentOff / 100,
                        Products = matched_products.ToArray(),
                        //RuleName = this.Name,
                        Rule = this,
                    };
                    matched_products.Clear();
                }
            }
        }
    }


}
