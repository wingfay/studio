using System;
using System.Collections.Generic;
using System.Text;

namespace ZK.NetCore.ConsoleApp
{

   /// <summary>
   /// 三个类用来测试继承和初始化的顺序状态
   /// </summary>
   public abstract class One
   {
      public One()
      {
         Init();
      }

      public One(bool InitializeDocument)
      {
         Init(InitializeDocument);
      }


      protected virtual void Init(bool InitializeDocument = false)
      {
         if (InitializeDocument)
         {
            System.Console.WriteLine("InitializeDocument is true");
         }
         else
         {
            System.Console.WriteLine("InitializeDocument is false");
         }
      }
   }

   public class Two : One
   {
      public Two()
      {
         Init();
      }



      public Two(string _MarcLeader, string _MarcDirectory, string _MarcData, string CUSTOMSETTING_CallNoTag = null)
      {

         Init(_MarcLeader, _MarcDirectory, _MarcData, CUSTOMSETTING_CallNoTag);
      }

      public Two(string MarcString, string CUSTOMSETTING_CallNoTag = null)
      {

         Init(MarcString, CUSTOMSETTING_CallNoTag);
      }

      protected virtual void Init()
      {
         Console.WriteLine("Two Init()");
      }

      protected void Init(string _MarcLeader, string _MarcDirectory, string _MarcData, string CUSTOMSETTING_CallNoTag = null, bool unFixFrenchChar = false)
      {
         Init();
         Console.WriteLine("Two Init(5 param)");
      }

      protected void Init(string MarcString, string CUSTOMSETTING_CallNoTag = null, bool unFixFrenchChar = false)
      {

         Console.WriteLine("Two Init(3 param)");
      }
   }


   public class Three : Two
   {
      public int ID { get; set; }
      public Three()
      {

      }


      public Three(string _MarcLeader, string _MarcDirectory, string _MarcData, string CUSTOMSETTING_CallNoTag = null)
         : base(_MarcLeader, _MarcDirectory, _MarcData, CUSTOMSETTING_CallNoTag)
      {

      }

      public Three(string MarcString, string CUSTOMSETTING_CallNoTag = null)
         : base(MarcString, CUSTOMSETTING_CallNoTag)
      {


      }


      public Three(string MarcString)
      {
         Init(MarcString);

      }

      public Three(int ID)
      {
         this.ID = ID;
      }

      protected override void Init()
      {
         base.Init();
         Console.WriteLine("Three Init()");
         this.ID = 0;
      }

      protected void Init(string MarcString)
      {

         Console.WriteLine("Three Init( 1 Param)");
      }
   }


   public class TestOneTwoThree
   {
      public static void Run()
      {

         var three = new Three();

         Console.WriteLine("*******************************");

         var three1 = new Three("111", "1111111111");

         Console.WriteLine("*******************************");

         var three2 = new Three("1111", "2222", "333", "2222");

         Console.WriteLine("*******************************");


         var three3 = new Three("1111");

         Console.WriteLine("*******************************");

         var three4 = new Three(1111);


         Console.WriteLine(three4.ID);
         Console.WriteLine("*******************************");
      }
   }

}
