using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQ_Publisher_Receiver_WF_App.RabbitMQ_Helper_Classes
{
    class RabbitMQ_Operations
    {

        public void CreateQueue(string queueName, RabbitMQService _rabbitMQService)
        {

            using (var connection = _rabbitMQService.GetRabbitMQConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queueName, false, false, false, null);

                }
            }
        }

        public void Publisher(string _queueName, string message, RabbitMQService _rabbitMQService)
        {
            using (var connection = _rabbitMQService.GetRabbitMQConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.BasicPublish("", _queueName, null, Encoding.UTF8.GetBytes(message));
                }
            }
        }
        public string Receiver(string queueName, RabbitMQService _rabbitMQService)
        {
            string response = "";
            using (var connection = _rabbitMQService.GetRabbitMQConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        response = message;
                    };
                    channel.BasicConsume(queue: queueName,
                                         autoAck: true,
                                         consumer: consumer);
                    Thread.Sleep(1000);
                    return response;
                }
            }
        }
    }
}
