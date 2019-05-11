using Bryan.Common.Interface;
using Bryan.Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using HF.GoodsService.Models;

namespace HF.GoodsService.Interface
{
    public interface IGd_GoodsService : IDenpendency
    {
        IRepository _repository { get; set; }
        int Insert(Gd_Goods model);
        bool DeleteById(int id);
        bool Update(Gd_Goods model);
        bool UpdateColumns(Expression<Func<Gd_Goods, object>> columns, Gd_Goods model, bool isLock = false);
        Gd_Goods GetEntityById(int id);
        Gd_Goods GetEntity(Expression<Func<Gd_Goods, bool>> where);
        bool IsAny(Expression<Func<Gd_Goods, bool>> where);
        List<Gd_Goods> GetList(Expression<Func<Gd_Goods, bool>> where, Expression<Func<Gd_Goods, object>> orderBy, bool isDesc = false);
        PageList<Gd_Goods> GetPageList(Expression<Func<Gd_Goods, bool>> where, PageSet pageSet, Expression<Func<Gd_Goods, object>> orderBy, bool isDesc = false, bool isPageNavStr = false);
        TResult GetOneKey<TResult>(Expression<Func<Gd_Goods, bool>> where, Expression<Func<Gd_Goods, TResult>> filed);
        int GetMaxValue(Expression<Func<Gd_Goods, bool>> where, Expression<Func<Gd_Goods, int>> filed);
        Gd_Goods GetEntity(Expression<Func<Gd_Goods, bool>> where, Expression<Func<Gd_Goods, object>> orderBy, bool isDesc = false);
        int GetCount(Expression<Func<Gd_Goods, bool>> where);
    }
}
