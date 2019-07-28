using Bryan.BaseService.Interface;
using Bryan.BaseService.Model;
using Bryan.Common.Interface;
using Bryan.Common.Repository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Bryan.BaseService.Service
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
        
        public PageList<TResult> GetPageList<TResult>(Expression<Func<Sys_User, bool>> where, PageSet pageSet, Expression<Func<Sys_User, TResult>> obj, Expression<Func<Sys_User, object>> orderBy, bool isDesc = false, bool isPageNavStr = false)
        {
            return _repository.GetPageList(where, pageSet, obj, orderBy, isDesc, isPageNavStr);
        }


        public Sys_User GetEntity(Expression<Func<Sys_User, bool>> where)
        {
            return _repository.GetEntity(where);
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

        public int DeleteByIdArray(params int[] idArr)
        {
            return _repository.DeleteByIdArray<Sys_UserRole>(idArr);
        }

        public List<TResult> GetList<TResult>(Expression<Func<Sys_UserRole, bool>> where, Expression<Func<Sys_UserRole, TResult>> obj, Expression<Func<Sys_UserRole, object>> orderBy, bool isDesc = false)
        {
            return _repository.GetList(where, obj, orderBy, isDesc);
        }

        #endregion

        /// <summary>
        /// 获取用户角色列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<Sys_UserRole> GetUserRoleList(int userId, int roleId)
        {
            var queryList = _repository.SqlSugarDB.Queryable<Sys_User, Sys_UserRole, Sys_AdminRole>((su, sr, sa) => new object[] {
                JoinType.Inner,su.Id==sr.UserId,
                JoinType.Inner,sr.RoleId==sa.Id
            })
            .Select((su, sr, sa) => new Sys_UserRole
            {
                Id = sr.Id,
                UserId = sr.UserId,
                RoleId = sr.RoleId
            })
            .WhereIF(userId > 0, su => su.Id == userId)
            .WhereIF(roleId > 0, sa => sa.Id == roleId)
            .ToList();
            return queryList;
        }

    }
}
