using System.Buffers;
using System.Collections.Frozen;
using System.Diagnostics;
using System.Numerics;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using CommunityToolkit.HighPerformance;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine(SearchValues.Create(1, 5, 8).Contains(1));

var indexOfAny = new byte[] { 9, 2, 5, 8 }.AsSpan().IndexOfAny(SearchValues.Create(1, 5, 8));

var ssv = SearchValues.Create(["one", "two"], StringComparison.OrdinalIgnoreCase);

var r = new[] { "test", "this contains one", "one" }.AsSpan().IndexOfAny(ssv);
var ofAny = "this contains one".AsSpan().IndexOfAny(ssv);
var ofAny1 = "this contains one".AsSpan().IndexOfAny(ssv);
var handler = new SocketsHttpHandler
{
    EnableMultipleHttp2Connections = false,EnableMultipleHttp3Connections = false
    // SslOptions =
    // {
    //     EnabledSslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13 // Specify desired protocols
    // },
    // AllowAutoRedirect = true
};
var client = new HttpClient(handler);
client.Timeout = TimeSpan.FromSeconds(4);

var request = new HttpRequestMessage(HttpMethod.Post, "https://gql.tokopedia.com/graphql/ShopProducts");
request.Headers.Add("User-Agent", "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/140.0.0.0 Safari/537.36");
// request.Headers.Add("Cookie", "_abck=94220D6B3E9983A0477EE01E57182E03~-1~YAAQHOzEF4s+STCaAQAAQQMkOQ4qaE9ZvjnBaM6yhdqgztng5pr8uUsX+6yKPdys2znVK7e9qAs3A0TiYsogzSLG8h5S4B+1oFgOV3BdnuGnLPwD5uwlH4Mj+WwpnDRrZJOPFS24H3nvw3zYisyazFkkJinBU1ideZRIiYU5Gnwjkz0DesAF3k9kVGWrm82soYKy66tzCgRI6hBGfvx3OpeZ86VzEtVDmsJhfmfnKTbb0UqS23g1DXyidfOP1EJAXW2vbg7FGgbE5v0IiDm/FpdIAB/kfhvjDTANM6oCrzJmybdfKHlYqgHgFYTAtU/TNI4OA+V7rmvrWkFRAv+3T0EuSDca0QRNMYA8qg4yDahospjqPOUlhHwH3yMHEFUI2a5DPJ1egkH/Mo+Ht9ellG3ybUHDyDz2TTgKJNNFH6Jhz9gl2tK0QZYXHxRZrZqsQYkXGdiERa8nPw==~-1~-1~-1~-1~-1; ak_bmsc=B3264E8F4416F8CE52089DA8835D9F28~000000000000000000000000000000~YAAQHOzEF4w+STCaAQAAQQMkOR1f3SAbxcE0wTgFzKVNNtvA4SDjAKt5Vdzki8LE3l6N1CUI3Kkpn0yA9WmxOR54kftRDQAuNmJ2lDk+5q68ySYr8RTBlX0GGRVRUaauJyilv9Yk0km97DV3Ml7wSgSQYpkG3Y7Id0VeaM9p34/LSkhI1DxObU+7uZmurkk/RIxJOjVzgQpWsmfwjlsMtTDm4YqKOGsiuaDZF81uesOWZGIe7CbTFeW144oHkfQ6kYmBFryF6kuHGw0EcfSFevrqdZz10GGiCLoqtrUaMMJGV64advjJif8EeRBqfvbM2Gxq0+nip6qkC47fNQeg2QAMzeHIKyHe8O6V1RhZJlTc; bm_sz=0110A424F9A74025B033FA350A1DF9CD~YAAQHOzEF40+STCaAQAAQQMkOR1Pja5DwuKw5oJWwxqu6iF0SnlDSfB4rzGrT5wUEbULbFnskKuuGJpldEFGoQYbN80NRPZhidwsPdP3VhvsXErzBjjdNZZJPmMQSM/1ir0j4ypLaCA1Snp7zAJwAwpyjR1VY98ituyneey7D0MDgTBeLK/bntg5Gyz6TaCBrUqFvpZMmdEUEC33GeJZjsI4w+lWiL99iOMhgIv02ox7aOhYnIto3/GU6vOTqb+ehGiVBV0pHPemZjJ5I6O4l2zmLj6YP8K/pNvM3FG4LRBzx4msS6FsaIUTrG9L8hPyk631FvVd6800tASWN6Sj+svAupML4GzXeoVQB7D4yaQU4w==~3163441~4534854");
request.Headers.Add("Accept", "*/*");
request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
request.Headers.Add("Host", "gql.tokopedia.com");
var content = new StringContent("\n\n[ {\n  \"operationName\" : \"ShopProducts\",\n  \"variables\" : {\n    \"source\" : \"shop\",\n    \"sid\" : \"1577650\",\n    \"page\" : 1,\n    \"perPage\" : 1,\n    \"etalaseId\" : \"etalase\",\n    \"sort\" : 1,\n    \"user_districtId\" : \"2274\",\n    \"user_cityId\" : \"176\",\n    \"user_lat\" : \"0\",\n    \"user_long\" : \"0\",\n    \"usecase\" : \"ace_get_shop_product_v2\"\n  },\n  \"query\" : \"query ShopProducts($sid:String!,$source:String,$page:Int,$perPage:Int,$keyword:String,$etalaseId:String,$sort:Int,$user_districtId:String,$user_cityId:String,$user_lat:String,$user_long:String,$usecase:String){\\nGetShopProduct(shopID:$sid,source:$source,filter:{page:$page,perPage:$perPage,fkeyword:$keyword,fmenu:$etalaseId,sort:$sort,user_districtId:$user_districtId,user_cityId:$user_cityId,user_lat:$user_lat,user_long:$user_long,usecase:$usecase}){\\nstatus\\nerrors\\nlinks{\\nprev\\nnext\\n__typename\\n}\\ndata{\\nname\\nproduct_url\\nproduct_id\\nprice{\\ntext_idr\\n__typename\\n}\\nprimary_image{\\noriginal\\nthumbnail\\nresize300\\n__typename\\n}\\nflags{\\nisSold\\nisPreorder\\nisWholesale\\nisWishlist\\n__typename\\n}\\ncampaign{\\ndiscounted_percentage\\noriginal_price_fmt\\nstart_date\\nend_date\\n__typename\\n}\\nlabel{\\ncolor_hex\\ncontent\\n__typename\\n}\\nlabel_groups{\\nposition\\ntitle\\ntype\\nurl\\nstyles{\\nkey\\nvalue\\n__typename\\n}\\n__typename\\n}\\nbadge{\\ntitle\\nimage_url\\n__typename\\n}\\nstats{\\nreviewCount\\nrating\\naverageRating\\n__typename\\n}\\ncategory{\\nid\\n__typename\\n}\\n__typename\\n}\\n__typename\\n}\\n}\\n\"\n} ]", null, "application/json");
request.Content = content;
var response = await client.SendAsync(request);
response.EnsureSuccessStatusCode();
Console.WriteLine(await response.Content.ReadAsStringAsync());


