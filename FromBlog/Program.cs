using Lib;
using Lib.Rules;
using Newtonsoft.Json;

namespace FromBlog
{
    internal class Program
    {
        static int _seed = 0;
        static IEnumerable<Product> LoadProducts(string filename = @"products.json")
        {
            foreach (var p in JsonConvert.DeserializeObject<Product[]>(File.ReadAllText(filename)))
            {
                _seed++;
                p.Id = _seed;
                yield return p;
            }
        }

        static IEnumerable<RuleBase> LoadRules()
        {
            yield return new BuyMoreBoxesDiscountRule(2, 12);   // 買 2 箱，折扣 12%
            yield return new TotalPriceDiscountRule(1000, 100);
            yield break;
        }

        static void Main(string[] args)
        {
            CartContext cart = new CartContext();
            POS pos = new POS();

            cart.PurchasedItems.AddRange(LoadProducts("TestData/products3.json"));
            pos.ActivedRules.AddRange(LoadRules());

            pos.CheckoutProcess(cart);

            Console.WriteLine($"購買商品:");
            Console.WriteLine($"---------------------------------------------------");
            foreach (var p in cart.PurchasedItems)
            {
                Console.WriteLine($"- {p.Id,02}, [{p.SKU}] {p.Price,8:C}, {p.Name}, {p.TagsValue}");
            }
            Console.WriteLine();

            Console.WriteLine($"折扣:");
            Console.WriteLine($"---------------------------------------------------");
            foreach (var d in cart.AppliedDiscounts)
            {
                Console.WriteLine($"- 折抵 {d.Amount,8:C}, {d.Rule.Name} ({d.Rule.Note})");
                foreach (var p in d.Products) Console.WriteLine($"  * 符合: {p.Id,02}, [{p.SKU}], {p.Name}, {p.TagsValue}");
                Console.WriteLine();
            }
            Console.WriteLine();

            Console.WriteLine($"---------------------------------------------------");
            Console.WriteLine($"結帳金額:   {cart.TotalPrice:C}");

            Console.Read();
        }



        //static decimal CheckoutProcess(Product[] products)
        //{
        //    decimal amount = 0;
        //    foreach (var p in products)
        //    {
        //        amount += p.Price;
        //    }
        //    return amount;
        //}
        //static decimal CheckoutProcess(Product[] products, RuleBase[] rules) {
        //    List<Discount> discounts = new List<Discount>();

        //    foreach (var rule in rules)
        //    {
        //        discounts.AddRange(rule.Process(products));
        //    }

        //    decimal amount_without_discount = CheckoutProcess(products);
        //    decimal total_discount = 0;

        //    foreach (var discount in discounts)
        //    {
        //        total_discount += discount.Amount;
        //        Console.WriteLine($"- 符合折扣 [{discount.RuleName}], 折抵 {discount.Amount} 元");
        //    }

        //    return amount_without_discount - total_discount;
        //}
    }



}