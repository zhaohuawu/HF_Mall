using Bryan.Common.Interface;
using Bryan.Common.Repository;
using HF.GoodsService.Interface;
using HF.GoodsService.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace HF.GoodsService
{
    public class Gd_GoodsService : IGd_GoodsService
    {
        public IRepository _repository { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public int GetCount(Expression<Func<Gd_Goods, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Gd_Goods GetEntity(Expression<Func<Gd_Goods, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Gd_Goods GetEntity(Expression<Func<Gd_Goods, bool>> where, Expression<Func<Gd_Goods, object>> orderBy, bool isDesc = false)
        {
            throw new NotImplementedException();
        }

        public Gd_Goods GetEntityById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Gd_Goods> GetList(Expression<Func<Gd_Goods, bool>> where, Expression<Func<Gd_Goods, object>> orderBy, bool isDesc = false)
        {
            throw new NotImplementedException();
        }

        public int GetMaxValue(Expression<Func<Gd_Goods, bool>> where, Expression<Func<Gd_Goods, int>> filed)
        {
            throw new NotImplementedException();
        }

        public TResult GetOneKey<TResult>(Expression<Func<Gd_Goods, bool>> where, Expression<Func<Gd_Goods, TResult>> filed)
        {
            throw new NotImplementedException();
        }

        public PageList<Gd_Goods> GetPageList(Expression<Func<Gd_Goods, bool>> where, PageSet pageSet, Expression<Func<Gd_Goods, object>> orderBy, bool isDesc = false, bool isPageNavStr = false)
        {
            throw new NotImplementedException();
        }

        public int Insert(Gd_Goods model)
        {
            throw new NotImplementedException();
        }

        public bool IsAny(Expression<Func<Gd_Goods, bool>> where)
        {
            throw new NotImplementedException();
        }

        public bool Update(Gd_Goods model)
        {
            throw new NotImplementedException();
        }

        public bool UpdateColumns(Expression<Func<Gd_Goods, object>> columns, Gd_Goods model, bool isLock = false)
        {
            throw new NotImplementedException();
        }
    }
}
