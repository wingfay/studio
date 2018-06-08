using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common.MQ.ActiveMQ
{
    /// <summary>
    /// ActiveMQ生产者,打开连接,向指定队列中发送数据
    /// </summary>
    public class ActiveMQProducer : ActiveMQ, IMessageQueue, IDisposable
    {
        
        /// <summary>
        /// 队列缓存字典
        /// </summary>
        private readonly ConcurrentDictionary<string, IMessageProducer> _concrtProcuder = new ConcurrentDictionary<string, IMessageProducer>();
        /// <summary>
        /// 打开连接
        /// </summary>
        public void Open()
        {
            if (string.IsNullOrWhiteSpace(this.BrokerUri))
                throw new MemberAccessException("未指定BrokerUri");

            var factory = new ConnectionFactory(this.BrokerUri);
            if (string.IsNullOrWhiteSpace(this.UserName) && string.IsNullOrWhiteSpace(this.Password))
                _connection = factory.CreateConnection();
            else
                _connection = factory.CreateConnection(this.UserName, this.Password);
            _connection.Start();
            _session = _connection.CreateSession();
        }


        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            IMessageProducer _p = null;
            foreach (var p in this._concrtProcuder)
            {
                if (this._concrtProcuder.TryGetValue(p.Key, out _p))
                {
                    _p.Close();
                }
            }
            this._concrtProcuder.Clear();

            _session.Close();
            _connection.Close();
        }

        /// <summary>
        /// 向队列发送数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="body">数据</param>
        public void Put<T>(T body)
        {
            Send(this.QueueName, body);
        }

        /// <summary>
        /// 向指定队列发送数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="body">数据</param>
        /// <param name="queueName">指定队列名</param>
        public void Put<T>(T body, string queueName)
        {
            Send(queueName, body);
        }

        /// <summary>
        /// 创建队列
        /// </summary>
        /// <param name="queueName"></param>
        private IMessageProducer CreateProducer(string queueName)
        {
            if (_session == null)
            {
                Open();
            }

            //创建新生产者

            return this._concrtProcuder.GetOrAdd(queueName, (name) =>
            {
                IMessageProducer _newProducer = null;
                switch (MQMode)
                {
                    case MQMode.Queue:
                        {
                            _newProducer = _session.CreateProducer(new ActiveMQQueue(name));
                            break;
                        }
                    case MQMode.Topic:
                        {
                            _newProducer = _session.CreateProducer(new ActiveMQTopic(name));
                            break;
                        }
                    default:
                        {
                            throw new Exception(string.Format("无法识别的MQMode类型:{0}", MQMode.ToString()));
                        }
                }
                return _newProducer;
            });
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="queueName">队列名称</param>
        /// <typeparam name="T"></typeparam>
        /// <param name="body">数据</param>
        private void Send<T>(string queueName, T body)
        {
            var producer = CreateProducer(queueName);
            IMessage msg;
            if (body is byte[])
            {
                msg = producer.CreateBytesMessage(body as byte[]);
            }
            else if (body is string)
            {
                msg = producer.CreateTextMessage(body as string);
            }
            else
            {
                msg = producer.CreateObjectMessage(body);
            }
            if (msg != null)
            {
                producer.Send(msg, MsgDeliveryMode.Persistent, MsgPriority.Normal, TimeSpan.MinValue);
            }
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