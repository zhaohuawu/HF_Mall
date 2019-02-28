using BryanWu.Domain.Model;
using Common.Interface;
using Common.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BryanWu.Domain.Interface
{
    public interface ISys_UploadFileService : IDenpendency
    {
        IRepository _repository { get; set; }
        List<Sys_UploadFile> GetAllUser();
        Sys_UploadFile GetUserById(int id);

        PageList<Sys_UploadFile> GetPageList(Expression<Func<Sys_UploadFile, bool>> where, PageSet pageSet, Expression<Func<Sys_UploadFile, object>> orderBy, bool isDesc = false, bool isPageNavStr = false);
        PageList<TResult> GetPageList<TResult>(Expression<Func<Sys_UploadFile, bool>> where, PageSet pageSet, Expression<Func<Sys_UploadFile, TResult>> obj, Expression<Func<Sys_UploadFile, object>> orderBy, bool isDesc = false, bool isPageNavStr = false);
        bool IsAny(Expression<Func<Sys_UploadFile, bool>> where);
        Sys_UploadFile AddUser(Sys_UploadFile model);
        bool UpdateUser(Sys_UploadFile model);
        bool UpdateColumns(Expression<Func<Sys_UploadFile, object>> columns, Sys_UploadFile user, bool isLock);
        bool DeleteUserBy(int id);
        Sys_UploadFile GetEntity(Expression<Func<Sys_UploadFile, bool>> where);
        Sys_UploadFile InsertAndGetEntity(Sys_UploadFile user);
        int Insert(Sys_UploadFile model, bool isReturnId);


        int InsertList(List<Sys_UploadFile> insertList);
        bool DeleteById(int id);
        int DeleteByIdArray(params int[] idArr);
        List<TResult> GetList<TResult>(Expression<Func<Sys_UploadFile, bool>> where, Expression<Func<Sys_UploadFile, TResult>> obj, Expression<Func<Sys_UploadFile, object>> orderBy, bool isDesc = false);
        string UploadImg(IFormFile file, string path, string imgUrl, string ip, int userId);
    }
}
