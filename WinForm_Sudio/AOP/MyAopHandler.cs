using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm_Sudio.AOP
{
    //AOP方法处理类，实现了IMessageSink接口，以便返回给IContributeObjectSink接口的GetObjectSink方法
    public sealed class MyAopHandler : IMessageSink
    {
        //下一个接收器
        private IMessageSink nextSink;
        public IMessageSink NextSink
        {
            get { return nextSink; }
        }
        public MyAopHandler(IMessageSink nextSink)
        {
            this.nextSink = nextSink;
        }

        //同步处理方法
        public IMessage SyncProcessMessage(IMessage msg)
        {
            IMessage retMsg = null;

            //方法调用消息接口
            IMethodCallMessage call = msg as IMethodCallMessage;

            //如果被调用的方法没打MyInterceptorMethodAttribute标签
            if (call == null || (Attribute.GetCustomAttribute(call.MethodBase, typeof(MyInterceptorMethodAttribute))) == null)
            {
                retMsg = nextSink.SyncProcessMessage(msg);
            }
            //如果打了MyInterceptorMethodAttribute标签
            else
            {
                MessageBox.Show("执行之前");
                retMsg = nextSink.SyncProcessMessage(msg);
                MessageBox.Show("执行之后");
            }

            return retMsg;
        }

        //异步处理方法（不需要）
        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
        {
            return null;
        }
    }
}
