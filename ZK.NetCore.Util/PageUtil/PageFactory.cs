

using System.Configuration;

namespace ZK.NetCore.Util.Page
{
    public class PageFactory
    {
        /// <summary>
        /// 默认SQLPage 方式
        /// </summary>
        public static SqlDialect defaultDialect { get; set; } = SqlDialect.MsSql2008;


        private static string SQLDialect = null;


        public static IPage Create()
        {
            if(string.IsNullOrEmpty(SQLDialect))
            {
                SQLDialect = ConfigurationManager.AppSettings["DATABASE:SqlDialect"];

                if (!string.IsNullOrEmpty(SQLDialect))
                {
                    defaultDialect = SQLDialect == "2008" ? SqlDialect.MsSql2008:SqlDialect.MsSql2012;
                }
            }



            if (defaultDialect == SqlDialect.MsSql2012)
            {
                return new Page2012();
            }

            return new Page2008();
        }
    }

    public enum SqlDialect
    {
        /// <summary>
        /// MS SQL Server
        /// </summary>
        MsSql2008,

        /// <summary>
        /// MS SQL Server
        /// </summary>
        MsSql2012,

        /// <summary>
        /// MySql
        /// </summary>
        MySql,

        /// <summary>
        /// SQLite
        /// </summary>
        SqLite,

        /// <summary>
        /// PostgreSql
        /// </summary>
        PostgreSql
    }
}
