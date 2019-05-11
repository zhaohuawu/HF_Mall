using Bryan.Common.Interface;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;

namespace Bryan.Common.Repository
{
    public class DBManager : IDBManager
    {
        //TODO 改为配置的方式
        public static string ConnectionString = string.Empty;
        public static string ConnectionString2 = string.Empty;
        public static bool isLocal = true;
        private ILogger _log;

        public DBManager(ILogger<DBManager> log)
        {
            _log = log;
        }

        public SqlSugarClient GetClient()
        {
            var conn = new ConnectionConfig()
            {
                ConnectionString = ConnectionString,
                DbType = DbType.MySql,
                IsAutoCloseConnection = true,
                IsShardSameThread = true, //设为true相同线程是同一个SqlConnection
            };
            //从库
            if (!string.IsNullOrEmpty(ConnectionString2))
            {
                conn.SlaveConnectionConfigs = new List<SlaveConnectionConfig>()
                {
                    new SlaveConnectionConfig(){ HitRate=10,ConnectionString=ConnectionString2 }
                };
            }
            SqlSugarClient db = new SqlSugarClient(conn);
            db.Ado.IsEnableLogEvent = true;

            if (Convert.ToBoolean(isLocal))
            {
                //db.Aop.OnLogExecuting = (sql, pars) =>//SQL执行前事件
                //{
                //    _log.LogDebug("【Sql】:" + sql, "sql.txt");
                //};
                //db.Aop.OnLogExecuted = (sql, pars) => //SQL执行完事件
                //{
                //    //_log.LogDebug("【Sql】:" + sql);
                //};
            }

            db.Aop.OnError = (exp) =>//执行SQL 错误事件
            {
                _log.LogError("【Sql】:" + exp);
            };
            return db;
        }
    }
}
