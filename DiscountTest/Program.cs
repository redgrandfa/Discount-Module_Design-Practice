
using DiscountTest;

static void Main(string[] args)
{
    var products = LoadProducts();
    foreach (var p in products)
    {
        Console.WriteLine($"- {p.Name}      {p.Price:C}");
    }
    Console.WriteLine($"Total: {CheckoutProcess(products.ToArray()):C}");
}


static decimal CheckoutProcess(Product[] products)
{
    decimal amount = 0;
    foreach (var p in products)
    {
        amount += p.Price;
    }
    return amount;
}

static IEnumerable<Product> LoadProducts()
{
    return JsonConvert.DeserializeObject<Product[]>(File.ReadAllText(@"products.json"));
}