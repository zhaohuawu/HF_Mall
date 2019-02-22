using Bryan.Domain.Model;
using Common.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Bryan.Domain.Interface
{
    public interface ISys_UserService
    {
        IRepository _repository { get; set; }
        List<Sys_User> GetAllUser();
        Sys_User GetUserById(int id);
        bool IsAny(Expression<Func<Sys_User, bool>> where);
        Sys_User AddUser(Sys_User model);
        bool UpdateUser(Sys_User model);
        bool UpdateColumns(Expression<Func<Sys_User, object>> columns, Sys_User user, bool isLock);
        bool DeleteUserBy(int id);
        Sys_User GetEntity(Expression<Func<Sys_User, bool>> where);
        Sys_User InsertAndGetEntity(Sys_User user);
        int Insert(Sys_User model, bool isReturnId);

      
        int InsertList(List<Sys_UserRole> insertList);
        bool DeleteById(int id);
        int DeleteByIdArray(params int[] idArr);
    }
}
