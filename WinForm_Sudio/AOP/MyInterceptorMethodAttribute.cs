using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForm_Sudio.AOP
{
    //贴在方法上的标签[MyInterceptorMethod]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class MyInterceptorMethodAttribute : Attribute
    { }
}
