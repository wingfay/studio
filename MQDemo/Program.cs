using Common.MQ.ActiveMQ;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQDemo
{
    class Program
    {
        static ActiveMQConsumerDemo  consumerDemo;
        static void Main(string[] args)
        {
            InitLog4Net();


            for(var i=0;i<100;i++)
            {
                ActiveMQProducerDemo activeMQProducerDemo = new ActiveMQProducerDemo();

                activeMQProducerDemo.PutMessage($"Message:[{i}]");
            }


            string ClientID = ConfigurationManager.AppSettings["ClientID"];
            consumerDemo = new ActiveMQConsumerDemo();
            Console.Title = $"PushMQ_{ClientID}";
            consumerDemo.Start(Console.Title);

            Console.ReadLine();
        }

        private static void InitLog4Net()
        {
            var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
        }
    }
}
