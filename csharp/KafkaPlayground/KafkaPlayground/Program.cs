using System.Text;
using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;

// using (var producer = new ProducerBuilder<Null, string>(config).Build())
// {
//     var result = await producer.ProduceAsync("test-topic", new Message<Null, string> { Value="a log message" });
// }
var barrier = new Barrier(2);
var msgNum = 10;
var producer = Task.Run(async () =>
{
    var config = new ProducerConfig
    {
        BootstrapServers = "127.0.0.1:9092",

    };

    var producerBuilder = new ProducerBuilder<string, string>(config);
    using var producer = producerBuilder.Build();

    int i = 0;
    for (; i < 3; i++)
    {
        await producer.ProduceAsync("test-topic", new Message<string, string>
        {
            Key = Guid.NewGuid().ToString("N"),
            Value = $$"""
                      {
                          "key": {{i}}
                          "Hahah":"nice_{{i}}"
                      }
                      """
        });
    }

    barrier.SignalAndWait();

    for (; i < msgNum; i++)
    {
        await Task.Delay(100);
        await producer.ProduceAsync("test-topic", new Message<string, string>
        {
            Key = i.ToString(),
            Value = $$"""
                      {
                          "key": {{i}}
                          "Hahah":"nice_{{i}}"
                      }
                      """
        });
    }
});

var consumerConfig = new ConsumerConfig
{
    BootstrapServers = "127.0.0.1:9092",
    GroupId = Guid.NewGuid().ToString()
};
using (var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build())
{
    barrier.SignalAndWait();
    consumer.Subscribe("test-topic");
    for (int i = 0; i < msgNum; i++)
    {
        var consumeResult = consumer.Consume(1200);
        if (consumeResult is null || consumeResult.IsPartitionEOF)
        {
            break;
        }
        Console.WriteLine("Offset: {0} - Message {1}", consumeResult.Offset, consumeResult.Message?.Value);
        Console.WriteLine();
        consumer.Commit(consumeResult);
    }
}

