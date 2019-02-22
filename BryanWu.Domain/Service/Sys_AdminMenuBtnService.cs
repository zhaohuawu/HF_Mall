using BryanWu.Domain.Interface;
using BryanWu.Domain.Model;
using Common.Interface;
using Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BryanWu.Domain.Service
{
    public class Sys_AdminMenuBtnService : ISys_AdminMenuBtnService
    {
        public IRepository _repository { get; set; }
        public ILog _log;
        public Sys_AdminMenuBtnService(IRepository repository, ILog log)
        {
            _repository = repository;
            _log = log;
        }

        public bool DeleteById(int id)
        {
            return _repository.DeleteById<Sys_AdminMenuBtn>(id) > 0;
        }

        public Sys_AdminMenuBtn GetEntity(Expression<Func<Sys_AdminMenuBtn, bool>> where)
        {
            return _repository.GetEntity(where);
        }

        public Sys_AdminMenuBtn GetEntity(Expression<Func<Sys_AdminMenuBtn, bool>> where, Expression<Func<Sys_AdminMenuBtn, object>> orderBy, bool isDesc = false)
        {
            return _repository.GetEntity(where, orderBy, isDesc);
        }

        public Sys_AdminMenuBtn GetEntityById(int id)
        {
            return _repository.GetEntityById<Sys_AdminMenuBtn>(id);
        }

        public List<Sys_AdminMenuBtn> GetList(Expression<Func<Sys_AdminMenuBtn, bool>> where, Expression<Func<Sys_AdminMenuBtn, object>> orderBy, bool isDesc = false)
        {
            return _repository.GetList(where, orderBy, isDesc);
        }

        public int GetMaxValue(Expression<Func<Sys_AdminMenuBtn, bool>> where, Expression<Func<Sys_AdminMenuBtn, int>> filed)
        {
            throw new NotImplementedException();
        }

        public TResult GetOneKey<TResult>(Expression<Func<Sys_AdminMenuBtn, bool>> where, Expression<Func<Sys_AdminMenuBtn, TResult>> filed)
        {
            throw new NotImplementedException();
        }

        public PageList<Sys_AdminMenuBtn> GetPageList(Expression<Func<Sys_AdminMenuBtn, bool>> where, PageSet pageSet, Expression<Func<Sys_AdminMenuBtn, object>> orderBy, bool isDesc = false, bool isPageNavStr = false)
        {
            throw new NotImplementedException();
        }

        public int Insert(Sys_AdminMenuBtn model)
        {
            return _repository.Insert(model);
        }

        public bool IsAny(Expression<Func<Sys_AdminMenuBtn, bool>> where)
        {
            return _repository.IsAny(where);
        }

        public bool Update(Sys_AdminMenuBtn model)
        {
            return _repository.Update(model) > 0;
        }

        public bool UpdateColumns(Expression<Func<Sys_AdminMenuBtn, object>> columns, Sys_AdminMenuBtn model, bool isLock = false)
        {
            return _repository.UpdateColumns(columns, model, isLock) > 0;
        }

        public bool UpdateIgnoreColumns(Expression<Func<Sys_AdminMenuBtn, object>> ignoreColumns, Sys_AdminMenuBtn updateObj, bool isLock = false)
        {
            return _repository.UpdateIgnoreColumns(ignoreColumns, updateObj, isLock) > 0;
        }
    }
}
