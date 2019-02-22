using BryanWu.Domain.Interface;
using BryanWu.Domain.Model;
using Common.Interface;
using Common.Repository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BryanWu.Domain.Service
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
            return _repository.SqlSugarDB.Queryable<Sys_User>().Where(p => true).OrderBy(p => p.Id, OrderByType.Desc).ToList();
        }

        public Sys_User GetUserById(int id)
        {
            return _repository.GetEntityById<Sys_User>(id);
        }

        public PageList<Sys_User> GetPageList(Expression<Func<Sys_User, bool>> where, PageSet pageSet, Expression<Func<Sys_User, object>> orderBy, bool isDesc = false, bool isPageNavStr = false)
        {
            return _repository.GetPageList(where, pageSet, orderBy, isDesc, isPageNavStr);
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
