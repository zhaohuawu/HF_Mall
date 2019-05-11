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
    public class Sys_OptionService : ISys_OptionService
    {
        public IRepository _repository { get; set; }
        public Sys_OptionService(IRepository repository)
        {
            _repository = repository;
        }

        public bool DeleteById(int id)
        {
            return _repository.DeleteById<Sys_Option>(id) > 0;
        }

        public Sys_Option GetEntity(Expression<Func<Sys_Option, bool>> where)
        {
            return _repository.GetEntity(where);
        }

        public Sys_Option GetEntity(Expression<Func<Sys_Option, bool>> where, Expression<Func<Sys_Option, object>> orderBy, bool isDesc = false)
        {
            return _repository.GetEntity(where, orderBy, isDesc);
        }

        public Sys_Option GetEntityById(int id)
        {
            return _repository.GetEntityById<Sys_Option>(id);
        }

        public List<Sys_Option> GetList(Expression<Func<Sys_Option, bool>> where, Expression<Func<Sys_Option, object>> orderBy, bool isDesc = false)
        {
            return _repository.GetList(where, orderBy, isDesc);
        }

        public int GetMaxValue(Expression<Func<Sys_Option, bool>> where, Expression<Func<Sys_Option, int>> filed)
        {
            return _repository.GetMaxValue(where, filed);
        }

        public PageList<Sys_Option> GetPageList(Expression<Func<Sys_Option, bool>> where, PageSet pageSet, Expression<Func<Sys_Option, object>> orderBy, bool isDesc = false, bool isPageNavStr = false)
        {
            return _repository.GetPageList(where, pageSet, orderBy, isDesc);
        }

        public int Insert(Sys_Option model)
        {
            return _repository.Insert(model);
        }

        public bool IsAny(Expression<Func<Sys_Option, bool>> where)
        {
            return _repository.IsAny(where);
        }

        public bool Update(Sys_Option model)
        {
            return _repository.Update(model) > 0;
        }

        public bool UpdateColumns(Expression<Func<Sys_Option, object>> columns, Sys_Option model, bool isLock = false)
        {
            return _repository.UpdateColumns(columns, model, isLock) > 0;
        }

        public TResult GetOneKey<TResult>(Expression<Func<Sys_Option, bool>> where, Expression<Func<Sys_Option, TResult>> filed)
        {
            throw new NotImplementedException();
        }


        public int GetCount(Expression<Func<Sys_Option, bool>> where)
        {
            return _repository.GetCount(where);
        }

        #region private method

        /// <summary>
        /// 下移>=OrderBy排序之间的菜单一位
        /// </summary>
        /// <param name="groupKey"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        private int UpdateOrderByPlus(string groupKey, int orderBy)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("groupKey", groupKey);
            dic.Add("orders", orderBy);
            return _repository.ExcuteSql("update sys_option set orders=orders+1 where levels=2 and groupKey=@groupKey and orders>=@orders", dic);
        }

        /// <summary>
        /// 下移>=minOrderBy和<maxOrderBy排序之间的菜单一位
        /// </summary>
        /// <param name="groupKey"></param>
        /// <param name="minOrderBy"></param>
        /// <param name="maxOrderBy"></param>
        /// <returns></returns>
        private int UpdateOrderByPlus(string groupKey, int minOrderBy, int maxOrderBy)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("groupKey", groupKey);
            dic.Add("minOrderBy", minOrderBy);
            dic.Add("maxOrderBy", maxOrderBy);
            return _repository.ExcuteSql("update sys_option set orders=orders+1 where levels=2 and groupKey=@groupKey and orders>=@minOrderBy and orders<@maxOrderBy", dic);
        }

        /// <summary>
        /// 上移>orderBy排序之间的菜单一位
        /// </summary>
        /// <param name="groupKey"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        private int UpdateOrderBySub(string groupKey, int orderBy)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("groupKey", groupKey);
            dic.Add("orders", orderBy);
            return _repository.ExcuteSql("update sys_option set orders=orders-1 where levels=2 and groupKey=@groupKey and orders>@orders", dic);
        }

        /// <summary>
        /// 上移>minOrderBy和=<maxOrderBy排序之间的菜单一位
        /// </summary>
        /// <param name="groupKey"></param>
        /// <param name="minOrderBy"></param>
        /// <param name="maxOrderBy"></param>
        /// <returns></returns>
        private int UpdateOrderBySub(string groupKey, int minOrderBy, int maxOrderBy)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("groupKey", groupKey);
            dic.Add("minOrderBy", minOrderBy);
            dic.Add("maxOrderBy", maxOrderBy);
            return _repository.ExcuteSql("update sys_option set orders=orders-1 where levels=2 and groupKey=@groupKey and orders>@minOrderBy and orders<=@maxOrderBy", dic);
        }

        #endregion

        #region 业务逻辑
        /// <summary>
        /// 添加字典
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddOption(Sys_Option model)
        {
            //防止使排序需要为正整数
            if (model.Orders <= 0)
                model.Orders = 1;
            bool isNeedCommit = false;
            _repository.BeginTran();
            try
            {
                var maxOrder = _repository.GetMaxValue<Sys_Option>(p => p.GroupKey == model.GroupKey, p => p.Orders);
                if (model.Orders > maxOrder)
                {
                    model.Orders = maxOrder + 1;//保证更新的排序不会大于（最大值+1）
                    if (_repository.Insert(model) > 0)
                        isNeedCommit = true;
                }
                else
                {
                    if (UpdateOrderByPlus(model.GroupKey, model.Orders) > 0)//上移排序需要将排在自己前面的排序往下移1
                    {
                        isNeedCommit = true;
                        if (_repository.Insert(model) <= 0)
                            isNeedCommit = false;
                    }
                }

                if (isNeedCommit)
                    _repository.CommitTran();
                else
                    _repository.RollBackTran();
                return isNeedCommit;
            }
            catch (Exception ex)
            {
                _repository.RollBackTran();
                return false;
            }

        }

        /// <summary>
        /// 改变字典排序
        /// </summary>
        /// <param name="orders"></param>
        /// <param name="adminMenu"></param>
        /// <returns></returns>
        public bool UpdateOptionOrders(int orders, Sys_Option adminMenu)
        {
            //防止使排序需要为正整数
            if (orders <= 0)
                orders = 1;
            bool isNeedCommit = false;
            _repository.BeginTran();
            try
            {
                var maxOrder = _repository.GetMaxValue<Sys_Option>(p => p.GroupKey == adminMenu.GroupKey, p => p.Orders);
                if (adminMenu.Orders > maxOrder)
                    adminMenu.Orders = maxOrder + 1;//保证更新的排序不会大于（最大值+1）

                if (adminMenu.Orders > orders)//上移排序
                {
                    if (UpdateOrderByPlus(adminMenu.GroupKey, orders, adminMenu.Orders) > 0)//上移排序需要将排在自己前面的排序往下移1
                    {
                        isNeedCommit = true;
                        adminMenu.Orders = orders;
                        if (!UpdateColumns(c => new { c.Orders }, adminMenu))
                            isNeedCommit = false;
                    }
                }
                else if (adminMenu.Orders < orders)//下移排序
                {
                    if (UpdateOrderBySub(adminMenu.GroupKey, adminMenu.Orders, orders) > 0)//下移排序需要将排在自己后面的排序往上移1
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
#pragma warning disable CS0168 // 声明了变量“ex”，但从未使用过
            catch (Exception ex)
#pragma warning restore CS0168 // 声明了变量“ex”，但从未使用过
            {
                _repository.RollBackTran();
                return false;
            }
        }


        #endregion
    }
}
