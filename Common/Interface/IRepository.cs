using Common;
using Common.Repository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataTable = System.Data.DataTable;

namespace Common.Interface
{
    public interface IRepository
    {
        SqlSugarClient SqlSugarDB { get; }
        /// <summary>
        /// 开启事务
        /// </summary>
        void BeginTran();
        /// <summary>
        /// 提交事务
        /// </summary>
        void CommitTran();
        /// <summary>
        /// 回滚事务
        /// </summary>
        void RollBackTran();
        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetEntityById<T>(dynamic id) where T : class, new();
        /// <summary>
        /// 根据条件表达式查询返回一条记录
        /// </summary>
        /// <param name="where">查找条件</param>
        /// <returns>一条记录</returns>
        T GetEntity<T>(Expression<Func<T, bool>> where) where T : class, new();

        /// <summary>
        /// 根据条件表达式查询返回一条记录
        /// </summary>
        /// <param name="where">查找条件</param>
        /// <returns>一条记录</returns>
        TResult GetEntity<T, TResult>(Expression<Func<T, bool>> where, Expression<Func<T, TResult>> select) where T : class, new();
        /// <summary>
        /// 根据条件排序获取其中一条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        T GetEntity<T>(Expression<Func<T, bool>> where, Expression<Func<T, object>> orderBy, bool isDesc = false) where T : class, new();
        /// <summary>
        /// 两张表关联查询
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="express"></param>
        /// <param name="where"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        TResult GetEntity<T, TResult, T2>(Expression<Func<T, T2, object[]>> express, Expression<Func<TResult, bool>> where, Expression<Func<T, T2, TResult>> obj) where T : class, new();
        /// <summary>
        /// 查询满足条件的行数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        int GetCount<T>(Expression<Func<T, bool>> where) where T : class, new();
        /// <summary>
        /// 是否存在这条记录
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        bool IsAny<T>(Expression<Func<T, bool>> where) where T : class, new();
        List<T> GetList<T>(Expression<Func<T, bool>> where) where T : class, new();
        /// <summary>
        /// 单个排序条件获取表中所有的数据
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        List<T> GetList<T>(Expression<Func<T, bool>> where, Expression<Func<T, object>> orderBy, bool isDesc = false) where T : class, new();
        /// <summary>
        /// 多个排序条件获取表中所有的数据
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        List<T> GetList<T>(Expression<Func<T, bool>> where, string orderBy) where T : class, new();
        /// <summary>
        /// 单个排序条件获取表中指定列所有的数据
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        List<object> GetList<T>(Expression<Func<T, bool>> where, Expression<Func<T, object>> obj, Expression<Func<T, object>> orderBy, bool isDesc = false) where T : class, new();
        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="where"></param>
        /// <param name="pageSet"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderByType"></param>
        /// <param name="isPageNavStr"></param>
        /// <returns></returns>
        PageList<T> GetPageList<T>(Expression<Func<T, bool>> where, PageSet pageSet, Expression<Func<T, object>> orderBy, bool isDesc = false, bool isPageNavStr = false) where T : class, new();

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="filed"></param>
        /// <returns></returns>
        int GetMaxValue<T>(Expression<Func<T, bool>> where, Expression<Func<T, int>> filed) where T : class, new();
        
        /// <summary>
        /// 获取单个字段
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="where"></param>
        /// <param name="filed"></param>
        /// <returns></returns>
        TResult GetOneKey<T, TResult>(Expression<Func<T, bool>> where, Expression<Func<T, TResult>> filed) where T : class, new();
        
        int Update<T>(T model, bool isLock = false) where T : class, new();
        /// <summary>
        /// 修改指定的列(主键要有值，主键是更新条件,只更新一条数据)
        /// </summary>
        /// <param name="updateColumns">指定要修改的列</param>
        /// <param name="updateObj">实体要修改的值</param>
        /// <param name="isLock">是否加锁</param>
        /// <returns></returns>
        int UpdateColumns<T>(Expression<Func<T, object>> updateColumns, T updateObj, bool isLock = false) where T : class, new();
        /// <summary>
        ///  修改指定的列（更新一条或多条）
        /// </summary>
        /// <param name="updateColumns"></param>
        /// <param name="updateObj"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        int UpdateColumns<T>(Expression<Func<T, object>> updateColumns, Expression<Func<T, bool>> where) where T : class, new();
        /// <summary>
        ///  修改除了指定的列以外所有列(主键要有值，主键是更新条件,只更新一条数据)
        /// </summary>
        /// <param name="ignoreColumns">指定不需要修改的列</param>
        /// <param name="updateObj">实体要修改的值</param>
        /// <param name="isLock">是否加锁</param>
        /// <returns></returns>
        int UpdateIgnoreColumns<T>(Expression<Func<T, object>> ignoreColumns, T updateObj, bool isLock = false) where T : class, new();
       
        /// <summary>
        /// 批量修改(主键要有值，主键是更新条件)
        /// </summary>
        /// <param name="updateList"></param>
        /// <returns></returns>
        int UpdateList<T>(List<T> updateList,Expression<Func<T, object>> updateColumns) where T : class, new();
        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="isLock">是否使用锁</param>
        /// <returns></returns>
        int DeleteById<T>(dynamic id, bool isLock = false) where T : class, new();
        /// <summary>
        /// 根据表达式删除
        /// </summary>
        /// <param name="where"></param>
        /// <param name="isLock">是否使用锁</param>
        /// <returns></returns>
        int Delete<T>(Expression<Func<T, bool>> where, bool isLock = false) where T : class, new();
        /// <summary>
        /// 根据主键批量删除
        /// </summary>
        /// <param name="idArr">主键数组</param>
        /// <returns></returns>
        int DeleteByIdArray<T>(params int[] idArr) where T : class, new();
        /// <summary>
        /// 插入并返回受影响行数或自增列值（返回值都是int类型）
        /// </summary>
        /// <param name="insertObj"></param>
        /// <param name="isReturnId">是否返回自增列</param>
        /// <returns></returns>
        int Insert<T>(T insertObj, bool isReturnId = false) where T : class, new();
        /// <summary>
        /// 插入并返回受影响实体数据（返回值都是int类型）
        /// </summary>
        /// <param name="insertObj"></param>
        /// <returns></returns>
        T InsertAndGetEntity<T>(T insertObj) where T : class, new();
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="insertList"></param>
        /// <returns></returns>
        int InsertList<T>(List<T> insertList) where T : class, new();

        T ExcuteGetEntity<T>(string sql, Dictionary<string, object> paramsDic) where T : class, new();
        /// <summary>
        /// 执行sql获取list
        /// </summary>
        /// <param name="sql">失去了语句</param>
        /// <param name="paramsDic">参数字典</param>
        /// <returns></returns>
        List<T> ExcuteGetList<T>(string sql, Dictionary<string, object> paramsDic) where T : class, new();
        /// <summary>
        /// 执行sql获取获取DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paramsDic"></param>
        /// <returns></returns>
        DataTable ExcuteGetDataTable(string sql, Dictionary<string, object> paramsDic);
        /// <summary>
        /// 执行sql获取获取IDataReader
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paramsDic"></param>
        /// <returns></returns>
        List<object> ExcuteGetDataReader(string sql, Dictionary<string, object> paramsDic);
        /// <summary>
        /// 执行sql返回受影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paramsDic"></param>
        /// <returns></returns>
        int ExcuteSql(string sql, Dictionary<string, object> paramsDic);
        DataTable ExcuteStoredProcedure(string procedureName);
    }
}
