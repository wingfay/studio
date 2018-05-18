using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz_demo
{
    public class ExamplesGo
    {
        static ISchedulerFactory schedFact = new StdSchedulerFactory();

        public static IScheduler sched =  schedFact.GetScheduler().GetAwaiter().GetResult();





        #region 1.运行任务【所有】
        /// <summary>
        /// 运行任务
        /// </summary>
        public void Run()
        {
            if (sched != null)
                sched.Start();
        }
        #endregion

        #region 2.添加job
        /// <summary>
        /// 添加job
        /// </summary>
        public void AddJob()
        {
            IJobDetail job = JobBuilder.Create<HelloJob>()
                            .WithIdentity("HelloJob", "group1")
                            .StoreDurably()
                            .Build();
            sched.AddJob(job, true);
        }
        #endregion

        #region 3.添加触发器
        /// <summary>
        /// 添加触发器
        /// </summary>
        /// <param name="job">需要添加触发器的job</param>
        public void AddTrigger(IJobDetail job)
        {
            ITrigger trigger = TriggerBuilder.Create()
                                          .WithIdentity("myTrigger", "group1")
                                          .WithCronSchedule("0/5 * * * * ?")     //5秒执行一次
                                          .ForJob(job)
                                          .Build();
            sched.ScheduleJob(trigger);
        }
        public void AddTrigger1(IJobDetail job)
        {
            ITrigger trigger = TriggerBuilder.Create()
                                          .WithIdentity("myTrigger2", "group1")
                                          .WithCronSchedule("0/1 * * * * ?")     //5秒执行一次
                                          .ForJob(job)
                                          .Build();
            sched.ScheduleJob(trigger);
        }
        #endregion

        #region 4.根据key返回Job
        /// <summary>
        /// 根据key返回Job
        /// </summary>
        /// <param name="jobName">需要查找的job名称</param>
        /// <returns></returns>
        public async Task<IJobDetail> GetJob(string jobName)
        {

            var keys = (await sched.GetJobKeys(GroupMatcher<JobKey>.AnyGroup()));

            JobKey jobKey = keys.Where(b => b.Name == jobName).FirstOrDefault();
            return await sched.GetJobDetail(jobKey);
        }
        #endregion

        #region 5.暂停任务[全部暂停]
        /// <summary>
        /// 暂停任务
        /// </summary>
        public void PauseAll()
        {
            if (sched != null)
                sched.PauseAll();
        }
        #endregion

        #region 6.暂停任务【单个任务】
        /// <summary>
        /// 暂停任务
        /// </summary>
        /// <param name="jobName">需要暂停任务的名称</param>
        public async void Shutdown(string jobName)
        {
            if (sched != null)
            {
                JobKey jobKey =(await sched.GetJobKeys(GroupMatcher<JobKey>.AnyGroup())).ToList().Where(b => b.Name == jobName).FirstOrDefault();
                await sched.PauseJob(jobKey);
            }

        }
        #endregion

        #region 7.恢复当前任务
        /// <summary>
        /// 恢复当前任务
        /// </summary>
        /// <param name="jobName">需要恢复任务的名称</param>
        public async void ResumeJob(string jobName)
        {
            if (sched != null)
            {
                JobKey jobKey = (await sched.GetJobKeys(GroupMatcher<JobKey>.AnyGroup())).ToList().Where(b => b.Name == jobName).FirstOrDefault();
                await sched.ResumeJob(jobKey);
            }

        }
        #endregion

        #region 8.恢复所有任务
        /// <summary>
        /// 恢复所有任务
        /// </summary>
        public void ResumeJobAll()
        {
            if (sched != null)
            {
                sched.ResumeAll();
            }

        }
        #endregion

        #region 9.删除当前任务
        /// <summary>
        /// 删除当前任务
        /// </summary>
        /// <param name="jobName">删除任务名称</param>
        public async void DeleteJob(string jobName)
        {
            if (sched != null)
            {
                JobKey jobKey = (await sched.GetJobKeys(GroupMatcher<JobKey>.AnyGroup())).ToList().Where(b => b.Name == jobName).FirstOrDefault();
                await sched.DeleteJob(jobKey);
            }
        }
        #endregion

        #region 10.删除所有任务
        /// <summary>
        /// 删除所有任务
        /// </summary>
        public void DeleteJobAll()
        {
            if (sched != null)
            {
                sched.Clear();
            }
        }
        #endregion

        #region 11.返回所有任务键
        /// <summary>
        /// 返回所有任务键
        /// </summary>
        /// <returns></returns>
        public async  Task<List<JobKey>> GetJobAll()
        {
            //获取所有job
            List<JobKey> jobKeys = (await sched.GetJobKeys(GroupMatcher<JobKey>.AnyGroup())).ToList();
            return jobKeys;
        }
        #endregion

        #region 12.返回所有的触发器
        /// <summary>
        /// 返回所有的触发器
        /// </summary>
        /// <returns></returns>
        public async Task<List<TriggerKey>> GetTrigAll()
        {
            List<TriggerKey> trigKeys = (await sched.GetTriggerKeys(GroupMatcher<TriggerKey>.AnyGroup())).ToList();
            return trigKeys;
        }
        #endregion

        #region 13.返回job对应的trigger
        /// <summary>
        /// 返回job对应的trigger
        /// </summary>
        /// <param name="jobName">任务名称</param>
        /// <returns></returns>
        public async Task<List<ITrigger>> GetJobToTrig(string jobName)
        {
            JobKey jobKey = (await sched.GetJobKeys(GroupMatcher<JobKey>.AnyGroup())).Where(b => b.Name == jobName).FirstOrDefault();
            List<ITrigger> triggerList = (await sched.GetTriggersOfJob(jobKey)).ToList();
            return triggerList;
        }
        #endregion

        #region 14.关闭所有任务
        /// <summary>
        /// 关闭所有任务
        /// </summary>
        public void Shutdown()
        {
            if (sched != null)
                sched.Shutdown();
        }
        #endregion

        #region 15.修改任务触发器
        public void ModiyTrig()
        {
            ITrigger trigger = TriggerBuilder.Create()
               .WithIdentity("myTrigger", "group1")
                // .StartAt(runTime)
                .WithCronSchedule("0/10 * * * * ?")     //10秒执行一次
               .Build();
            sched.RescheduleJob(trigger.Key, trigger);
            sched.Start();
        }
        #endregion
    }

    class HelloJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync($"3,现在时间：{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        }
    }

}
