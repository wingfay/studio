using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace WinForm_Sudio.AOP
{
    //贴在类上的标签
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class MyInterceptorAttribute : ContextAttribute, IContributeObjectSink
    {
        public MyInterceptorAttribute()
            : base("MyInterceptor")
        { }

        //实现IContributeObjectSink接口当中的消息接收器接口
        public IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink next)
        {
            return new MyAopHandler(next);
        }
    }
}
