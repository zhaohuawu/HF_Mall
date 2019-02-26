﻿using BryanWu.Domain.Model;
using Common.Interface;
using Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BryanWu.Domain.Interface
{
    public interface ISys_OptionService : IDenpendency
    {
        IRepository _repository { get; set; }
        int Insert(Sys_Option model);
        bool DeleteById(int id);
        bool Update(Sys_Option model);
        bool UpdateColumns(Expression<Func<Sys_Option, object>> columns, Sys_Option model, bool isLock = false);
        Sys_Option GetEntityById(int id);
        Sys_Option GetEntity(Expression<Func<Sys_Option, bool>> where);
        bool IsAny(Expression<Func<Sys_Option, bool>> where);
        List<Sys_Option> GetList(Expression<Func<Sys_Option, bool>> where, Expression<Func<Sys_Option, object>> orderBy, bool isDesc = false);
        PageList<Sys_Option> GetPageList(Expression<Func<Sys_Option, bool>> where, PageSet pageSet, Expression<Func<Sys_Option, object>> orderBy, bool isDesc = false, bool isPageNavStr = false);
        TResult GetOneKey<TResult>(Expression<Func<Sys_Option, bool>> where, Expression<Func<Sys_Option, TResult>> filed);
        int GetMaxValue(Expression<Func<Sys_Option, bool>> where, Expression<Func<Sys_Option, int>> filed);
        Sys_Option GetEntity(Expression<Func<Sys_Option, bool>> where, Expression<Func<Sys_Option, object>> orderBy, bool isDesc = false);

        #region 业务逻辑
        bool UpdateOptionOrders(int orders, Sys_Option model);
        #endregion
    }
}
