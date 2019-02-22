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
    public interface ISys_AdminMenuService : IDenpendency
    {
        IRepository _repository { get; set; }
        int Insert(Sys_AdminMenu model);
        bool DeleteById(int id);
        bool Update(Sys_AdminMenu model);
        bool UpdateColumns(Expression<Func<Sys_AdminMenu, object>> columns, Sys_AdminMenu model, bool isLock = false);
        Sys_AdminMenu GetEntityById(int id);
        List<Sys_AdminMenu> GetAllList();
        Sys_AdminMenu GetEntity(Expression<Func<Sys_AdminMenu, bool>> where);
        bool IsAny(Expression<Func<Sys_AdminMenu, bool>> where);
        List<Sys_AdminMenu> GetList(Expression<Func<Sys_AdminMenu, bool>> where, Expression<Func<Sys_AdminMenu, object>> orderBy, bool isDesc = false);
        PageList<Sys_AdminMenu> GetPageList(Expression<Func<Sys_AdminMenu, bool>> where, PageSet pageSet, Expression<Func<Sys_AdminMenu, object>> orderBy, bool isDesc = false, bool isPageNavStr = false);
        TResult GetOneKey<TResult>(Expression<Func<Sys_AdminMenu, bool>> where, Expression<Func<Sys_AdminMenu, TResult>> filed);
        int GetMaxValue(Expression<Func<Sys_AdminMenu, bool>> where, Expression<Func<Sys_AdminMenu, int>> filed);
        Sys_AdminMenu GetEntity(Expression<Func<Sys_AdminMenu, bool>> where, Expression<Func<Sys_AdminMenu, object>> orderBy, bool isDesc = false);
        //int UpdateOrderByPlus(int pid, int orderBy);
        //int UpdateOrderByPlus(int pid, int minOrderBy, int maxOrderBy);
        //int UpdateOrderBySub(int pid, int orderBy);
        //int UpdateOrderBySub(int pid, int minOrderBy, int maxOrderBy);
        #region 业务逻辑
        bool UpdateMenuOrders(int orders, Sys_AdminMenu adminMenu);
        #endregion

    }
}
