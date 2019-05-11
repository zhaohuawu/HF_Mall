using Bryan.BaseService.Model;
using Bryan.Common.Interface;
using Bryan.Common.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Bryan.BaseService.Interface
{
    public interface ILog_AdminService : IDenpendency
    {
        IRepository _repository { get; set; }
        int Insert(Log_Admin model);
        bool DeleteById(int id);
        bool Update(Log_Admin model);
        Log_Admin GetEntityById(int id);
        List<Log_Admin> GetAllList();
        Log_Admin GetEntity(Expression<Func<Log_Admin, bool>> where);
        bool IsAny(Expression<Func<Log_Admin, bool>> where);
        List<Log_Admin> GetList(Expression<Func<Log_Admin, bool>> where, Expression<Func<Log_Admin, object>> orderBy, bool isDesc = false);
        PageList<Log_Admin> GetPageList(Expression<Func<Log_Admin, bool>> where, PageSet pageSet, Expression<Func<Log_Admin, object>> orderBy, bool isDesc = false, bool isPageNavStr = false);
        TResult GetOneKey<TResult>(Expression<Func<Log_Admin, bool>> where, Expression<Func<Log_Admin, TResult>> filed);
        void LogAdmin(Log_Admin logAdmin, HttpContext http);

    }
}
