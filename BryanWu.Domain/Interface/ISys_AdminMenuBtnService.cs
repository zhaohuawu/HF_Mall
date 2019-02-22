using BryanWu.Domain.Model;
using Common.Autofac;
using Common.Interface;
using Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BryanWu.Domain.Interface
{
    public interface ISys_AdminMenuBtnService : IDenpendency
    {
        IRepository _repository { get; set; }
        int Insert(Sys_AdminMenuBtn model);
        bool DeleteById(int id);
        bool Update(Sys_AdminMenuBtn model);
        bool UpdateColumns(Expression<Func<Sys_AdminMenuBtn, object>> columns, Sys_AdminMenuBtn model, bool isLock = false);
        bool UpdateIgnoreColumns(Expression<Func<Sys_AdminMenuBtn, object>> ignoreColumns, Sys_AdminMenuBtn updateObj, bool isLock = false);
        Sys_AdminMenuBtn GetEntityById(int id);
        Sys_AdminMenuBtn GetEntity(Expression<Func<Sys_AdminMenuBtn, bool>> where);
        bool IsAny(Expression<Func<Sys_AdminMenuBtn, bool>> where);
        List<Sys_AdminMenuBtn> GetList(Expression<Func<Sys_AdminMenuBtn, bool>> where, Expression<Func<Sys_AdminMenuBtn, object>> orderBy, bool isDesc = false);
        PageList<Sys_AdminMenuBtn> GetPageList(Expression<Func<Sys_AdminMenuBtn, bool>> where, PageSet pageSet, Expression<Func<Sys_AdminMenuBtn, object>> orderBy, bool isDesc = false, bool isPageNavStr = false);
        TResult GetOneKey<TResult>(Expression<Func<Sys_AdminMenuBtn, bool>> where, Expression<Func<Sys_AdminMenuBtn, TResult>> filed);
        int GetMaxValue(Expression<Func<Sys_AdminMenuBtn, bool>> where, Expression<Func<Sys_AdminMenuBtn, int>> filed);
        Sys_AdminMenuBtn GetEntity(Expression<Func<Sys_AdminMenuBtn, bool>> where, Expression<Func<Sys_AdminMenuBtn, object>> orderBy, bool isDesc = false);
    }
}
