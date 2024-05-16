using System.Text;
using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;

var config = new ProducerConfig
{
    BootstrapServers = "127.0.0.1:9092",

};
using (var producer = new ProducerBuilder<Null, string>(config).Build())
{
    var result = await producer.ProduceAsync("test-topic", new Message<Null, string> { Value="a log message" });
}

var producerBuilder = new ProducerBuilder<string, string>(config);
// producerBuilder.SetKeySerializer(Serializers.Utf8);
using (var producer = producerBuilder.Build())
{
    await producer.ProduceAsync("test-topic", new Message<string, string>
    {
        Key = "someNewKey",
        Value = """
                {
                    "Hahah":"nice"
                }
                """
    });
}
var consumerConfig = new ConsumerConfig
{
    BootstrapServers = "127.0.0.1:9092",
    GroupId = Guid.NewGuid().ToString()
};
using (var consumer = new ConsumerBuilder<Null, string>(consumerConfig).Build())
{
    consumer.Subscribe("test-topic");
    var consumeResult = consumer.Consume();
    Console.WriteLine("Offset: {0} - Message {1}", consumeResult.Offset, consumeResult.Message.Value);
    Console.WriteLine();
    consumer.Commit(consumeResult);
}

