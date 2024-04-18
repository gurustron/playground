// See https://aka.ms/new-console-template for more information

using System.Text;
using Confluent.Kafka;

Console.WriteLine("Hello, World!");

var array = Enumerable.Range(0, 256)
    .Select(i => (byte)i)
    .ToArray();
var s = Convert.ToHexString(array);
var bytes = Convert.FromHexString(s);
var sequenceEqual = bytes.SequenceEqual(array);
Console.WriteLine(sequenceEqual);

var config = new ProducerConfig
{
    BootstrapServers = "127.0.0.1:9092",

};
using (var producer = new ProducerBuilder<Null, string>(config).Build())
{
    var result = await producer.ProduceAsync("test-topic", new Message<Null, string> { Value="a log message" });
}
// using (var producer = new ProducerBuilder<string, string>(config).Build())
// {
//     await producer.ProduceAsync("test-topic", new Message<string, string>
//     {
//         Key = "someNewKey",
//         Value = """
//                 {
//                     "Hahah":"nice"
//                 }
//                 """
//     });
// }

