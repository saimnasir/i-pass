namespace Patika.Shared.Extensions
{
    public static class RabbitMQExtensions
    {
        //public static IModel InitiateConnectionChannel(this ConnectionFactory factory, IConnection connection, string queueName)
        //{
        //    connection = factory.CreateConnection();
        //    var channel = connection.CreateModel();
        //    channel.QueueDeclare(queue: queueName,
        //       durable: true,
        //       exclusive: false,
        //       autoDelete: false,
        //       arguments: null);

        //    return channel;
        //}

        //public static void InitiateQueueConsumer(this IModel channel, Func<BasicDeliverEventArgs, Task> func, string queueName)
        //{
        //    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

        //    var consumer = new EventingBasicConsumer(channel);
        //    consumer.Received += async (sender, ea) =>
        //    {
        //        await func(ea);
        //    };
        //    channel.BasicConsume(queue: queueName,
        //                         autoAck: false,
        //                         consumer: consumer);
        //}
    }
}