// using (HttpClient client = new HttpClient())
// {
//     client.Timeout = TimeSpan.FromSeconds(10);
//     client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/109.0.0.0 Safari/537.36");
//     client.DefaultRequestHeaders.Accept.ParseAdd("*/*");
//     client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("en-US,en;q=0.9");
//
//     using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, new Uri("https://gql.tokopedia.com/graphql/ShopProducts")))
//     {
//         string json = @"[{""operationName"":""ShopProducts"",""variables"":{""source"":""shop"",""sid"":""1577650"",""page"":1,""perPage"":80,""etalaseId"":""etalase"",""sort"":1,""user_districtId"":""2274"",""user_cityId"":""176"",""user_lat"":""0"",""user_long"":""0"",""usecase"":""ace_get_shop_product_v2""},""query"":""query ShopProducts($sid:String!,$source:String,$page:Int,$perPage:Int,$keyword:String,$etalaseId:String,$sort:Int,$user_districtId:String,$user_cityId:String,$user_lat:String,$user_long:String,$usecase:String){\nGetShopProduct(shopID:$sid,source:$source,filter:{page:$page,perPage:$perPage,fkeyword:$keyword,fmenu:$etalaseId,sort:$sort,user_districtId:$user_districtId,user_cityId:$user_cityId,user_lat:$user_lat,user_long:$user_long,usecase:$usecase}){\nstatus\nerrors\nlinks{\nprev\nnext\n__typename\n}\ndata{\nname\nproduct_url\nproduct_id\nprice{\ntext_idr\n__typename\n}\nprimary_image{\noriginal\nthumbnail\nresize300\n__typename\n}\nflags{\nisSold\nisPreorder\nisWholesale\nisWishlist\n__typename\n}\ncampaign{\ndiscounted_percentage\noriginal_price_fmt\nstart_date\nend_date\n__typename\n}\nlabel{\ncolor_hex\ncontent\n__typename\n}\nlabel_groups{\nposition\ntitle\ntype\nurl\nstyles{\nkey\nvalue\n__typename\n}\n__typename\n}\nbadge{\ntitle\nimage_url\n__typename\n}\nstats{\nreviewCount\nrating\naverageRating\n__typename\n}\ncategory{\nid\n__typename\n}\n__typename\n}\n__typename\n}\n}\n""}]";
//         request.Content = new StringContent(json, Encoding.UTF8, "application/json");
//         using (HttpResponseMessage response = await client.SendAsync(request))
//         {
//             using (StreamWriter sw1 = new StreamWriter("response.txt"))
//             {
//                 var buffer = response.ToString();
//                 sw1.Write(buffer);
//             }
//         }
//     }
// }
// SearchValues.Create("2019/0002391");
// Assembly.Load("Xin.Service")
//     .GetExportedTypes()
//     .Where(a => a.Name.EndsWith("Service") && !a.IsInterface && !a.IsAbstract);
int numRows = 3;
int numColumns = 2;
var firstFor = Random.Shared.Next(100) > 1000
    ? Generator(0, 1, i => i < numRows)
    : Generator(numRows - 1, -1, i => i >= 0);

