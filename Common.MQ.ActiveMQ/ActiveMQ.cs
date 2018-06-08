using Apache.NMS;

namespace Common.MQ.ActiveMQ
{
    /// <summary>
    /// ActiveMQ
    /// </summary>
    public abstract class ActiveMQ
    {
        #region 监听连接对象
        protected IConnection _connection;
        protected ISession _session;
        protected IMessageConsumer _consumer;
        #endregion

        public ActiveMQ()
        {
            ClientID = "ClientID";
        }

        /// <summary>
        /// 连接地址
        /// </summary>
        public string BrokerUri { get; set; }

        /// <summary>
        /// 用于登录的用户名,必须和密码同时指定
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用于登录的密码,必须和用户名同时指定
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 队列名称
        /// </summary>
        public string QueueName { get; set; }

        /// <summary>
        /// 生产者端需要的客户端标识
        /// </summary>
        public string ClientID { get; set; }

        /// <summary>
        /// 指定使用队列的模式
        /// </summary>
        public MQMode MQMode { get; set; }
    }
    /// <summary>
    /// 队列模式
    /// </summary>
    public enum MQMode
    {
        /// <summary>
        /// 队列，点对点模式。
        /// 使用此模式。一个生产者向队列存入一条消息之后,只有一个消费者能触发消息接收事件。
        /// </summary>
        Queue,
        /// <summary>
        /// 主题，发布者/订阅模式。
        /// 使用此模式，一个生产者向队列存入一条消息之后,所有订阅当前的主题的消费者都能触发消息接收事件。
        /// 使用此模式，必须先创建消费者，再创建生产者。
        /// </summary>
        Topic
    }
}