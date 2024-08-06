
namespace Lib
{
    // 進階：
    // 指定商品
    // 在湊折扣時先用售價來排序，從價格高的開始湊折扣
    //  配對折扣
    //  折扣排除

    public abstract class RuleBase
    {
        public int Id;
        public string Name;
        public string Note;

        //規則 消化 購物車 => 產生 多個折扣紀錄
        public abstract IEnumerable<Discount> Process(CartContext cart);

        public string ExclusiveTag = null;//若他的值不是 <NULL>, 則代表這個折扣規則屬於 “獨佔折扣”
    }
    public class Discount
    {
        public int Id;
        //public string RuleName;
        public RuleBase Rule;
        public Product[] Products;
        public decimal Amount;
    }
}