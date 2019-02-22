using Bryan.Domain.Interface;
using Bryan.Domain.Model;
using Common.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Bryan.Domain.Service
{
    public class Sys_UserService : ISys_UserService
    {
        public IRepository _repository { get; set; }
        public Sys_UserService(IRepository repository)
        {
            _repository = repository;
        }

        #region Sys_User
        public List<Sys_User> GetAllUser()
        {
            return _repository.GetList<Sys_User>(p => 1 == 1);
        }

        public Sys_User GetUserById(int id)
        {
            return _repository.GetEntityById<Sys_User>(id);
        }

        public Sys_User AddUser(Sys_User model)
        {
            return _repository.InsertAndGetEntity(model);
        }

        public bool UpdateUser(Sys_User model)
        {
            return _repository.Update(model) > 0; ;
        }

        public bool DeleteUserBy(int id)
        {
            return _repository.DeleteById<Sys_User>(id) > 0;
        }

        public Sys_User GetEntity(Expression<Func<Sys_User, bool>> where)
        {
            return _repository.GetEntity(where);
        }
        public Sys_User InsertAndGetEntity(Sys_User user)
        {
            return _repository.InsertAndGetEntity(user);
        }

        public bool UpdateColumns(Expression<Func<Sys_User, object>> columns, Sys_User user, bool isLock)
        {
            return _repository.UpdateColumns(columns, user, isLock) > 0;
        }

        public bool IsAny(Expression<Func<Sys_User, bool>> where)
        {
            return _repository.IsAny(where);
        }

        public int Insert(Sys_User model, bool isReturnId)
        {
            return _repository.Insert(model, isReturnId);
        }
        #endregion

        #region Sys_UserRole
        public int InsertList(List<Sys_UserRole> insertList)
        {
            return _repository.InsertList(insertList);
        }

        public bool DeleteById(int id)
        {
            return _repository.DeleteById<Sys_UserRole>(id) > 0;
        }

        public int DeleteByIdArray(params int[] idArr)
        {
            return _repository.DeleteByIdArray<Sys_UserRole>(idArr);
        }
        #endregion

    }
}
