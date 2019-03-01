using BryanWu.Domain.Interface;
using BryanWu.Domain.Model;
using Common;
using Common.Interface;
using Common.Repository;
using Microsoft.AspNetCore.Http;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BryanWu.Domain.Service
{
    public class Sys_UploadFileService : ISys_UploadFileService
    {
        public IRepository _repository { get; set; }
        private ILog _log;
        public Sys_UploadFileService(IRepository repository, ILog log)
        {
            _repository = repository;
            this._log = log;
        }

        #region Service
        public List<Sys_UploadFile> GetAllUser()
        {
            return _repository.SqlSugarDB.Queryable<Sys_UploadFile>().Where(p => true).OrderBy(p => p.Id, OrderByType.Desc).ToList();
        }

        public Sys_UploadFile GetUserById(int id)
        {
            return _repository.GetEntityById<Sys_UploadFile>(id);
        }

        public PageList<Sys_UploadFile> GetPageList(Expression<Func<Sys_UploadFile, bool>> where, PageSet pageSet, Expression<Func<Sys_UploadFile, object>> orderBy, bool isDesc = false, bool isPageNavStr = false)
        {
            return _repository.GetPageList(where, pageSet, orderBy, isDesc, isPageNavStr);
        }

        public PageList<TResult> GetPageList<TResult>(Expression<Func<Sys_UploadFile, bool>> where, PageSet pageSet, Expression<Func<Sys_UploadFile, TResult>> obj, Expression<Func<Sys_UploadFile, object>> orderBy, bool isDesc = false, bool isPageNavStr = false)
        {
            return _repository.GetPageList(where, pageSet, obj, orderBy, isDesc, isPageNavStr);
        }

        public Sys_UploadFile AddUser(Sys_UploadFile model)
        {
            return _repository.InsertAndGetEntity(model);
        }

        public bool UpdateUser(Sys_UploadFile model)
        {
            return _repository.Update(model) > 0; ;
        }

        public bool DeleteUserBy(int id)
        {
            return _repository.DeleteById<Sys_UploadFile>(id) > 0;
        }

        public Sys_UploadFile GetEntity(Expression<Func<Sys_UploadFile, bool>> where)
        {
            return _repository.GetEntity(where);
        }
        public Sys_UploadFile InsertAndGetEntity(Sys_UploadFile user)
        {
            return _repository.InsertAndGetEntity(user);
        }

        public bool UpdateColumns(Expression<Func<Sys_UploadFile, object>> columns, Sys_UploadFile user, bool isLock)
        {
            return _repository.UpdateColumns(columns, user, isLock) > 0;
        }

        public bool IsAny(Expression<Func<Sys_UploadFile, bool>> where)
        {
            return _repository.IsAny(where);
        }

        public int Insert(Sys_UploadFile model, bool isReturnId)
        {
            return _repository.Insert(model, isReturnId);
        }

        public int InsertList(List<Sys_UploadFile> insertList)
        {
            return _repository.InsertList(insertList);
        }

        public bool DeleteById(int id)
        {
            return _repository.DeleteById<Sys_UploadFile>(id) > 0;
        }

        public int DeleteByIdArray(params int[] idArr)
        {
            return _repository.DeleteByIdArray<Sys_UploadFile>(idArr);
        }

        public List<TResult> GetList<TResult>(Expression<Func<Sys_UploadFile, bool>> where, Expression<Func<Sys_UploadFile, TResult>> obj, Expression<Func<Sys_UploadFile, object>> orderBy, bool isDesc = false)
        {
            return _repository.GetList(where, obj, orderBy, isDesc);
        }
        #endregion

        #region 业务逻辑
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="file"></param>
        /// <param name="path"></param>
        /// <param name="imgUrl"></param>
        /// <param name="ip"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string UploadImg(IFormFile file, string path, string imgUrl, string ip, int userId)
        {
            var now = DateTime.Now;
            imgUrl = imgUrl + now.Year + "/" + now.Month + "/" + now.Day + "/";
            path = path + imgUrl;
            //文件名称
            string fileName = GUIDHelper.GetStringID() + ".png";
            imgUrl = imgUrl + fileName;
            Task.Run(() =>
            {
                FileHelper.UploadFileWithFormFile(file, path, path + fileName);
            });

            var uploadModel = new Sys_UploadFile();
            uploadModel.CrtDate = now;
            uploadModel.FileName = fileName;
            uploadModel.FilePath = imgUrl;
            uploadModel.FileSize = 0;
            uploadModel.FileType = "image/jpeg";
            uploadModel.Ip = ip;
            uploadModel.TypeId = (int)UploadTypeEnum.image;
            uploadModel.UserId = userId;
            uploadModel.Status = (int)UploadStatusEnum.未使用;
            uploadModel.Id = _repository.Insert(uploadModel, true);
            return JSONHelper.Seriallize(new { url = imgUrl, uploadId = uploadModel.Id });
        }

        /// <summary>
        /// 修改上传文件的状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="uEnum"></param>
        public async void UpdateUploadStatusAsync(int id, UploadStatusEnum uEnum)
        {
            var model = new Sys_UploadFile();
            model.Id = id;
            model.Status = (int)uEnum;
            await Task.Run(() =>
            {
                _repository.UpdateColumns(p => new { p.Status }, model);
            });
        }

        /// <summary>
        /// 修改上传文件的状态
        /// </summary>
        /// <param name="typeEnum"></param>
        /// <param name="filePath"></param>
        /// <param name="statusEnum"></param>
        public async void UpdateUploadStatusAsync(UploadTypeEnum typeEnum, string filePath, UploadStatusEnum statusEnum)
        {
            _log.Debug(typeEnum.ToString() + "--" + filePath + "---" + statusEnum.ToString() + "---" + (int)statusEnum);
            if (filePath.StartsWith("upload/"))
            {
                var lastIndex = filePath.LastIndexOf('/');
                var length = filePath.Length;
                string fileName = filePath.Substring(lastIndex + 1, filePath.Length - lastIndex - 1);
                int status = (int)statusEnum;
                int typeId = (int)typeEnum;
                await Task.Run(() =>
                {
                    _repository.SqlSugarDB.Updateable<Sys_UploadFile>().UpdateColumns(p => new Sys_UploadFile() { Status = status }).Where(p => p.FileName == fileName && p.TypeId == typeId).ExecuteCommand();
                });
            }
        }

        #endregion


    }

}
