using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
   public class Win32Test
   {



      public struct KBDLLHOOKSTRUCT
      {
         public int vkCode;
         public int scanCode;
         public int flags;
         public int time;
         public int dwExtraInfo;
      }
      public const int WM_KEYDOWN = 0x0100;
      public const int WH_KEYBOARD_LL = 13;
      public const int WM_SYSKEYDOWN = 0x0104;

      public delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);

      [DllImport("USER32.DLL")]
      public static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);


      [DllImport("user32.dll", CharSet = CharSet.Auto)]
      private static extern int GetWindowTextLength(IntPtr hWnd);

      [DllImport("user32.dll", SetLastError = true)]
      public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

      [DllImport("user32.dll")]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, int lParam);

      [DllImport("USER32.DLL")]
      public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

      [DllImport("User32.dll", CharSet = CharSet.Auto, EntryPoint = "GetWindowText")]
      public static extern int GetWindowText(IntPtr hwnd, StringBuilder lpString, int cch);

      [DllImport("User32.dll", CharSet = CharSet.Auto, EntryPoint = "GetWindowTextA")]
      public static extern int GetWindowTextA(IntPtr hwnd, out StringBuilder lpString, int cch);

      [DllImport("user32.dll", EntryPoint = "SendMessageA")]
      private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, StringBuilder lParam);

      [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
      public static extern bool SendMessage(IntPtr hWnd, uint Msg, int wParam, StringBuilder lParam);

      [DllImport("user32.dll", SetLastError = true)]
      public static extern IntPtr SendMessage(int hWnd, int Msg, int wparam, int lparam);

      [DllImport("user32.dll", SetLastError = true)]
      public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

      const int WM_GETTEXT = 0x000D;
      const int WM_GETTEXTLENGTH = 0x000E;


      [DllImport("user32.dll", SetLastError = true)]
      static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);


      #region hook 

      public  delegate int HookHandlerDelegate(int nCode, IntPtr wparam, ref KBDLLHOOKSTRUCT lparam);

      [DllImport("kernel32.dll")]
      static extern int GetCurrentThreadId(); //取得当前线程编号的API

      [DllImport("User32.dll")]

      internal extern static bool UnhookWindowsHookEx(IntPtr handle); //取消Hook的API



      [DllImport("User32.dll")]

      internal extern static IntPtr SetWindowsHookEx(int idHook, [MarshalAs(UnmanagedType.FunctionPtr)] HookHandlerDelegate lpfn, IntPtr hinstance, int threadID); //设置Hook的API



      [DllImport("User32.dll")]

      internal extern static IntPtr CallNextHookEx(IntPtr handle, int code, IntPtr wparam, IntPtr lparam); //取得下一个Hook的API 


      // 获取模块句柄
      [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
      public static extern IntPtr GetModuleHandle(String modulename);


      #endregion

      public static string GetControlText(IntPtr hWnd)
      {

         // Get the size of the string required to hold the window title (including trailing null.) 
         Int32 titleSize = SendMessage((int)hWnd, WM_GETTEXTLENGTH, 0, 0).ToInt32();

         // If titleSize is 0, there is no title so return an empty string (or null)
         if (titleSize == 0)
            return String.Empty;

         StringBuilder title = new StringBuilder(titleSize + 1);

         SendMessage(hWnd, (int)WM_GETTEXT, title.Capacity, title);

         return title.ToString();
      }



      public static Stack<string> matchWord2013PrintUIObjects = new Stack<string>(
                       new string[] { "RICHEDIT60W", "NetUICtrlNotifySink", "NetUIHWND", "NUIScrollbar", "_WwG", "NetUICtrlNotifySink", "RICHEDIT60W",
                          "NetUICtrlNotifySink", "RICHEDIT60W", "NetUICtrlNotifySink", "NetUIHWND", "FullpageUIHost" });


      public class UIObject
		{
         public UIObject(string className) : this(className, false, false)
         {

         }

         public UIObject(string className, bool needGetTextValue):this(className, needGetTextValue, false)
			{

			}

         public UIObject(string className,bool needGetTextValue,bool needGetCaption)
			{
            this.ClassName = className;
            this.NeedGetTextValue = needGetTextValue;
            this.NeedGetCaption = needGetCaption;
			}

         public string ClassName { get; set; }



         public bool NeedGetTextValue { get; set; }

         public bool NeedGetCaption { get; set; }
      }

      public static Stack<UIObject> GetOfficeWord2013Ui()
		{
         return new Stack<UIObject>(new UIObject[] { new UIObject("RICHEDIT60W"), 
            new UIObject("NetUICtrlNotifySink"),
         new UIObject("NetUIHWND"),
         new UIObject("NUIScrollbar"),
         new UIObject("_WwG"),
         new UIObject("NetUICtrlNotifySink"),
         new UIObject("RICHEDIT60W"),
         new UIObject("NetUICtrlNotifySink"),
         new UIObject("RICHEDIT60W",true),
         new UIObject("NetUICtrlNotifySink"),
         new UIObject("NetUIHWND"),
         new UIObject("FullpageUIHost")} );
		}

      /// <summary>
      /// RECURSIVE
      /// This function will take the window classnames in the provided stack
      /// and then find each one in order via recursive calls. If all of them
      /// are found - we return true = found
      /// </summary>
      /// <param name="PintHandle"></param>
      /// <param name="PobjStack"></param>
      /// <returns></returns>
      private static bool FindWindowStack(IntPtr PintHandle, Stack<string> PobjStack)
      {
         try
         {
            // get the window with the classname being popped off the stack
            IntPtr LintNewHandle = FindWindowEx(PintHandle, IntPtr.Zero, PobjStack.Pop(), "");


            if (LintNewHandle != IntPtr.Zero && PobjStack.Count == 0)
            {
               return true; // found it
            }
            else if (LintNewHandle != IntPtr.Zero)
            {
               string apppName = GetWindowClass(LintNewHandle);
               Console.WriteLine($"apppName:{apppName}");

               // found a window, but the stack still has items, call next one
               return FindWindowStack(LintNewHandle, PobjStack);
            }
            else
            {
               // did not find it
               return false;
            }
         }
         catch
         {
            // oops
            return false;
         }
      }

      public static string GetWindowClass(IntPtr hWnd)
      {
         int length = 255;
         StringBuilder builder = new StringBuilder(length);
         GetClassName(hWnd, builder, length + 1);
         return builder.ToString();
      }
      /// <summary> Get the text for the window pointed to by hWnd </summary>
      public static string GetWindowText(IntPtr hWnd)
      {
         int size = GetWindowTextLength(hWnd);
         if (size > 0)
         {
            var builder = new StringBuilder(size + 1);
            GetWindowText(hWnd, builder, builder.Capacity);
            return builder.ToString();
         }

         return String.Empty;
      }


      public static string GetRichTextBoxText(IntPtr hWnd)
      {
         int size = GetWindowTextLength(hWnd);
         if (size > 0)
         {
            var builder = new StringBuilder(size + 1);
            GetWindowText(hWnd, builder, builder.Capacity);
            return builder.ToString();
         }

         return String.Empty;
      }



      public static IntPtr Find_Word_FULLPAGEUIHOST()
		{
         //RichTextBoxHelper richTextBoxHelper = new RichTextBoxHelper();

         IntPtr FULLPAGEUIHOST = IntPtr.Zero;


         int i = 0;
         bool found= EnumWindows(delegate (IntPtr wnd, int _)
         {

            string ClassName = GetWindowClass(wnd);

            if (ClassName == "OpusApp")
            {
               string cap = GetWindowText(wnd);
               Console.WriteLine($"{++i} apppName:{cap}");

               bool Result = EnumChildWindows(wnd, (wnd1, __) =>
               {

                  string apppName = GetWindowClass(wnd1);

                  if (apppName.ToUpper() == "FULLPAGEUIHOST")
                  {
                     FULLPAGEUIHOST = wnd1;

							//Console.WriteLine($"{++i} apppName:{apppName}");
							return false;
                     //EnumChildWindows(wnd1, (wnd2, ___) =>
                     //{

                     //   if (apppName.ToUpper() == "RICHEDIT60W")
                     //   {
                     //      string cap = GetWindowText(wnd1);

                     //      if (string.IsNullOrEmpty(cap))
                     //      {
                     //         cap = GetControlText(wnd1);
                     //      }

                     //      Console.WriteLine($"{++i} apppName:{apppName} Value:{cap}");
                     //   }
                     //   else if (apppName.ToUpper() == "NetUIHWND")
                     //   {
                     //      var icon = Win32.IconHelper.GetAppIcon(wnd1);

                     //      if (icon != null)
                     //      {
                     //         Console.WriteLine($"{++i} apppName:{apppName} ICON:1");
                     //      }
                     //      else
                     //      {
                     //         Console.WriteLine($"{++i} apppName:{apppName}");
                     //      }
                     //   }

                     //   else
                     //   {
                     //      Console.WriteLine($"{++i} apppName:{apppName}");
                     //   }

                     //   return true;
                     //}, 0);

                  }




                  return true;

               }, 0);


       
              // Console.WriteLine($"{++i} result:{Result}");

               if (FULLPAGEUIHOST != IntPtr.Zero)
               {
                  string apppName = GetWindowClass(FULLPAGEUIHOST);

                 //  Console.WriteLine($"{++i} apppName:{apppName}");

                  return false;
               }
            }

            return true;

         }, 0);

         return FULLPAGEUIHOST;


      }


      public static object DeepClone(object obj)
      {
         object objResult = null;
         using (MemoryStream ms = new MemoryStream())
         {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, obj);

            ms.Position = 0;
            objResult = bf.Deserialize(ms);
         }
         return objResult;
      }

      public static bool FindWindowStackEx(IntPtr parentWnd,Stack<UIObject> PobjStack,out int copies)
		{
         bool Founded = false;

         copies = 1;

         List<IntPtr> childs = new List<IntPtr>();

         bool Result = EnumChildWindows(parentWnd, (wnd1, __) =>
         {

            childs.Add(wnd1);

            return true;

         }, 0);


         

			foreach (IntPtr item in childs)
			{
				// Console.WriteLine($"child:{item}");

				if (PobjStack.Count == 0)
				{
              // Console.WriteLine($"count:0");
               return true;
				}

            string className = GetWindowClass(item);

            UIObject uIObject = PobjStack.Pop();

            if (className != uIObject.ClassName)
				{

               return false;
				}
				else
				{
					if (uIObject.NeedGetTextValue)
					{
                  var text = GetControlText(item);

                  if (int.TryParse(text,out int result))
						{
                     copies = result;
						}
               }
				}
         }


    


         return Founded;
      }



      private static int HookCallback(int nCode, IntPtr wparam, ref KBDLLHOOKSTRUCT lparam)
      {
         if (
             nCode >= 0
             &&
             (wparam == (IntPtr)WM_KEYDOWN
             ||
             wparam == (IntPtr)WM_SYSKEYDOWN)
             )
         {

            //Console.WriteLine(lparam.vkCode);
            if (lparam.vkCode == 13)
            {
               Console.WriteLine("按下回车键");
               return 0;
            }
            else if (lparam.vkCode == 8)
            {
               Console.WriteLine("按下backspace键");
               return 0;
            }
            else if (lparam.vkCode == 20)
            {
               Console.WriteLine("按下大小写切换键");
               return 0;
            }
            else if (lparam.vkCode == 160 || lparam.vkCode == 161)
            {
               Console.WriteLine("按下shift键");
               return 0;
            }
            else if (lparam.vkCode == 162)
            {
               Console.WriteLine("按下ctrl键");
               return 0;
            }
            else if (lparam.vkCode == 190)
            {
               Console.Write(".");
               return 0;
            }
            byte[] array = new byte[1];
            array[0] = (byte)(Convert.ToInt32(lparam.vkCode)); //ASCII码强制转换二进制
            Console.Write(Convert.ToString(System.Text.Encoding.UTF8.GetString(array)).ToLower());
         }
         return 0;
      }


      //钩子回掉委托实例
      private static HookHandlerDelegate proc;

      //钩子句柄
      public static IntPtr HWND_KeyborardHook = IntPtr.Zero;

      public static bool HookStart(int processId,IntPtr word)
		{
         if (HWND_KeyborardHook != IntPtr.Zero)
            return true;

         // 创建HookProc实例 
         proc = new HookHandlerDelegate(HookCallback);
         using (Process curPro = Process.GetProcessById(processId))
         using (ProcessModule curMod = curPro.MainModule)
         {
            //定义全局钩子 
            HWND_KeyborardHook = SetWindowsHookEx(WH_KEYBOARD_LL, proc, word, 0);

            Console.WriteLine($"HookStart");

            

            if (HWND_KeyborardHook == IntPtr.Zero)
            {
               Console.WriteLine($"hook config fail,Marshal.GetLastWin32Error():{Marshal.GetLastWin32Error()} ");

               HookStop();

               

              

               return false;
               //throw new Exception("钩子设置失败");
            }
				else
				{
               return true;
            }

         }

        


      }

      public static void HookStop()
      {
         bool retKeyboard = true;
         if (HWND_KeyborardHook != IntPtr.Zero)
         {
            retKeyboard = UnhookWindowsHookEx(HWND_KeyborardHook);
            HWND_KeyborardHook = IntPtr.Zero;
         }


         if (!retKeyboard) Console.WriteLine($"stop hook fail");// throw new Exception("卸载钩子失败");

      }
     

      public static void GetWindow()
      {
         Console.WriteLine($"{DateTime.Now}");

         IntPtr wnd = Find_Word_FULLPAGEUIHOST();

         int i = 0;

         if (wnd != IntPtr.Zero)
			{
            string apppName = GetWindowClass(wnd);
            Console.WriteLine($"root apppName:{apppName}");

            EnumChildWindows(wnd, (wnd1, ___) =>
				{

               apppName = GetWindowClass(wnd1);

               if (apppName.ToUpper() == "RICHEDIT60W")
					{
						string cap = GetWindowText(wnd1);

						if (string.IsNullOrEmpty(cap))
						{
							cap = GetControlText(wnd1);
						}

						Console.WriteLine($"{++i} apppName:{apppName} Value:{cap}");
					}
					else if (apppName.ToUpper() == "NetUIHWND")
					{
						var icon = Win32.IconHelper.GetAppIcon(wnd1);

						if (icon != null)
						{
							Console.WriteLine($"{++i} apppName:{apppName} ICON:1");
						}
						else
						{
							Console.WriteLine($"{++i} apppName:{apppName}");
						}
					}

					else
					{
						Console.WriteLine($"{++i} apppName:{apppName}");
					}

					return true;
				}, 0);
			}
			else
			{

			}

      }




      #region Match word 2013 print page 

      public static List<IntPtr> Find_Word2013()
      {
         List<IntPtr> wordApps = new List<IntPtr>();


         EnumWindows(delegate (IntPtr wnd, int _)
         {

            string ClassName = GetWindowClass(wnd);

            if (ClassName == "OpusApp")
            {
               wordApps.Add(wnd);

            }

            return true;

         }, 0);

         return wordApps;

      }

      public static void MatchWord2013PrintPage()
      {
         List<IntPtr> matchPrintPageRoots = new List<IntPtr>();

         var wordapps = Find_Word2013();

         foreach (var item in wordapps)
         {
            string apppName = GetWindowClass(item);
            string cap = GetWindowText(item);
            Console.WriteLine($"apppName:{apppName} {cap}");

            

            if (FindWindowStackEx(item, GetOfficeWord2013Ui(),out int copies))
            {
               matchPrintPageRoots.Add(item);

               uint lpdwProcessId = 0;

               //GetWindowThreadProcessId(item, out lpdwProcessId);

               Console.WriteLine($"apppName:{apppName} {cap} copies count:{copies}  ThreadProcessId:[{lpdwProcessId}] show the Print tab");

					//if (textBox == IntPtr.Zero)
					//{
     //             Console.WriteLine($"textBox is IntPtr.Zero");
     //          }
					//else
					//{
     //             if (HookStart((int)lpdwProcessId, item))
     //             {
     //                System.Threading.Timer _timer = new System.Threading.Timer((_) => { HookStop(); }, null, 120000, 100000);
     //             }
     //          }





              



            }


         }

      }

      #endregion



   }

	public static class RichEditMessages
   {
      public const int WM_CONTEXTMENU = 123;
      public const int WM_UNICHAR = 265;
      public const int WM_PRINTCLIENT = 792;
      public const int EM_GETLIMITTEXT = 1061;
      public const int EM_POSFROMCHAR = 1062;
      public const int EM_CHARFROMPOS = 1063;
      public const int EM_SCROLLCARET = 1073;
      public const int EM_CANPASTE = 1074;
      public const int EM_DISPLAYBAND = 1075;
      public const int EM_EXGETSEL = 1076;
      public const int EM_EXLIMITTEXT = 1077;
      public const int EM_EXLINEFROMCHAR = 1078;
      public const int EM_EXSETSEL = 1079;
      public const int EM_FINDTEXT = 1080;
      public const int EM_FORMATRANGE = 1081;
      public const int EM_GETCHARFORMAT = 1082;
      public const int EM_GETEVENTMASK = 1083;
      public const int EM_GETOLEINTERFACE = 1084;
      public const int EM_GETPARAFORMAT = 1085;
      public const int EM_GETSELTEXT = 1086;
      public const int EM_HIDESELECTION = 1087;
      public const int EM_PASTESPECIAL = 1088;
      public const int EM_REQUESTRESIZE = 1089;
      public const int EM_SELECTIONTYPE = 1090;
      public const int EM_SETBKGNDCOLOR = 1091;
      public const int EM_SETCHARFORMAT = 1092;
      public const int EM_SETEVENTMASK = 1093;
      public const int EM_SETOLECALLBACK = 1094;
      public const int EM_SETPARAFORMAT = 1095;
      public const int EM_SETTARGETDEVICE = 1096;
      public const int EM_STREAMIN = 1097;
      public const int EM_STREAMOUT = 1098;
      public const int EM_GETTEXTRANGE = 1099;
      public const int EM_FINDWORDBREAK = 1100;
      public const int EM_SETOPTIONS = 1101;
      public const int EM_GETOPTIONS = 1102;
      public const int EM_FINDTEXTEX = 1103;
      public const int EM_GETWORDBREAKPROCEX = 1104;
      public const int EM_SETWORDBREAKPROCEX = 1105;
      public const int EM_SETUNDOLIMIT = 1106;
      public const int EM_REDO = 1108;
      public const int EM_CANREDO = 1109;
      public const int EM_GETUNDONAME = 1110;
      public const int EM_GETREDONAME = 1111;
      public const int EM_STOPGROUPTYPING = 1112;
      public const int EM_SETTEXTMODE = 1113;
      public const int EM_GETTEXTMODE = 1114;
   }


 //  public class RichTextBoxHelper
	//{
 //     public string ReadRTF(IntPtr handle)
 //     {
 //        string result = String.Empty;
 //        using (MemoryStream stream = new MemoryStream())
 //        {
 //           EDITSTREAM editStream = new EDITSTREAM();
 //           editStream.pfnCallback = new EditStreamCallback(EditStreamProc);
 //           editStream.dwCookie = stream;

 //           SendMessage(new HandleRef(this,handle), EM_STREAMIN, SF_RTF, ref editStream);

 //           stream.Seek(0, SeekOrigin.Begin);
 //           using (StreamReader reader = new StreamReader(stream))
 //           {
 //              result = reader.ReadToEnd();
 //           }
 //        }
 //        return result;
 //     }

 //     private int EditStreamProc(MemoryStream dwCookie, IntPtr pbBuff, int cb, out int pcb)
 //     {
 //        pcb = cb;
 //        byte[] buffer = new byte[cb];
 //        Marshal.Copy(pbBuff, buffer, 0, cb);
 //        dwCookie.Write(buffer, 0, cb);
 //        return 0;
 //     }
 //     public delegate int EditStreamCallback(IntPtr dwCookie, IntPtr buf, int cb, out int transferred);


 //    // private delegate int EditStreamCallback(MemoryStream dwCookie, IntPtr pbBuff, int cb, out int pcb);


 //     [StructLayout(LayoutKind.Sequential)]
 //     public class GETTEXTLENGTHEX
 //     {                               // Taken from richedit.h:
 //        public uint flags;          // Flags (see GTL_XXX defines)              
 //        public uint codepage;       // Code page for translation (CP_ACP for default, 1200 for Unicode)                         
 //     }

 //     [StructLayout(LayoutKind.Sequential)]
 //     public class EDITSTREAM
 //     {
 //        public IntPtr dwCookie = IntPtr.Zero;
 //        public int dwError = 0;
 //        public EditStreamCallback pfnCallback = null;
 //     }


 //     [StructLayout(LayoutKind.Sequential)]
 //     public class EDITSTREAM64
 //     {
 //        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
 //        public byte[] contents = new byte[20];
 //     }

 //     //[StructLayout(LayoutKind.Sequential)]
 //     //private class EDITSTREAM
 //     //{
 //     //   public MemoryStream dwCookie;
 //     //   public int dwError;
 //     //   public EditStreamCallback pfnCallback;
 //     //}

 //     [DllImport("user32.dll", CharSet = CharSet.Auto)]
 //     private static extern IntPtr SendMessage(HandleRef hwnd, uint msg, uint wParam, ref EDITSTREAM lParam);

 //     public const int WM_USER = 0x0400;
 //     public const int EM_STREAMIN = (WM_USER + 73);
 //     public const int EM_STREAMOUT = WM_USER + 74;
 //     public const int SF_RTF = 2;


 //     private unsafe EDITSTREAM64 ConvertToEDITSTREAM64(EDITSTREAM es)
 //     {
 //        EDITSTREAM64 es64 = new EDITSTREAM64();

 //        fixed (byte* es64p = &es64.contents[0])
 //        {
 //           byte* bp;
 //           long l;

 //           /*
 //           l = (long) es.dwCookie;
 //           bp = (byte *) &l;
 //           for (int i=0; i < sizeof(long); i++) {
 //               es64.contents[i] = bp[i];
 //           }*/
 //           *((long*)es64p) = (long)es.dwCookie;
 //           /*
 //           int il = es.dwError;
 //           bp = (byte *) &il;
 //           for (int i=0; i < sizeof(int); i++) {
 //               es64.contents[i+8] = bp[i];
 //           }*/
 //           *((int*)(es64p + 8)) = es.dwError;

 //           l = (long)Marshal.GetFunctionPointerForDelegate(es.pfnCallback);
 //           bp = (byte*)&l;
 //           for (int i = 0; i < sizeof(long); i++)
 //           {
 //              es64.contents[i + 12] = bp[i];
 //           }
 //           //*((long *)(es64p + 12)) = (long) Marshal.GetFunctionPointerForDelegate(es.pfnCallback);
 //        }

 //        return es64;
 //     }


 //  }
}
