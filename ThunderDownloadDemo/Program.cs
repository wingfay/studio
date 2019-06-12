using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThunderDownloadDemo
{
   /// <summary>
   /// 调用迅雷下载程序demo 
   /// 我导入的dll是 是我下载运行中的迅雷 ThunderAgent 1.0 Type Library
   /// 
   /// 在com里导入这个dll后， 右键这个dll选择属性， 修改 Embed Interop Types 为false 
   /// project改成X86 ，重新编译一下
   /// 
   /// 设置里打开 静默下载 可以不回提示新建窗口
   /// </summary>
   class Program
   {
      static void Main(string[] args)
      {
         AddTask("http://www.baidu.com/index.html", "test.html", "C:\\", "", "", 1, 0, 5);
         

      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="pURL">目标URL，必须参数</param>
      /// <param name="pFileName">存名称，默认为空，表示由迅雷处理，可选参数</param>
      /// <param name="pPath">存储目录，默认为空，表示由迅雷处理，可选参数</param>
      /// <param name="pComments">下载注释，默认为空，可选参数</param>
      /// <param name="pReferURL">引用页URL，默认为空，可选参数</param>
      /// <param name="nStartMode">开始模式，0手工开始，1立即开始，默认为 - 1，表示由迅雷处理，可选参数</param>
      /// <param name="nOnlyFromOrigin">是否只从原始URL下载，1只从原始URL下载，0多资源下载，默认为0，可选参数</param>
      /// <param name="nOriginThreadCount">原始地址下载线程数，范围1 - 10，默认为 - 1，表示由迅雷处理，可选参数</param>
      static void AddTask(string pURL, string pFileName, string pPath, string pComments, string pReferURL, int nStartMode, int nOnlyFromOrigin, int nOriginThreadCount)
      {
         ThunderAgentLib.AgentClass agentClass = new ThunderAgentLib.AgentClass();

         //添加任务：下载http://www.baidu.com/index.html这个文件至C:\baidu.html，
         //没有注释，没有引用，立即开始，从多资源下载，原始资源线程5
         agentClass.AddTask(pURL, pFileName, pPath, pComments, pReferURL, nStartMode, nOnlyFromOrigin, nOriginThreadCount);
         agentClass.CommitTasks2(1);//提交
      }
   }
}
