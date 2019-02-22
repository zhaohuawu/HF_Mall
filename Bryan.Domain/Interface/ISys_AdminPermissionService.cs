using Bryan.Domain.Model;
using Common.Infrastructure.Interface;
using Common.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Bryan.Domain.Interface
{
    public interface ISys_AdminPermissionService
    {
        IRepository _repository { get; set; }
        int Insert(Sys_AdminPermission model, bool isReturnId);
        int InsertList(List<Sys_AdminPermission> insertList);
        bool DeleteById(int id);
        int DeleteByIdArray(params int[] idArr);
        bool Update(Sys_AdminPermission model);
        int UpdateList(List<Sys_AdminPermission> updateList);
        Sys_AdminPermission GetEntityById(int id);
        List<Sys_AdminPermission> GetAllList();
        Sys_AdminPermission GetEntity(Expression<Func<Sys_AdminPermission, bool>> where);
        bool IsAny(Expression<Func<Sys_AdminPermission, bool>> where);
        List<Sys_AdminPermission> GetList(Expression<Func<Sys_AdminPermission, bool>> where, Expression<Func<Sys_AdminPermission, object>> orderBy, bool isDesc = false);
        PageList<Sys_AdminPermission> GetPageList(Expression<Func<Sys_AdminPermission, bool>> where, PageSet pageSet, Expression<Func<Sys_AdminPermission, object>> orderBy, bool isDesc = false, bool isPageNavStr = false);
        TResult GetOneKey<TResult>(Expression<Func<Sys_AdminPermission, bool>> where, Expression<Func<Sys_AdminPermission, TResult>> filed);
    }
}
