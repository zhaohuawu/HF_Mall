using BryanWu.Domain.Interface;
using BryanWu.Domain.Model;
using Bryan.Common.Interface;
using Bryan.Common.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bryan.Common.Extension;

namespace BryanWu.Domain.Service
{
    public class Log_AdminService : ILog_AdminService
    {
        public IRepository _repository { get; set; }

        public Log_AdminService(IRepository repository)
        {
            _repository = repository;
        }

        public bool DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Log_Admin> GetAllList()
        {
            return _repository.GetList<Log_Admin>(n => true);
        }

        public Log_Admin GetEntity(Expression<Func<Log_Admin, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Log_Admin GetEntityById(int id)
        {
            return _repository.GetEntityById<Log_Admin>(id);
        }

        public List<Log_Admin> GetList(Expression<Func<Log_Admin, bool>> where, Expression<Func<Log_Admin, object>> orderBy, bool isDesc = false)
        {
            throw new NotImplementedException();
        }

        public TResult GetOneKey<TResult>(Expression<Func<Log_Admin, bool>> where, Expression<Func<Log_Admin, TResult>> filed)
        {
            throw new NotImplementedException();
        }

        public PageList<Log_Admin> GetPageList(Expression<Func<Log_Admin, bool>> where, PageSet pageSet, Expression<Func<Log_Admin, object>> orderBy, bool isDesc = false, bool isPageNavStr = false)
        {
            return _repository.GetPageList(where, pageSet, orderBy, isDesc, isPageNavStr);
        }

        /// <summary>
        /// 添加后台操作日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(Log_Admin model)
        {
            model.CrtDate = DateTime.Now;
            return _repository.Insert(model);
        }

        public bool IsAny(Expression<Func<Log_Admin, bool>> where)
        {
            throw new NotImplementedException();
        }

        public bool Update(Log_Admin model)
        {
            throw new NotImplementedException();
        }

        #region 业务相关
        /// <summary>
        /// 返回json数据
        /// </summary>
        /// <param name="logAdmin"></param>
        /// <returns></returns>
        public async void LogAdmin(Log_Admin logAdmin, HttpContext http)
        {
            await Task.Run(() =>
            {
                logAdmin.Ip = http.GetIp();
                logAdmin.Url = HttpContextExtension.GetAbsoluteUri(http.Request);
                logAdmin.CrtDate = DateTime.Now;
                _repository.Insert(logAdmin);
            });
        }
        #endregion
    }
}
