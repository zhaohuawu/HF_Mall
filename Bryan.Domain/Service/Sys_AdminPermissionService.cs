using Bryan.Domain.Interface;
using Bryan.Domain.Model;
using Common.Infrastructure.Interface;
using Common.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Bryan.Domain.Service
{
    public class Sys_AdminPermissionService : ISys_AdminPermissionService
    {
        public IRepository _repository { get; set; }

        public Sys_AdminPermissionService(IRepository repository)
        {
            _repository = repository;
        }


        public bool DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Sys_AdminPermission> GetAllList()
        {
            throw new NotImplementedException();
        }

        public Sys_AdminPermission GetEntity(Expression<Func<Sys_AdminPermission, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Sys_AdminPermission GetEntityById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Sys_AdminPermission> GetList(Expression<Func<Sys_AdminPermission, bool>> where, Expression<Func<Sys_AdminPermission, object>> orderBy, bool isDesc = false)
        {
            throw new NotImplementedException();
        }

        public TResult GetOneKey<TResult>(Expression<Func<Sys_AdminPermission, bool>> where, Expression<Func<Sys_AdminPermission, TResult>> filed)
        {
            throw new NotImplementedException();
        }

        public PageList<Sys_AdminPermission> GetPageList(Expression<Func<Sys_AdminPermission, bool>> where, PageSet pageSet, Expression<Func<Sys_AdminPermission, object>> orderBy, bool isDesc = false, bool isPageNavStr = false)
        {
            throw new NotImplementedException();
        }

        public int Insert(Sys_AdminPermission model, bool isReturnId)
        {
            throw new NotImplementedException();
        }

        public bool IsAny(Expression<Func<Sys_AdminPermission, bool>> where)
        {
            throw new NotImplementedException();
        }

        public bool Update(Sys_AdminPermission model)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="insertList"></param>
        /// <returns></returns>
        public int InsertList(List<Sys_AdminPermission> insertList)
        {
            return _repository.InsertList(insertList);
        }

        public int DeleteByIdArray(params int[] idArr)
        {
            return _repository.DeleteByIdArray<Sys_AdminPermission>(idArr);
        }
        
        public int UpdateList(List<Sys_AdminPermission> updateList)
        {
            return _repository.UpdateList(updateList);
        }
    }
}
