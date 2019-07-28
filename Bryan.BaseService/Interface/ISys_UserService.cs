using Bryan.BaseService.Model;
using Bryan.Common.Interface;
using Bryan.Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Bryan.BaseService.Interface
{
    public interface ISys_UserService : IDenpendency
    {
        IRepository _repository { get; set; }
        List<Sys_User> GetAllUser();
        Sys_User GetUserById(int id);
        PageList<TResult> GetPageList<TResult>(Expression<Func<Sys_User, bool>> where, PageSet pageSet, Expression<Func<Sys_User, TResult>> obj, Expression<Func<Sys_User, object>> orderBy, bool isDesc = false, bool isPageNavStr = false);
        bool IsAny(Expression<Func<Sys_User, bool>> where);
        bool UpdateColumns(Expression<Func<Sys_User, object>> columns, Sys_User user, bool isLock);
        Sys_User GetEntity(Expression<Func<Sys_User, bool>> where);
        int Insert(Sys_User model, bool isReturnId);
        int InsertList(List<Sys_UserRole> insertList);
        int DeleteByIdArray(params int[] idArr);
        List<TResult> GetList<TResult>(Expression<Func<Sys_UserRole, bool>> where, Expression<Func<Sys_UserRole, TResult>> obj, Expression<Func<Sys_UserRole, object>> orderBy, bool isDesc = false);
        List<Sys_UserRole> GetUserRoleList(int userId, int roleId);
        
    }
}
