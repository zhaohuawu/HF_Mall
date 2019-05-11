using Bryan.Common.Interface;
using Bryan.Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using HF.GoodsService.Models;

namespace HF.GoodsService.Interface
{
    public interface IGd_GoodsCategoryService : IDenpendency
    {
        IRepository _repository { get; set; }
        int Insert(Gd_GoodsCategory model);
        bool DeleteById(int id);
        bool Update(Gd_GoodsCategory model);
        bool UpdateColumns(Expression<Func<Gd_GoodsCategory, object>> columns, Gd_GoodsCategory model, bool isLock = false);
        Gd_GoodsCategory GetEntityById(int id);
        Gd_GoodsCategory GetEntity(Expression<Func<Gd_GoodsCategory, bool>> where);
        bool IsAny(Expression<Func<Gd_GoodsCategory, bool>> where);
        List<Gd_GoodsCategory> GetList(Expression<Func<Gd_GoodsCategory, bool>> where, Expression<Func<Gd_GoodsCategory, object>> orderBy, bool isDesc = false);
        PageList<Gd_GoodsCategory> GetPageList(Expression<Func<Gd_GoodsCategory, bool>> where, PageSet pageSet, Expression<Func<Gd_GoodsCategory, object>> orderBy, bool isDesc = false, bool isPageNavStr = false);
        TResult GetOneKey<TResult>(Expression<Func<Gd_GoodsCategory, bool>> where, Expression<Func<Gd_GoodsCategory, TResult>> filed);
        int GetMaxValue(Expression<Func<Gd_GoodsCategory, bool>> where, Expression<Func<Gd_GoodsCategory, int>> filed);
        Gd_GoodsCategory GetEntity(Expression<Func<Gd_GoodsCategory, bool>> where, Expression<Func<Gd_GoodsCategory, object>> orderBy, bool isDesc = false);
        int GetCount(Expression<Func<Gd_GoodsCategory, bool>> where);
    }
}
