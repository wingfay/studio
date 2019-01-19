using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Specialized;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Quartz_demo
{
    /// <summary>
    /// 官网地址：  https://www.quartz-scheduler.net/documentation/quartz-3.x/quick-start.html
    /// 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var task = RunProgram().GetAwaiter();

            var i =  task.GetResult();

            System.Console.WriteLine(i);

            Console.WriteLine("启动成功 ");
            Console.ReadKey();

        }


        private static async Task<int> RunProgram()
        {
            try
            {
                // Grab the Scheduler instance from the Factory  
                NameValueCollection props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };


                //创建一个标准调度器工厂
                ISchedulerFactory factory = new StdSchedulerFactory(props);
                //通过从标准调度器工厂获得一个调度器，用来启动任务
                IScheduler scheduler = await factory.GetScheduler();
                //调度器的线程开始执行，用以触发Trigger

                await scheduler.Start();


                //使用组别、名称创建一个工作明细，此处为所需要执行的任务
                IJobDetail detail1 = JobBuilder.Create<MyFirstJob>().WithIdentity("MyJob1", "MyGroup").Build();
                //通过使用UsingJobData添加传递到context(类型：IJobExecutionContext)的属性
                IJobDetail detail2 = JobBuilder.Create<MySecondJob>().WithIdentity("MyJob2", "MyGroup").UsingJobData("Title", "Hello World").
                    UsingJobData("Pi", Math.PI).UsingJobData("Cnxy", "http://www.cnc6.cn").Build();
                IJobDetail detail3 = JobBuilder.Create<MyThridJob>().WithIdentity("MyJob3", "MyGroup").Build();
                //使用组别、名称创建一个触发器，其中触发器立即执行，且每隔1秒或3秒执行一个任务，重复执行
                ITrigger trigger1 = TriggerBuilder
                    .Create()
                    .WithIdentity("MyTrigger1", "MyGroup")
                    .StartNow().WithSimpleSchedule(x => x.WithIntervalInSeconds(1)
                    .RepeatForever())
                    .Build();
                ITrigger trigger2 = TriggerBuilder.Create().WithIdentity("MyTrigger2", "MyGroup").StartNow().WithSimpleSchedule(x => x.WithIntervalInSeconds(3).RepeatForever()).Build();
                ITrigger trigger3 = TriggerBuilder.Create().WithIdentity("MyTrigger3", "MyGroup").StartNow().WithSimpleSchedule(x => x.WithIntervalInSeconds(2).RepeatForever()).Build();
                //开始执行使用指定的触发器运行执行的工作任务
                await scheduler.ScheduleJob(detail1, trigger1);
                await scheduler.ScheduleJob(detail2, trigger2);
                await scheduler.ScheduleJob(detail3, trigger3);

              

            }
            catch (SchedulerException se)
            {
                await Console.Error.WriteLineAsync(se.ToString());
            }

            return 0;
        }

    }
    //运行并发运行
    class MyFirstJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            //JobDetail.Key：标识JobDetail的唯一ID
            //Trigger.Key：标识Trigger的唯一ID


            await Console.Out.WriteLineAsync($"1,Date Now：{DateTime.Now:yyyy-MM-dd HH:mm:ss}，" +
                $"work Detail：{context.JobDetail.Key.Name}，work trigger：{context.Trigger.Key.Name}");

            
        }

        
    }
    //运行并发运行
    class MySecondJob : IJob
    {
        //获得值的方式：3、属性注入
        public string Cnxy { set; get; }

        public async Task Execute(IJobExecutionContext context)
        {
         //获得值的方式：1、通过IJobExecutionContext.JobDetail.JobDataMap["Key"]获得所对应的值
         string value1 = (string)context.JobDetail.JobDataMap["Title"];
            //获得值的方式：2、也可以通过IJobExecutionContext.MergedJobDataMap["Key"]获得所对应的值
            double value2 = (double)context.MergedJobDataMap["Pi"];

            await Console.Out.WriteLineAsync($"2,现在时间：{DateTime.Now:yyyy-MM-dd HH:mm:ss}，" +
                $"Title：{value1}，Pi：{value2}，CNXY：{Cnxy}");

        }
    }

    //不运行并发运行
    [DisallowConcurrentExecution]
    class MyThridJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync($"3,现在时间：{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            Thread.Sleep(3);
        }
    }
}
