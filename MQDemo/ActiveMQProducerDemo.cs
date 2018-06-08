using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.MQ.ActiveMQ
{
    public class ActiveMQProducerDemo
    {
        public ActiveMQProducer CreateActiveMQProducer()
        {
            return new ActiveMQProducer()
            {
                BrokerUri = ConfigurationManager.AppSettings["MQ:BrokerUri"],
                UserName = ConfigurationManager.AppSettings["MQ:UserName"],
                Password = ConfigurationManager.AppSettings["MQ:Password"],
                MQMode = MQMode.Queue,

            };

        }



        /// <summary>
        /// Demo
        /// </summary>
        /// <param name="shortVideoId"></param>
        public void PutMessage(string message)
        {
            using (ActiveMQProducer producer = CreateActiveMQProducer())
            {
                producer.Put(message, "Demo");
            }
        }
    }
}
