using Bryan.Common;
using Bryan.Common.Interface;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bryan.Common.Repository
{
    public class DBManager : IDBManager
    {
        //TODO 改为配置的方式
        public static string ConnectionString;
        public static string ConnectionString2;
        public static bool isLocal = true;
        private ILogger _log;

        public DBManager(ILogger<DBManager> log)
        {
            _log = log;
        }

        public SqlSugarClient GetClient()
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
                    //LogHelper.Log("【Sql】:" + sql, "sql.txt");
                    //LogHelper.Log("【Sql】【ConnectionString】:" + db.Ado.Connection.ConnectionString, "sql.txt");
                };
                db.Aop.OnLogExecuted = (sql, pars) => //SQL执行完事件
                {
                    _log.LogDebug("【Sql】:" + sql);
                };
            }

            db.Aop.OnError = (exp) =>//执行SQL 错误事件
            {
                _log.LogError("【Sql】:" + exp);
            };
            return db;
        }
    }
}
