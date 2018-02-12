using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm_Sudio.AOP
{
    /// <summary>
    /// 业务层的类和方法，让它继承自上下文绑定类的基类
    /// 参考说明 <url>http://www.cnblogs.com/ZengYunChun/p/5921575.html</url>
    /// </summary>
    [MyInterceptor]
    public class BusinessHandler : ContextBoundObject
    {
        [MyInterceptorMethod]
        public void DoSomething()
        {
            MessageBox.Show("执行了方法本身！");
        }
    }
}
