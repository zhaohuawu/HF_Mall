using BryanWu.Domain.Dto;
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
    public class Sys_AdminPermissionService : ISys_AdminPermissionService
    {
        public IRepository _repository { get; set; }

        public Sys_AdminPermissionService(IRepository repository)
        {
            _repository = repository;
        }


        public bool DeleteById(int id)
        {
            return _repository.DeleteById<Sys_AdminPermission>(id) > 0;
        }

        public List<Sys_AdminPermission> GetAllList()
        {
            return _repository.GetList<Sys_AdminPermission>(p => true);
        }

        public Sys_AdminPermission GetEntity(Expression<Func<Sys_AdminPermission, bool>> where)
        {
            return _repository.GetEntity(where);
        }

        public Sys_AdminPermission GetEntityById(int id)
        {
            return _repository.GetEntityById<Sys_AdminPermission>(id);
        }

        public List<Sys_AdminPermission> GetList(Expression<Func<Sys_AdminPermission, bool>> where, Expression<Func<Sys_AdminPermission, object>> orderBy, bool isDesc = false)
        {
            return _repository.GetList(where, orderBy, isDesc);
        }

        public TResult GetOneKey<TResult>(Expression<Func<Sys_AdminPermission, bool>> where, Expression<Func<Sys_AdminPermission, TResult>> filed)
        {
            throw new NotImplementedException();
        }

        public PageList<Sys_AdminPermission> GetPageList(Expression<Func<Sys_AdminPermission, bool>> where, PageSet pageSet, Expression<Func<Sys_AdminPermission, object>> orderBy, bool isDesc = false, bool isPageNavStr = false)
        {
            throw new NotImplementedException();
        }

        public int Insert(Sys_AdminPermission model, bool isReturnId)
        {
            return _repository.Insert(model, isReturnId);
        }

        public bool IsAny(Expression<Func<Sys_AdminPermission, bool>> where)
        {
            return _repository.IsAny(where);
        }

        public bool Update(Sys_AdminPermission model)
        {
            return _repository.Update(model) > 0;
        }
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="insertList"></param>
        /// <returns></returns>
        public int InsertList(List<Sys_AdminPermission> insertList)
        {
            return _repository.InsertList(insertList);
        }

        public int DeleteByIdArray(params int[] idArr)
        {
            return _repository.DeleteByIdArray<Sys_AdminPermission>(idArr);
        }

        public int UpdateList(List<Sys_AdminPermission> updateList, Expression<Func<Sys_AdminPermission, object>> updateColumns)
        {
            return _repository.UpdateList(updateList, updateColumns);
        }

        public int DeleteBatch(Expression<Func<Sys_AdminPermission, bool>> where)
        {
            return _repository.SqlSugarDB.Deleteable(where).ExecuteCommand();
        }

        /// <summary>
        /// 获取角色菜单列表
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="btnId"></param>
        /// <returns></returns>
        public List<RoleToPermissionDto> GetRolePerList(int menuId, int btnId)
        {
            //菜单数据
            string mSql = @"SELECT sap.RoleId,sap.MenuId,sam.Tag,sap.Type
                            FROM sys_adminrole sar
                            inner join sys_adminpermission sap on sap.RoleId=sar.Id
                            inner join sys_adminmenu sam on sam.Id=sap.MenuId
                            where 1=1
                            and sap.Type='url'
                            and sam.`Status`=1
                            and sar.IsForbidden=1";
            string mWhereSql = "";

            //按钮数据
            string bSql = @"union all
                            SELECT sap.RoleId,sap.MenuId,samb.`Code` as Tag,sap.Type
                            FROM sys_adminrole sar
                            inner join sys_adminpermission sap on sap.RoleId=sar.Id
                            inner join sys_adminmenubtn samb on samb.Id=sap.MenuId
                            inner join sys_adminmenu sam on sam.Id=samb.MenuId
                            where 1=1
                            and sap.Type='btn'
                            and samb.IsForbidden=0
                            and sar.IsForbidden=1";
            string bWhereSql = "";
            var whereDic = new Dictionary<string, object>();
            if (menuId > 0)
            {
                mWhereSql += " and sam.Id=@menuId";
                whereDic.Add("@menuId", menuId);
            }

            if (btnId > 0)
            {
                bWhereSql += " and samb.Id=@btnId";
                whereDic.Add("@btnId", btnId);
            }

            return _repository.ExcuteGetList<RoleToPermissionDto>(mSql + mWhereSql + bSql + bWhereSql, whereDic);
        }
    }
}
