using Bryan.BaseService.Interface;
using Bryan.BaseService.Model;
using Bryan.Common.Interface;
using Bryan.Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Bryan.BaseService.Service
{
    public class Sys_AdminMenuService : ISys_AdminMenuService
    {
        public IRepository _repository { get; set; }
        public Sys_AdminMenuService(IRepository repository)
        {
            _repository = repository;
        }

        public bool DeleteById(int id)
        {
            return _repository.DeleteById<Sys_AdminMenu>(id) > 0;
        }

        public List<Sys_AdminMenu> GetAllList()
        {
            throw new NotImplementedException();
        }

        public Sys_AdminMenu GetEntity(Expression<Func<Sys_AdminMenu, bool>> where)
        {
            return _repository.GetEntity(where);
        }

        public Sys_AdminMenu GetEntity(Expression<Func<Sys_AdminMenu, bool>> where, Expression<Func<Sys_AdminMenu, object>> orderBy, bool isDesc = false)
        {
            return _repository.GetEntity(where, orderBy, isDesc);
        }

        public Sys_AdminMenu GetEntityById(int id)
        {
            return _repository.GetEntityById<Sys_AdminMenu>(id);
        }

        public List<Sys_AdminMenu> GetList(Expression<Func<Sys_AdminMenu, bool>> where, Expression<Func<Sys_AdminMenu, object>> orderBy, bool isDesc = false)
        {
            return _repository.GetList(where, orderBy, isDesc);
        }

        public int GetMaxValue(Expression<Func<Sys_AdminMenu, bool>> where, Expression<Func<Sys_AdminMenu, int>> filed)
        {
            return _repository.GetMaxValue(where, filed);
        }

        public TResult GetOneKey<TResult>(Expression<Func<Sys_AdminMenu, bool>> where, Expression<Func<Sys_AdminMenu, TResult>> filed)
        {
            throw new NotImplementedException();
        }

        public PageList<Sys_AdminMenu> GetPageList(Expression<Func<Sys_AdminMenu, bool>> where, PageSet pageSet, Expression<Func<Sys_AdminMenu, object>> orderBy, bool isDesc = false, bool isPageNavStr = false)
        {
            throw new NotImplementedException();
        }

        public int Insert(Sys_AdminMenu model)
        {
            return _repository.Insert(model);
        }

        public bool IsAny(Expression<Func<Sys_AdminMenu, bool>> where)
        {
            return _repository.IsAny(where);
        }

        public bool Update(Sys_AdminMenu model)
        {
            throw new NotImplementedException();
        }

        public bool UpdateColumns(Expression<Func<Sys_AdminMenu, object>> columns, Sys_AdminMenu model, bool isLock = false)
        {
            return _repository.UpdateColumns(columns, model, isLock) > 0;
        }

        #region private method

        /// <summary>
        /// 下移>=OrderBy排序之间的菜单一位
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        private int UpdateOrderByPlus(int pid, int orderBy)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("pid", pid);
            dic.Add("orderBy", orderBy);
            return _repository.ExcuteSql("update Sys_AdminMenu set Orders=Orders+1 where Pid=@pid and Orders>=@orderBy", dic);
        }
        /// <summary>
        /// 下移>=minOrderBy和<maxOrderBy排序之间的菜单一位
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="minOrderBy"></param>
        /// <param name="maxOrderBy"></param>
        /// <returns></returns>
        private int UpdateOrderByPlus(int pid, int minOrderBy, int maxOrderBy)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("pid", pid);
            dic.Add("minOrderBy", minOrderBy);
            dic.Add("maxOrderBy", maxOrderBy);
            return _repository.ExcuteSql("update Sys_AdminMenu set Orders=Orders+1 where Pid=@pid and Orders>=@minOrderBy and Orders<@maxOrderBy", dic);
        }
        /// <summary>
        /// 上移>orderBy排序之间的菜单一位
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        private int UpdateOrderBySub(int pid, int orderBy)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("pid", pid);
            dic.Add("orderBy", orderBy);
            return _repository.ExcuteSql("update Sys_AdminMenu set Orders=Orders-1 where Pid=@pid and Orders>@orderBy", dic);
        }
        /// <summary>
        /// 上移>minOrderBy和=<maxOrderBy排序之间的菜单一位
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="minOrderBy"></param>
        /// <param name="maxOrderBy"></param>
        /// <returns></returns>
        private int UpdateOrderBySub(int pid, int minOrderBy, int maxOrderBy)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("pid", pid);
            dic.Add("minOrderBy", minOrderBy);
            dic.Add("maxOrderBy", maxOrderBy);
            return _repository.ExcuteSql("update Sys_AdminMenu set Orders=Orders-1 where Pid=@pid and Orders>@minOrderBy and Orders<=@maxOrderBy", dic);
        }

        #endregion

        #region 业务逻辑
        /// <summary>
        /// 改变菜单排序
        /// </summary>
        /// <param name="orders"></param>
        /// <param name="adminMenu"></param>
        /// <returns></returns>
        public bool UpdateMenuOrders(int orders, Sys_AdminMenu adminMenu)
        {
            //防止使排序需要为正整数
            if (orders <= 0)
                orders = 1;
            bool isNeedCommit = false;
            _repository.BeginTran();
            try
            {
                var maxOrder = _repository.GetMaxValue<Sys_AdminMenu>(p => p.Pid == adminMenu.Pid, p => p.Orders);
                if (adminMenu.Orders > maxOrder)
                    adminMenu.Orders = maxOrder + 1;//保证更新的排序不会大于（最大值+1）

                if (adminMenu.Orders > orders)//上移排序
                {
                    if (UpdateOrderByPlus(adminMenu.Pid, orders, adminMenu.Orders) > 0)//上移排序需要将排在自己前面的排序往下移1
                    {
                        isNeedCommit = true;
                        adminMenu.Orders = orders;
                        if (!UpdateColumns(c => new { c.Orders }, adminMenu))
                            isNeedCommit = false;
                    }
                }
                else if (adminMenu.Orders < orders)//下移排序
                {
                    if (UpdateOrderBySub(adminMenu.Pid, adminMenu.Orders, orders) > 0)//下移排序需要将排在自己后面的排序往上移1
                    {
                        isNeedCommit = true;
                        adminMenu.Orders = orders;
                        if (!UpdateColumns(c => new { c.Orders }, adminMenu))
                            isNeedCommit = false;
                    }
                }

                if (isNeedCommit)
                    _repository.CommitTran();
                else
                    _repository.RollBackTran();
                return isNeedCommit;
            }
            catch
            {
                _repository.RollBackTran();
                return false;
            }
        }
        #endregion

    }
}
