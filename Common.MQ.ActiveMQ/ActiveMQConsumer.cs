using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using System;
using System.Configuration;

namespace Common.MQ.ActiveMQ
{
    /// <summary>
    /// ActiveMQ消费者,打开连接,监听队列,接收到数据之后触发回调
    /// </summary>
    public class ActiveMQConsumer : ActiveMQ, IDisposable
    {
        /// <summary>
        /// 接收到数据回调,ActiveMQ原生IMessage类型
        /// </summary>
        public Action<IMessage> OnMessageReceived { get; set; }


        ///// <summary>
        ///// 打开连接
        ///// </summary>
        private void Open()
        {
            if (string.IsNullOrWhiteSpace(this.BrokerUri))
                throw new MemberAccessException("未指定BrokerUri");
            try  
            {
                //初始化工厂     
                var factory = new ConnectionFactory(this.BrokerUri);  
                //通过工厂建立连接  
                if (string.IsNullOrWhiteSpace(this.UserName) && string.IsNullOrWhiteSpace(this.Password))
                    _connection = factory.CreateConnection();
                else
                    _connection = factory.CreateConnection(this.UserName, this.Password);
                _connection.ClientId = ClientID;
                _connection.Start();
                //通过连接创建Session会话  
                _session = _connection.CreateSession();

            }  
            catch (Exception e)  
            {  
                throw e;  
            }  
        }


        private IMessageConsumer CreateConsumer(string _QueueName=null)
        {

            if(!string.IsNullOrEmpty(_QueueName))
            {
                QueueName = _QueueName;
            }

            if (string.IsNullOrEmpty(QueueName))
            {
                throw new Exception($"队列名称不能为空");
            }

            switch (MQMode)
            {
                case MQMode.Queue:
                    {
                        _consumer = _session.CreateConsumer(new ActiveMQQueue(QueueName));
                        break;
                    }
                case MQMode.Topic:
                    {
                        _consumer = _session.CreateDurableConsumer(new ActiveMQTopic(QueueName), ClientID, null, false);
                        break;
                    }
                default:
                    {
                        throw new Exception(string.Format("无法识别的MQMode类型:{0}", MQMode.ToString()));
                    }
            }

            return _consumer;
        }

        /// <summary>
        /// 单队列监控的时候使用这个
        /// </summary>
        public void StartListen()
        {
            if (_session == null)
            {
                Open();
            }

            CreateConsumer();

            _consumer.Listener += new MessageListener(msg =>
            {
                OnMessageReceived?.Invoke(msg);
            });
        }


        /// <summary>
        /// 多队列名的时候使用这个
        /// </summary>
        /// <param name="QueueName"></param>
        /// <param name="action"></param>
        public void StartListen(string QueueName, Action<IMessage> action)
        {
            if(_session==null)
            {
                Open();
            }

            CreateConsumer(QueueName).Listener += new MessageListener(msg =>
            {
                action?.Invoke(msg);
            });

        }


        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            _consumer?.Close();
            _session?.Close();
            _connection?.Close();
        }
        /// <summary>
        /// 执行与释放或重置非托管资源相关的应用程序定义的任务。
        /// </summary>
        public void Dispose()
        {
            this.Close();
        }
    }
}