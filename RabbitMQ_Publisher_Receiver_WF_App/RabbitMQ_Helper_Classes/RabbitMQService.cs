using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ_Publisher_Receiver_WF_App.RabbitMQ_Helper_Classes
{
    public class RabbitMQService
    {
        private readonly string _hostName = "localhost";

        public IConnection GetRabbitMQConnection()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory()
            {
                HostName = _hostName
            };

            return connectionFactory.CreateConnection();
        }
    }
}
