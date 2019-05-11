using Bryan.BaseService.Model;
using Bryan.Common.Autofac;
using Bryan.Common.Interface;
using Bryan.Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Bryan.BaseService.Interface
{
    public interface ISys_AdminRoleService : IDenpendency
    {
        IRepository _repository { get; set; }
        int Insert(Sys_AdminRole model, bool isReturnId);
        bool DeleteById(int id);
        bool Update(Sys_AdminRole model);
        bool UpdateColumns(Expression<Func<Sys_AdminRole, object>> columns, Sys_AdminRole model, bool isLock = false);
        Sys_AdminRole GetEntityById(int id);
        List<Sys_AdminRole> GetAllList();
        Sys_AdminRole GetEntity(Expression<Func<Sys_AdminRole, bool>> where);
        bool IsAny(Expression<Func<Sys_AdminRole, bool>> where);
        List<Sys_AdminRole> GetList(Expression<Func<Sys_AdminRole, bool>> where, Expression<Func<Sys_AdminRole, object>> orderBy, bool isDesc = false);
        List<TResult> GetList<TResult>(Expression<Func<Sys_AdminRole, bool>> where, Expression<Func<Sys_AdminRole, TResult>> obj, Expression<Func<Sys_AdminRole, object>> orderBy, bool isDesc = false);
        PageList<Sys_AdminRole> GetPageList(Expression<Func<Sys_AdminRole, bool>> where, PageSet pageSet, Expression<Func<Sys_AdminRole, object>> orderBy, bool isDesc = false, bool isPageNavStr = false);
        TResult GetOneKey<TResult>(Expression<Func<Sys_AdminRole, bool>> where, Expression<Func<Sys_AdminRole, TResult>> filed);
    }
}