var secondFor = Generator(0, 1, i => i < numColumns);

foreach (var row in firstFor)
foreach (var column in secondFor)
{
    Console.WriteLine($"{row} - {column}");
}

IEnumerable<int> Generator(int start, int incr, Func<int, bool> breakCondition)
{
    for (int j = start; breakCondition(j); j += incr)
    {
        yield return j;
    }
}
foreach (var Size in new[] { 10, 100, 10_000 })
{
    var _source = Enumerable.Range(0, Size)
         .Select(i => new KeyValuePair<string, string>($"Key{i}", $"Value{i}"))
         .ToArray();
    var _middleKey = $"Key{Size / 2}";
    var _middleKeyPlusOne = $"Key{Size / 2 + 1}";

    var _dictionary = new Dictionary<string, string>(_source);
    var _frozen = _dictionary.ToFrozenDictionary();
    Console.WriteLine(_frozen.GetType());
    Console.WriteLine(_dictionary.Comparer as StringComparer);
}

Environment.Exit(0);
var tuple1 = (x:2,y:4);
var tuple2 = (x:0,y:-1);
var tuple3 = tuple1.Add(tuple2);
int[,] array =
{
    { 1, 2, 3 },
    { 4, 5, 6 },
    { 7, 8, 9 }
};

Memory2D<int> memory = array;
var memoryLength = memory.Length.ToInt32();
var dest = new int[memory.Length];
memory.CopyTo(dest);
Task.Run(async () =>
{
    await Task.Delay(100);
    return (1, 100);
});

int[] delays = [100, 50, 0];

var tasksToRun = delays
    .Select(async (delay, index) =>
    {
        await Task.Delay(delay);
        return (index, delay);
    });
var sw = Stopwatch.StartNew();
await foreach (var completed in Task.WhenEach(tasksToRun))
{
    Console.Write($"{completed.Status} ");
    var result = completed.Result;
    Console.WriteLine($"index: {result.index} delay: {result.delay} elapsed: {sw.ElapsedMilliseconds}ms");
    break;
}

Environment.Exit(0);

async Task TestAsyncEnumerable(string name, IAsyncEnumerable<Task<int>> tasks)
{
    Console.WriteLine($"Before: {name}");
    await foreach (var t in tasks)
    {
        var i = await t;
        Console.WriteLine($"\t\t{name}: {i}");
    }
    Console.WriteLine($"Between: {name}");
    await foreach (var t in tasks)
    {
        var i = await t;
        Console.WriteLine($"\t\t{name}: {i}");
    }
    Console.WriteLine($"After: {name}");
}

async IAsyncEnumerable<Task<int>> GenerateAsyncEnumerable()
{
    await Task.Yield();
    for (int j = 0; j < 3; j++)
    {
        yield return Task.FromResult(j);
    }
}

await TestAsyncEnumerable(nameof(GenerateAsyncEnumerable), GenerateAsyncEnumerable());

Task<int>[] tasks = [Task.FromResult(0), Task.FromResult(1), Task.FromResult(2)];

await TestAsyncEnumerable(nameof(Task.WhenEach), Task.WhenEach(tasks));

Environment.Exit(0);

string password = "password123!";
string b64 = Convert.ToBase64String(Encoding.ASCII.GetBytes(password));
string b641 = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
string b6412 = Convert.ToBase64String(Encoding.Unicode.GetBytes(password));
ServiceCollection services = new ServiceCollection();

