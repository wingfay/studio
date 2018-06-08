using Apache.NMS;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.MQ.ActiveMQ
{
    public class ActiveMQConsumerDemo
    {
        public static ILog Logger
        {
            get
            {
                return LogManager.GetLogger("Demo");
            }
        }

        public void Start(string ClientID)
        {
            try
            {
                ActiveMQConsumer activeMQConsumer = new ActiveMQConsumer()
                {
                    BrokerUri = ConfigurationManager.AppSettings["MQ:BrokerUri"],
                    UserName = ConfigurationManager.AppSettings["MQ:UserName"],
                    Password = ConfigurationManager.AppSettings["MQ:Password"],
                    MQMode = MQMode.Queue,
                    ClientID = ClientID,

                };


                activeMQConsumer.StartListen("Demo", message =>
                {
                    Demo(message);
                });

                Logger.Info($"ShortVideo_AddShortVideo 监听开始");





                Logger.Info($"短视频监听开始.BrokerUri:{activeMQConsumer.BrokerUri}");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                Console.ReadLine();
                return;
            }

        }


        private long Demo_ReceivedMsg_Count = 0;
        /// <summary>
        /// ShortVideo_AddShortVideo
        /// </summary>
        /// <param name="receivedMsg"></param>
        private void Demo(IMessage receivedMsg)
        {
            if (receivedMsg is ITextMessage message)
            {
                

                Logger.Info(message.Text);

                Logger.Info($"Demo Success: {++Demo_ReceivedMsg_Count}");
            }
        }
    }
}
