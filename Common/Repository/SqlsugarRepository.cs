using Bryan.Common.Interface;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace Bryan.Common.Repository
{
    public class SqlsugarRepository : IRepository
    {
        public SqlSugarClient SqlSugarDB => DBManager.GetInstance();

        public void BeginTran()
        {
            SqlSugarDB.Ado.BeginTran();
        }

        public void CommitTran()
        {
            SqlSugarDB.Ado.CommitTran();
        }

        public void RollBackTran()
        {
            SqlSugarDB.Ado.RollbackTran();
        }

        #region Query

        #region SingleTable
        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetEntityById<T>(dynamic id) where T : class, new()
        {
            return SqlSugarDB.Queryable<T>().InSingle(id);
        }

        /// <summary>
        /// 根据条件表达式查询返回一条记录
        /// </summary>
        /// <param name="where">查找条件</param>
        /// <returns>一条记录</returns>
        public T GetEntity<T>(Expression<Func<T, bool>> where) where T : class, new()
        {
            return SqlSugarDB.Queryable<T>().Where(where).First();
        }

        /// <summary>
        /// 根据条件表达式查询返回一条记录
        /// </summary>
        /// <param name="where">查找条件</param>
        /// <returns>一条记录</returns>
        public TResult GetEntity<T, TResult>(Expression<Func<T, bool>> where, Expression<Func<T, TResult>> select) where T : class, new()
        {
            return SqlSugarDB.Queryable<T>().Where(where).Select(select).First();
        }
        /// <summary>
        /// 根据条件排序获取其中一条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public T GetEntity<T>(Expression<Func<T, bool>> where, Expression<Func<T, object>> orderBy, bool isDesc = false) where T : class, new()
        {
            if (isDesc)
                return SqlSugarDB.Queryable<T>().Where(where).OrderBy(orderBy, OrderByType.Desc).First();
            else
                return SqlSugarDB.Queryable<T>().Where(where).OrderBy(orderBy).First();
        }
        /// <summary>
        /// 两张表关联查询
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="express"></param>
        /// <param name="where"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public TResult GetEntity<T, TResult, T2>(Expression<Func<T, T2, object[]>> express, Expression<Func<TResult, bool>> where, Expression<Func<T, T2, TResult>> obj) where T : class, new()
        {
            return SqlSugarDB.Queryable<T, T2>(express).Select(obj).MergeTable().Where(where).First();
        }

        /// <summary>
        /// 查询满足条件的行数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public int GetCount<T>(Expression<Func<T, bool>> where) where T : class, new()
        {
            return SqlSugarDB.Queryable<T>().Where(where).Count();
        }

        /// <summary>
        /// 是否存在这条记录
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public bool IsAny<T>(Expression<Func<T, bool>> where) where T : class, new()
        {
            return SqlSugarDB.Queryable<T>().Any(where);
        }

        public List<T> GetList<T>(Expression<Func<T, bool>> where) where T : class, new()
        {
            return SqlSugarDB.Queryable<T>().Where(where).ToList();
        }

        /// <summary>
        /// 单个排序条件获取表中所有的数据
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        public List<T> GetList<T>(Expression<Func<T, bool>> where, Expression<Func<T, object>> orderBy, bool isDesc = false) where T : class, new()
        {
            if (isDesc)
                return SqlSugarDB.Queryable<T>().Where(where).OrderBy(orderBy, OrderByType.Desc).ToList();
            else
                return SqlSugarDB.Queryable<T>().Where(where).OrderBy(orderBy, OrderByType.Asc).ToList();
        }

        /// <summary>
        /// 多个排序条件获取表中所有的数据
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        public List<T> GetList<T>(Expression<Func<T, bool>> where, string orderBy) where T : class, new()
        {
            return SqlSugarDB.Queryable<T>().Where(where).OrderBy(orderBy).ToList();
        }

        /// <summary>
        /// 单个排序条件获取表中指定列所有的数据
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        public List<TResult> GetList<TResult, T>(Expression<Func<T, bool>> where, Expression<Func<T, TResult>> obj, Expression<Func<T, object>> orderBy, bool isDesc = false) where T : class, new()
        {
            if (isDesc)
                return SqlSugarDB.Queryable<T>().Where(where).OrderBy(orderBy, OrderByType.Desc).Select(obj).ToList();
            else
                return SqlSugarDB.Queryable<T>().Where(where).OrderBy(orderBy, OrderByType.Asc).Select(obj).ToList();
        }

        public List<TResult> GetList<T, TResult, T2>(Expression<Func<T, T2, object[]>> express, Expression<Func<TResult, bool>> where, Expression<Func<T, T2, TResult>> obj, Expression<Func<TResult, object>> orderBy, bool isDesc = false) where T : class, new()
        {
            if (isDesc)
                return SqlSugarDB.Queryable<T, T2>(express).Select(obj).MergeTable().Where(where).OrderBy(orderBy, OrderByType.Desc).ToList();
            else
                return SqlSugarDB.Queryable<T, T2>(express).Select(obj).MergeTable().Where(where).OrderBy(orderBy, OrderByType.Asc).ToList();
        }

        /// <summary>
        /// 多个排序条件获取表中所有的数据
        /// </summary>
        /// <param name="where"></param>
        /// <param name="obj"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<TResult> GetList<T, TResult>(Expression<Func<T, bool>> where, Expression<Func<T, TResult>> obj, string orderBy) where T : class, new()
        {
            return SqlSugarDB.Queryable<T>().Where(where).OrderBy(orderBy).Select(obj).ToList();
        }

        public List<TResult> GetListByTake<T, TResult>(Expression<Func<T, bool>> where, Expression<Func<T, TResult>> obj, string orderBy, int num) where T : class, new()
        {
            return SqlSugarDB.Queryable<T>().Where(where).OrderBy(orderBy).Select(obj).Take(num).ToList();
        }

        public List<T> GetListByTake<T>(Expression<Func<T, bool>> where, string orderBy, int num) where T : class, new()
        {
            return SqlSugarDB.Queryable<T>().Where(where).OrderBy(orderBy).Take(num).ToList();
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="where"></param>
        /// <param name="pageSet"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderByType"></param>
        /// <param name="isPageNavStr"></param>
        /// <returns></returns>
        public PageList<T> GetPageList<T>(Expression<Func<T, bool>> where, PageSet pageSet, Expression<Func<T, object>> orderBy, bool isDesc = false, bool isPageNavStr = false) where T : class, new()
        {
            if (pageSet == null)
            {
                pageSet = new PageSet() { PageIndex = 1, PageSize = 100000 };
            }

            OrderByType orderByType = OrderByType.Asc;
            if (isDesc)
                orderByType = OrderByType.Desc;

            int totalCount = 0;
            var list = SqlSugarDB.Queryable<T>().Where(where).OrderBy(orderBy, orderByType).ToPageList(pageSet.PageIndex, pageSet.PageSize, ref totalCount);

            PageList<T> result = new PageList<T>();
            result.PageIndex = pageSet.PageIndex;
            result.RecordCount = totalCount;
            result.SetPageCount(pageSet.PageSize);
            if (isPageNavStr)
            {
                result.SetPageNavStr();
            }

            result.DataList = list;
            return result;
        }

        /// <summary>
        /// 分页查询数据（多个排序条件）
        /// </summary>
        /// <param name="where"></param>
        /// <param name="pageSet"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderByType"></param>
        /// <param name="isPageNavStr"></param>
        /// <returns></returns>
        public PageList<T> GetPageList<T>(Expression<Func<T, bool>> where, PageSet pageSet, string orderBy, bool isPageNavStr = false) where T : class, new()
        {
            if (pageSet == null)
            {
                pageSet = new PageSet() { PageIndex = 1, PageSize = 100000 };
            }
            int totalCount = 0;
            var list = SqlSugarDB.Queryable<T>().Where(where).OrderBy(orderBy).ToPageList(pageSet.PageIndex, pageSet.PageSize, ref totalCount);
            PageList<T> result = new PageList<T>();
            result.PageIndex = pageSet.PageIndex;
            result.RecordCount = totalCount;
            result.SetPageCount(pageSet.PageSize);
            if (isPageNavStr)
            {
                result.SetPageNavStr();
            }

            result.DataList = list;
            return result;
        }

        /// <summary>
        /// 分页查询数据（指定列）
        /// </summary>
        /// <param name="where"></param>
        /// <param name="pageSet"></param>
        /// <param name="obj"></param>
        /// <param name="orderBy"></param>
        /// <param name="isDesc"></param>
        /// <param name="isPageNavStr"></param>
        /// <returns></returns>
        public PageList<TResult> GetPageList<TResult, T>(Expression<Func<T, bool>> where, PageSet pageSet, Expression<Func<T, TResult>> obj, Expression<Func<T, object>> orderBy, bool isDesc = false, bool isPageNavStr = false) where T : class, new()
        {
            if (pageSet == null)
            {
                pageSet = new PageSet() { PageIndex = 1, PageSize = 100000 };
            }

            OrderByType orderByType = OrderByType.Asc;
            if (isDesc)
                orderByType = OrderByType.Desc;

            int totalCount = 0;
            var list = SqlSugarDB.Queryable<T>().Where(where).OrderBy(orderBy, orderByType).Select(obj).ToPageList(pageSet.PageIndex, pageSet.PageSize, ref totalCount);

            PageList<TResult> result = new PageList<TResult>();
            result.PageIndex = pageSet.PageIndex;
            result.RecordCount = totalCount;
            result.SetPageCount(pageSet.PageSize);
            if (isPageNavStr)
            {
                result.SetPageNavStr();
            }

            result.DataList = list;
            return result;
        }

        /// <summary>
        /// 分页查询数据（指定列，多排序条件）
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="where"></param>
        /// <param name="pageSet"></param>
        /// <param name="obj"></param>
        /// <param name="orderBy"></param>
        /// <param name="isPageNavStr"></param>
        /// <returns></returns>
        public PageList<TResult> GetPageList<T, TResult>(Expression<Func<T, bool>> where, PageSet pageSet, Expression<Func<T, TResult>> obj, string orderBy, bool isPageNavStr = false) where T : class, new()
        {
            if (pageSet == null)
            {
                pageSet = new PageSet() { PageIndex = 1, PageSize = 100000 };
            }

            int totalCount = 0;
            var list = SqlSugarDB.Queryable<T>().Where(where).OrderBy(orderBy).Select(obj).ToPageList(pageSet.PageIndex, pageSet.PageSize, ref totalCount);

            PageList<TResult> result = new PageList<TResult>();
            result.PageIndex = pageSet.PageIndex;
            result.RecordCount = totalCount;
            result.SetPageCount(pageSet.PageSize);
            if (isPageNavStr)
            {
                result.SetPageNavStr();
            }

            result.DataList = list;
            return result;
        }

        /// <summary>
        /// 两表分页查询数据（指定列）
        /// </summary>
        /// <param name="where"></param>
        /// <param name="pageSet"></param>
        /// <param name="obj"></param>
        /// <param name="orderBy"></param>
        /// <param name="isDesc"></param>
        /// <param name="isPageNavStr"></param>
        /// <returns></returns>
        public PageList<TResult> GetPageList<T, TResult, T2>(Expression<Func<T, T2, object[]>> express, Expression<Func<TResult, bool>> where, PageSet pageSet, Expression<Func<T, T2, TResult>> obj, Expression<Func<TResult, object>> orderBy, bool isDesc = false, bool isPageNavStr = false) where T : class, new()
        {
            if (pageSet == null)
            {
                pageSet = new PageSet() { PageIndex = 1, PageSize = 100000 };
            }

            OrderByType orderByType = OrderByType.Asc;
            if (isDesc)
                orderByType = OrderByType.Desc;

            int totalCount = 0;
            var list = SqlSugarDB.Queryable<T, T2>(express).Select(obj).MergeTable().Where(where).OrderBy(orderBy, orderByType).ToPageList(pageSet.PageIndex, pageSet.PageSize, ref totalCount);

            PageList<TResult> result = new PageList<TResult>();
            result.PageIndex = pageSet.PageIndex;
            result.RecordCount = totalCount;
            result.SetPageCount(pageSet.PageSize);
            if (isPageNavStr)
            {
                result.SetPageNavStr();
            }

            result.DataList = list;
            return result;
        }
        #endregion

        #region MoreTables
        //TODO 添加多表查询
        #endregion

        public int GetMaxValue<T>(Expression<Func<T, bool>> where, Expression<Func<T, int>> filed) where T : class, new()
        {
            return SqlSugarDB.Queryable<T>().Where(where).Max(filed);
        }

        public TResult GetOneKey<T, TResult>(Expression<Func<T, bool>> where, Expression<Func<T, TResult>> filed) where T : class, new()
        {
            return SqlSugarDB.Queryable<T>().Where(where).Select(filed).First();
        }

        #endregion

        #region Update
        /// <summary>
        /// 主键必须要有值
        /// </summary>
        /// <param name="updateObj"></param>
        /// <param name="isLock"></param>
        /// <returns></returns>
        public int Update<T>(T updateObj, bool isLock = false) where T : class, new()
        {
            return SqlSugarDB.Updateable<T>().ExecuteCommand();
        }

        /// <summary>
        /// 修改指定的列(主键要有值，主键是更新条件,只更新一条数据)
        /// </summary>
        /// <param name="updateColumns">指定要修改的列</param>
        /// <param name="updateObj">实体要修改的值</param>
        /// <param name="isLock">是否加锁</param>
        /// <returns></returns>
        public int UpdateColumns<T>(Expression<Func<T, object>> updateColumns, T updateObj, bool isLock = false) where T : class, new()
        {
            if (isLock)
                return SqlSugarDB.Updateable(updateObj).UpdateColumns(updateColumns).With(SqlWith.UpdLock).ExecuteCommand();
            else
                return SqlSugarDB.Updateable(updateObj).UpdateColumns(updateColumns).ExecuteCommand();
        }

        /// <summary>
        ///  修改指定的列（更新一条或多条）
        /// </summary>
        /// <param name="updateColumns"></param>
        /// <param name="updateObj"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public int UpdateColumns<T>(Expression<Func<T, object>> updateColumns, Expression<Func<T, bool>> where) where T : class, new()
        {
            return SqlSugarDB.Updateable<T>().UpdateColumns(updateColumns).Where(where).ExecuteCommand();
        }

        /// <summary>
        ///  修改除了指定的列以外所有列(主键要有值，主键是更新条件,只更新一条数据)
        /// </summary>
        /// <param name="ignoreColumns">指定不需要修改的列</param>
        /// <param name="updateObj">实体要修改的值</param>
        /// <param name="isLock">是否加锁</param>
        /// <returns></returns>
        public int UpdateIgnoreColumns<T>(Expression<Func<T, object>> ignoreColumns, T updateObj, bool isLock = false) where T : class, new()
        {
            if (isLock)
                return SqlSugarDB.Updateable(updateObj).IgnoreColumns(ignoreColumns).With(SqlWith.UpdLock).ExecuteCommand();
            else
                return SqlSugarDB.Updateable(updateObj).IgnoreColumns(ignoreColumns).ExecuteCommand();
        }

        /// <summary>
        /// 批量删除(主键要有值，主键是更新条件)
        /// </summary>
        /// <param name="updateList"></param>
        /// <returns></returns>
        public int UpdateList<T>(List<T> updateList, Expression<Func<T, object>> updateColumns) where T : class, new()
        {
            return SqlSugarDB.Updateable(updateList).UpdateColumns(updateColumns).ExecuteCommand();
        }

        #endregion

        #region Delete
        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="isLock">是否使用锁</param>
        /// <returns></returns>
        public int DeleteById<T>(dynamic id, bool isLock = false) where T : class, new()
        {
            if (isLock)
                return SqlSugarDB.Deleteable<T>().In(id).With(SqlWith.RowLock).ExecuteCommand();
            else
                return SqlSugarDB.Deleteable<T>().In(id).ExecuteCommand();
        }

        /// <summary>
        /// 根据表达式删除
        /// </summary>
        /// <param name="where"></param>
        /// <param name="isLock">是否使用锁</param>
        /// <returns></returns>
        public int Delete<T>(Expression<Func<T, bool>> where, bool isLock = false) where T : class, new()
        {
            if (isLock)
                return SqlSugarDB.Deleteable<T>().Where(where).With(SqlWith.RowLock).ExecuteCommand();
            else
                return SqlSugarDB.Deleteable<T>().Where(where).ExecuteCommand();
        }

        /// <summary>
        /// 根据主键批量删除
        /// </summary>
        /// <param name="idArr">主键数组</param>
        /// <returns></returns>
        public int DeleteByIdArray<T>(params int[] idArr) where T : class, new()
        {
            return SqlSugarDB.Deleteable<T>().In(idArr).ExecuteCommand();
        }

        #endregion

        #region Insert
        /// <summary>
        /// 插入并返回受影响行数或自增列值（返回值都是int类型）
        /// </summary>
        /// <param name="insertObj"></param>
        /// <param name="isReturnId">是否返回自增列</param>
        /// <returns></returns>
        public int Insert<T>(T insertObj, bool isReturnId = false) where T : class, new()
        {
            if (isReturnId)
                return SqlSugarDB.Insertable(insertObj).ExecuteReturnIdentity();
            else
                return SqlSugarDB.Insertable(insertObj).ExecuteCommand();
        }

        /// <summary>
        /// 插入并返回自增列值（返回值都是long类型）
        /// </summary>
        /// <param name="insertObj"></param>
        /// <returns></returns>
        public long InsertGetId<T>(T insertObj) where T : class, new()
        {
            return SqlSugarDB.Insertable(insertObj).ExecuteReturnBigIdentity();
        }

        /// <summary>
        /// 插入并返回实体 ,  只是自identity 添加到 参数的实体里面并返回，没有查2次库，所以有些默认值什么的变动是取不到的你们需要手动进行2次查询获取
        /// </summary>
        /// <param name="insertObj"></param>
        /// <returns></returns>
        public T InsertAndGetEntity<T>(T insertObj) where T : class, new()
        {
            return SqlSugarDB.Insertable(insertObj).ExecuteReturnEntity();
        }

        /// <summary>
        /// 批量插入（性能很快不用操心）
        /// </summary>
        /// <param name="insertList"></param>
        /// <returns></returns>
        public int InsertList<T>(List<T> insertList) where T : class, new()
        {
            return SqlSugarDB.Insertable(insertList.ToArray()).ExecuteCommand();
        }
        #endregion

        #region ExcuteSql
        public T ExcuteGetEntity<T>(string sql, Dictionary<string, object> paramsDic) where T : class, new()
        {
            return SqlSugarDB.Ado.SqlQuerySingle<T>(sql, paramsDic);
        }

        /// <summary>
        /// 执行sql获取list
        /// </summary>
        /// <param name="sql">失去了语句</param>
        /// <param name="paramsDic">参数字典</param>
        /// <returns></returns>
        public List<T> ExcuteGetList<T>(string sql, Dictionary<string, object> paramsDic) where T : class, new()
        {
            return SqlSugarDB.Ado.SqlQuery<T>(sql, paramsDic);
        }

        /// <summary>
        /// 执行sql获取获取IDataReader
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paramsDic"></param>
        /// <returns></returns>
        public List<object> ExcuteGetDataReader(string sql, Dictionary<string, object> paramsDic)
        {
            List<object> readerList = new List<object>();
            IDataReader reader = SqlSugarDB.Ado.GetDataReader(sql, paramsDic);
            while (reader.Read())
            {
                readerList.Add(JSONHelper.Seriallize(reader));
            }
            return readerList;
        }

        /// <summary>
        /// 执行sql返回受影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paramsDic"></param>
        /// <returns></returns>
        public int ExcuteSql(string sql, Dictionary<string, object> paramsDic)
        {
            //List<SugarParameter> paramsList = new List<SugarParameter>();
            //foreach (var dic in paramsDic)
            //{
            //    string key = dic.Key.Contains("@") ? dic.Key : ("@" + dic.Key);
            //    paramsList.Add(new SugarParameter(key, dic.Value));
            //}

            return SqlSugarDB.Ado.ExecuteCommand(sql, paramsDic);
        }

        public string ExcuteSqlString(string sql, Dictionary<string, object> paramsDic)
        {
            return SqlSugarDB.Ado.GetString(sql, paramsDic);
        }

        #endregion

        #region StoredProcedure 

        public void tt()
        {
            var t = new SugarParameter("@p1", "1");
        }

        System.Data.DataTable IRepository.ExcuteStoredProcedure(string procedureName)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable ExcuteGetDataTable(string sql, Dictionary<string, object> paramsDic)
        {
            throw new NotImplementedException();
        }

        System.Data.DataTable IRepository.ExcuteGetDataTable(string sql, Dictionary<string, object> paramsDic)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable ExcuteStoredProcedure(string procedureName)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
