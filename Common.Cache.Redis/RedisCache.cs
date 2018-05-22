using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.LegacyConfiguration;
using StackExchange.Redis.Extensions.Newtonsoft;
using System;
using System.Configuration;

namespace Common.Cache.Redis
{
    public class RedisCache
    {
        private static RedisConfiguration config =null;

        private static StackExchangeRedisCacheClient client;

        public static StackExchangeRedisCacheClient GetClient()
        {

            if(config==null)
            {
                config = RedisCachingSectionHandler.GetConfig();
            }



            client = new StackExchangeRedisCacheClient(new NewtonsoftSerializer(), config);

            return client;

        }

        #region -- Item --
        /// <summary>
        /// 设置单体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public static bool Set<T>(string key, T t)
        {
            try
            {


                using (var redis = GetClient())
                {
                    return redis.Add(key, t);
                }
            }
            catch (Exception ex)
            {
                
                return false;
            }
        }
        /// <summary>
        /// 设置单体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public static bool Set<T>(string key, T t, TimeSpan timeSpan) where T : class
        {
            try
            {

                using (var redis = GetClient())
                {
                    return redis.Add(key, t, timeSpan);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 设置单体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static bool Set<T>(string key, T t, DateTime dateTime)
        {
            try
            {
                using (var redis = GetClient())
                {
                    return redis.Add(key, t, dateTime);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 获取单体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            try
            {
                using (var redis = GetClient())
                {
                    return redis.Get<T>(key);
                }
            }
            catch (Exception ex)
            {

                return default(T);
            }
        }

        /// <summary>
        /// 移除单体
        /// </summary>
        /// <param name="key"></param>
        public static bool Remove(string key)
        {
            try
            {
                using (var redis = GetClient())
                {
                    return redis.Remove(key);
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 清空所有缓存
        /// </summary>
        public static void RemoveAll()
        {
            using (var redis = GetClient())
            {
                redis.FlushDb();
            }
        }
        #endregion

        #region hash
        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public static bool Hash_Set<T>(string key, string dataKey, T t)
        {
            using (var redis = GetClient())
            {

                return redis.HashSet(key, dataKey, t);
            }
        }



        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public static T Hash_Get<T>(string key, string dataKey)
        {
            using (var redis = GetClient())
            {
                return redis.HashGet<T>(key, dataKey);
            }
        }

        #endregion

    }
}
