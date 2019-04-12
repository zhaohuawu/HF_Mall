using BryanWu.Domain.Interface;
using BryanWu.Domain.Model;
using Bryan.Common.Interface;
using Bryan.Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BryanWu.Domain.Service
{
    public class Sys_AdminRoleService : ISys_AdminRoleService
    {
        public IRepository _repository { get; set; }

        public Sys_AdminRoleService(IRepository repository)
        {
            _repository = repository;
        }

        public bool DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Sys_AdminRole> GetAllList()
        {
            throw new NotImplementedException();
        }

        public Sys_AdminRole GetEntity(Expression<Func<Sys_AdminRole, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Sys_AdminRole GetEntityById(int id)
        {
            return _repository.GetEntityById<Sys_AdminRole>(id);
        }

        public List<Sys_AdminRole> GetList(Expression<Func<Sys_AdminRole, bool>> where, Expression<Func<Sys_AdminRole, object>> orderBy, bool isDesc = false)
        {
            throw new NotImplementedException();
        }

        public TResult GetOneKey<TResult>(Expression<Func<Sys_AdminRole, bool>> where, Expression<Func<Sys_AdminRole, TResult>> filed)
        {
            throw new NotImplementedException();
        }

        public PageList<Sys_AdminRole> GetPageList(Expression<Func<Sys_AdminRole, bool>> where, PageSet pageSet, Expression<Func<Sys_AdminRole, object>> orderBy, bool isDesc = false, bool isPageNavStr = false)
        {
            return _repository.GetPageList(where, pageSet, orderBy, isDesc, isPageNavStr);
        }

        public int Insert(Sys_AdminRole model, bool isReturnId)
        {
            return _repository.Insert(model, isReturnId);
        }

        public bool IsAny(Expression<Func<Sys_AdminRole, bool>> where)
        {
            return _repository.IsAny(where);
        }

        public bool Update(Sys_AdminRole model)
        {
            throw new NotImplementedException();
        }

        public bool UpdateColumns(Expression<Func<Sys_AdminRole, object>> columns, Sys_AdminRole model, bool isLock = false)
        {
            return _repository.UpdateColumns(columns, model, isLock) > 0;
        }

        public List<TResult> GetList<TResult>(Expression<Func<Sys_AdminRole, bool>> where, Expression<Func<Sys_AdminRole, TResult>> obj, Expression<Func<Sys_AdminRole, object>> orderBy, bool isDesc = false)
        {
            return _repository.GetList(where, obj, orderBy, isDesc);
        }
    }
}
