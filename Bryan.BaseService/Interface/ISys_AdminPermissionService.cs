﻿using Bryan.BaseService.Model;
using Bryan.Common.Interface;
using Bryan.Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bryan.BaseService.Dto;

namespace Bryan.BaseService.Interface
{
    public interface ISys_AdminPermissionService : IDenpendency
    {
        IRepository _repository { get; set; }
        int Insert(Sys_AdminPermission model, bool isReturnId);
        int InsertList(List<Sys_AdminPermission> insertList);
        bool DeleteById(int id);
        int DeleteByIdArray(params int[] idArr);
        int DeleteBatch(Expression<Func<Sys_AdminPermission, bool>> where);
        bool Update(Sys_AdminPermission model);
        int UpdateList(List<Sys_AdminPermission> updateList,Expression<Func<Sys_AdminPermission, object>> updateColumns);
        Sys_AdminPermission GetEntityById(int id);
        List<Sys_AdminPermission> GetAllList();
        Sys_AdminPermission GetEntity(Expression<Func<Sys_AdminPermission, bool>> where);
        bool IsAny(Expression<Func<Sys_AdminPermission, bool>> where);
        List<Sys_AdminPermission> GetList(Expression<Func<Sys_AdminPermission, bool>> where, Expression<Func<Sys_AdminPermission, object>> orderBy, bool isDesc = false);
        PageList<Sys_AdminPermission> GetPageList(Expression<Func<Sys_AdminPermission, bool>> where, PageSet pageSet, Expression<Func<Sys_AdminPermission, object>> orderBy, bool isDesc = false, bool isPageNavStr = false);
        TResult GetOneKey<TResult>(Expression<Func<Sys_AdminPermission, bool>> where, Expression<Func<Sys_AdminPermission, TResult>> filed);

        List<PermissionToReloDto> GetRolePerList(int menuId, int btnId);
    }
}
