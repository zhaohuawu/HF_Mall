using Bryan.Domain.Model;
using Common.Infrastructure.Interface;
using Common.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Bryan.Domain.Interface
{
    public interface ISys_AdminRoleService
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
        PageList<Sys_AdminRole> GetPageList(Expression<Func<Sys_AdminRole, bool>> where, PageSet pageSet, Expression<Func<Sys_AdminRole, object>> orderBy, bool isDesc = false, bool isPageNavStr = false);
        TResult GetOneKey<TResult>(Expression<Func<Sys_AdminRole, bool>> where, Expression<Func<Sys_AdminRole, TResult>> filed);
    }
}
