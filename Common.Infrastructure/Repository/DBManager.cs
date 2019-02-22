using Common;
using Common.Infrastructure.Interface;
using Common.Infrastructure.Log;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Repository
{
    public class DBManager
    {
        //TODO 改为配置的方式
        public static string ConnectionString = "server=193.112.41.35;Database=rongfang1;Uid=rongfang;Pwd=rf654321";
        public static string ConnectionString2 = "server=193.112.41.35;Database=rongfang1;Uid=rongfang;Pwd=rf654321";
        public static string isLocal = "1";
        public static SqlSugarClient GetInstance()
        {
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = ConnectionString,
                DbType = DbType.MySql,
                IsAutoCloseConnection = true,
                IsShardSameThread = true, //设为true相同线程是同一个SqlConnection
                //SlaveConnectionConfigs = new List<SlaveConnectionConfig>()
                //{
                //    new SlaveConnectionConfig(){ HitRate=10,ConnectionString=Config.ConnectionString2 }
                //}
            });
            db.Ado.IsEnableLogEvent = true;

            if (Convert.ToBoolean(isLocal))
            {
                db.Aop.OnLogExecuting = (sql, pars) =>//SQL执行前事件
                {
                    LogHelper.Log("【Sql】:" + sql, "sql.txt");
                    LogHelper.Log("【Sql】【ConnectionString】:" + db.Ado.Connection.ConnectionString, "sql.txt");
                };
                //db.Aop.OnLogExecuted = (sql, pars) => //SQL执行完事件
                //{
                //    LogHelper.Log("【Sql】:" + sql, "sql.txt");
                //};
            }

            db.Aop.OnError = (exp) =>//执行SQL 错误事件
            {
                LogHelper.Log("【Sql】:" + exp);
            };
            return db;
        }
    }
}
