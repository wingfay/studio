using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Win32
{
   public class Win32Api
   {
      public struct POINTAPI
      {
         public long x;
         public long y;
      }
      /*Public Declare Function WindowFromPoint Lib "user32" Alias "WindowFromPoint" (ByVal xPoint As Long, ByVal yPoint As Long) As Long
      Public Declare Function GetCursorPos Lib "user32" Alias "GetCursorPos" (lpPoint As POINTAPI) As Long
      Public Declare Function GetWindowText Lib "user32" Alias "GetWindowTextA" (ByVal hwnd As Long, ByVal lpString As String, ByVal cch As Long) As Long
      Public Declare Function GetParent Lib "user32" Alias "GetParent" (ByVal hwnd As Long) As Long
      Public Declare Function GetClassName Lib "user32" Alias "GetClassNameA" (ByVal hwnd As Long, ByVal lpClassName As String, ByVal nMaxCount As Long) As Long
      Public Declare Function ChildWindowFromPoint Lib "user32" Alias "ChildWindowFromPoint" (ByVal hWndParent As Long, ByVal pt As POINTAPI) As Long*/

      [DllImport("User32.dll")]
      public static extern long WindowFromPoint(long x, long y);

      [DllImport("User32.dll")]
      public static extern long GetCursorPos(ref POINTAPI p);

      [DllImport("User32.dll")]
      public static extern long GetWindowTextA(long hwnd, ref string lpString, long cch);

      [DllImport("User32.dll")]
      public static extern long GetParent(long hwnd);

      [DllImport("User32.dll")]
      public static extern long ChildWindowFromPoint(long hWndParent, long x, long y);

      [DllImport("User32.dll")]
      public static extern long GetclassNameA(long hwnd, string lpClassName, long nMaxCount);

      [DllImport("user32.dll", CharSet = CharSet.Auto)]
      public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

      [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
      public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

      [DllImport("user32.dll")]
      public static extern IntPtr LoadIcon(IntPtr hInstance, IntPtr lpIconName);

      [DllImport("user32.dll", EntryPoint = "GetClassLong")]
      public static extern uint GetClassLongPtr32(IntPtr hWnd, int nIndex);

      [DllImport("user32.dll", EntryPoint = "GetClassLongPtr")]
      public static extern IntPtr GetClassLongPtr64(IntPtr hWnd, int nIndex);

      [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
      public static extern int GetWindowThreadProcessId(IntPtr handle, out uint processId);

      [DllImport("user32.Dll")]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool EnumChildWindows(IntPtr parentHandle, EnumWindowProc callback, IntPtr lParam);




      public delegate bool EnumDelegate(IntPtr hWnd, int lParam);
      public delegate bool EnumWindowProc(IntPtr hwnd, IntPtr lParam);

      public static List<IntPtr> GetChildWindows(IntPtr parent)
      {
         List<IntPtr> result = new List<IntPtr>();
         GCHandle listHandle = GCHandle.Alloc(result);
         try
         {
            Win32Api.EnumWindowProc childProc = new Win32Api.EnumWindowProc(EnumWindow);
            Win32Api.EnumChildWindows(parent, childProc, GCHandle.ToIntPtr(listHandle));
         }
         finally
         {
            if (listHandle.IsAllocated)
               listHandle.Free();
         }
         return result;
      }

      public static bool EnumWindow(IntPtr handle, IntPtr pointer)
      {
         GCHandle gch = GCHandle.FromIntPtr(pointer);
         List<IntPtr> list = gch.Target as List<IntPtr>;
         if (list == null)
         {
            throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");
         }
         list.Add(handle);
         //  You can modify this to check to see if you want to cancel the operation, then return a null here
         return true;
      }

      public static IntPtr GetClassLongPtr(IntPtr hWnd, int nIndex)
      {
         try
         {
            if (IntPtr.Size == 4)
               return new IntPtr(GetClassLongPtr32(hWnd, nIndex));
            else
               return GetClassLongPtr64(hWnd, nIndex);
         }
         catch (Exception ex)
         {
            return IntPtr.Zero;
         }
      }

      public static Process GetModernAppProcessPath(IntPtr hwnd)
      {
         uint pid = 0;
         GetWindowThreadProcessId(hwnd, out pid);
         // now this is a bit tricky. Modern apps are hosted inside ApplicationFrameHost process, so we need to find
         // child window which does NOT belong to this process. This should be the process we need
         var children = GetChildWindows(hwnd);
         foreach (var childHwnd in children)
         {
            uint childPid = 0;
            GetWindowThreadProcessId(childHwnd, out childPid);
            if (childPid != pid)
            {
               // here we are
               return Process.GetProcessById((int)childPid);
            }
         }
         return null;
         //throw new Exception("Cannot find a path to Modern App executable file");
      }
   }
}
