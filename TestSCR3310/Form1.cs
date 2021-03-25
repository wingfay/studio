using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestSCR3310
{
   public partial class Form1 : Form
   {
      public Form1()
      {
         InitializeComponent();
      }
   }


   /// <summary>
	/// SCOPE context
	/// </summary>
	public enum SCOPE
   {
      /// <summary>
      /// The context is a user context, and any database operations are performed within the
      /// domain of the user.
      /// </summary>
      User,

      /// <summary>
      /// The context is that of the current terminal, and any database operations are performed
      /// within the domain of that terminal.  (The calling application must have appropriate
      /// access permissions for any database actions.)
      /// </summary>
      Terminal,

      /// <summary>
      /// The context is the system context, and any database operations are performed within the
      /// domain of the system.  (The calling application must have appropriate access
      /// permissions for any database actions.)
      /// </summary>
      System
   }

   public enum PROTOCOL
   {
      /// <summary>
      /// There is no active protocol.
      /// </summary>
      Undefined = 0x00000000,

      /// <summary>
      /// T=0 is the active protocol.
      /// </summary>
      T0 = 0x00000001,

      /// <summary>
      /// T=1 is the active protocol.
      /// </summary>
      T1 = 0x00000002,

      /// <summary>
      /// Raw is the active protocol.
      /// </summary>
      Raw = 0x00000004,
      Default = unchecked((int)0x80000000),  // Use implicit PTS.

      /// <summary>
      /// T=1 or T=0 can be the active protocol
      /// </summary>
      T0orT1 = T0 | T1
   }



}
