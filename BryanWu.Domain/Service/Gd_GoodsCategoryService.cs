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
    public class Gd_GoodsCategoryService : IGd_GoodsCategoryService
    {
        public IRepository _repository { get; set; }
        private ILog _log;
        public Gd_GoodsCategoryService(IRepository repository, ILog log)
        {
            _repository = repository;
            _log = log;
        }

        public bool DeleteById(int id)
        {
            return _repository.DeleteById<Gd_GoodsCategory>(id) > 0;
        }

        public int GetCount(Expression<Func<Gd_GoodsCategory, bool>> where)
        {
            return _repository.GetCount(where);
        }

        public Gd_GoodsCategory GetEntity(Expression<Func<Gd_GoodsCategory, bool>> where)
        {
            return _repository.GetEntity(where);
        }

        public Gd_GoodsCategory GetEntity(Expression<Func<Gd_GoodsCategory, bool>> where, Expression<Func<Gd_GoodsCategory, object>> orderBy, bool isDesc = false)
        {
            return _repository.GetEntity(where, orderBy, isDesc);
        }

        public Gd_GoodsCategory GetEntityById(int id)
        {
            return _repository.GetEntityById<Gd_GoodsCategory>(id);
        }

        public List<Gd_GoodsCategory> GetList(Expression<Func<Gd_GoodsCategory, bool>> where, Expression<Func<Gd_GoodsCategory, object>> orderBy, bool isDesc = false)
        {
            return _repository.GetList(where, orderBy, isDesc);
        }

        public int GetMaxValue(Expression<Func<Gd_GoodsCategory, bool>> where, Expression<Func<Gd_GoodsCategory, int>> filed)
        {
            return _repository.GetMaxValue(where, filed);
        }

        public TResult GetOneKey<TResult>(Expression<Func<Gd_GoodsCategory, bool>> where, Expression<Func<Gd_GoodsCategory, TResult>> filed)
        {
            return _repository.GetOneKey(where, filed);
        }

        public PageList<Gd_GoodsCategory> GetPageList(Expression<Func<Gd_GoodsCategory, bool>> where, PageSet pageSet, Expression<Func<Gd_GoodsCategory, object>> orderBy, bool isDesc = false, bool isPageNavStr = false)
        {
            return _repository.GetPageList(where, pageSet, orderBy, isDesc, isPageNavStr);
        }

        public int Insert(Gd_GoodsCategory model)
        {
            return _repository.Insert(model);
        }

        public bool IsAny(Expression<Func<Gd_GoodsCategory, bool>> where)
        {
            return _repository.IsAny(where);
        }

        public bool Update(Gd_GoodsCategory model)
        {
            return _repository.Update(model) > 0;
        }

        public bool UpdateColumns(Expression<Func<Gd_GoodsCategory, object>> columns, Gd_GoodsCategory model, bool isLock = false)
        {
            return _repository.UpdateColumns(columns, model, isLock) > 0;
        }
    }
}