services.AddScoped<IValidator<object, Cat>, MyClass<object, Cat>>();
services.AddScoped<IValidator<object, Dog>, MyClass<object, Dog>>();
services.AddScoped<IValidator<Cat, Dog>, MyClass<Cat, Dog>>();
services.AddSingleton<IGenericInterface<Dog>, ConcreteClassA>();
services.AddSingleton<IInterface>(sp => sp.GetRequiredService<IGenericInterface<Dog>>());
services.AddSingleton<IInterface, ConcreteClassB>();
services.AddSingleton<ConcreteClassFactory>();

ServiceProvider sp = services.BuildServiceProvider();
using (IServiceScope scope = sp.CreateScope())
{
    IEnumerable<IValidator<object, Cat>>? validators =
        scope.ServiceProvider.GetService<IEnumerable<IValidator<object, Cat>>>();
}

ConcreteClassFactory t = sp.GetRequiredService<ConcreteClassFactory>();

Console.WriteLine(t.Get<Cat>());
Environment.Exit(0);
t.Do(new Cat());
AssemblyName aName = new AssemblyName("DynamicAssemblyExample");
AssemblyBuilder ab =
    AssemblyBuilder.DefineDynamicAssembly(
        aName,
        AssemblyBuilderAccess.Run);

// The module name is usually the same as the assembly name.
ModuleBuilder mb = ab.DefineDynamicModule(aName.Name ?? "DynamicAssemblyExample");

TypeBuilder tb = mb.DefineType(
    "MyDynamicType",
    TypeAttributes.Public);

FieldInfo firstOrDefault = typeof(TestFuncPointers).GetFields().FirstOrDefault();
Type type = firstOrDefault.FieldType;
Type type1 = typeof(delegate*<int, int>);
bool b = type1 == type;
Type makePointerType = typeof(Func<int>).MakePointerType();


Console.WriteLine("Hello, World!");
int i = 0;
long totalAllocatedBytes = GC.GetTotalAllocatedBytes(true);
Console.WriteLine(new object());
for (int j = 0; j < 10_000; j++)
{
    i = 100L.ToNumberOrDefault(1);
}

for (int j = 0; j < 10_000; j++)
{
    i = 100.ToNumberOrDefault(1);
}


Console.WriteLine(GC.GetTotalAllocatedBytes(true) - totalAllocatedBytes);

public static class Exts
{
    public static TDestination ToNumberOrDefault<TSource, TDestination>(this TSource number, TDestination defaultValue)
        where TSource : struct, INumber<TSource>
        where TDestination : struct, INumber<TDestination>
    {
        try
        {
            return TDestination.CreateChecked(number);
        }
        catch
        {
            return defaultValue;
        }
    }
}


public unsafe class TestFuncPointers
{
    public delegate*<int, int> I;
}

public interface IInterface
{
    Type AnimalType { get; }
    void Do(Animal animal);
}

public interface IGenericInterface<T> : IInterface
{
    void Do(T obj);
}

public abstract class Animal
{
}

public class Dog : Animal
{
}

public class Cat : Animal
{
}

public abstract class ConcreteClassBase<T> : IGenericInterface<T>, IInterface where T : Animal
{
    public abstract void Do(T obj);

    void IInterface.Do(Animal animal) => Do((T)animal);
    public Type AnimalType => typeof(T);
}

public class ConcreteClassA : ConcreteClassBase<Dog>
{
    public override void Do(Dog obj)
    {
    }
}

public class ConcreteClassB : ConcreteClassBase<Cat>
{
    public override void Do(Cat obj)
    {
        Console.WriteLine("cat");
    }
}

public class ConcreteClassFactory
{
    private readonly Dictionary<Type, IInterface> _dictionary;

    public ConcreteClassFactory(IEnumerable<IInterface> concretes)
    {
        _dictionary = concretes.ToDictionary(i => i.AnimalType, i => i);
    }

    public void Do(Animal animal) => _dictionary[animal.GetType()].Do(animal);
    public IGenericInterface<T> Get<T>() => (IGenericInterface<T>)_dictionary[typeof(T)];
}

public interface IValidator<in TModel, in TContext>;

class MyClass<TModel, TContext> : IValidator<TModel, TContext>
{
    public MyClass()
    {
        Console.WriteLine($"MyClass: {typeof(TModel)} - {typeof(TContext)}");
    }
}

public static class TupleExts
{
    public static (TX X, TY Y) Add<TX, TY>(this (TX X, TY Y) left, (TX X, TY Y) right)
        where TX : IAdditionOperators<TX, TX, TX>
        where TY : IAdditionOperators<TY, TY, TY> =>
        (left.X + right.X, left.Y + right.Y);
}

public struct Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public static Point operator +(Point left, Point right) => new Point
    {
        X = left.X + right.X,
        Y = left.Y + right.Y
    };
}
