using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bryan.WebApi.Models;
using Bryan.WebApi.Models.AppSettings;
using BryanWu.Domain.Interface;
using BryanWu.Domain;
using Common;
using Common.Interface;
using Common.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Bryan.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : BaseController
    {
        private ISys_UploadFileService _uploadFileService;
        private UploadSettings _uploadSettings;
        public UploadController(ISys_UploadFileService uploadFileService, IOptions<UploadSettings> uploadSettings, ILog log)
        {
            _log = log;
            this._uploadFileService = uploadFileService;
            this._uploadSettings = uploadSettings.Value;
        }

        #region 
        /// <summary>
        /// 上传头像
        /// </summary>
        /// <returns></returns>
        [HttpPost("UploadAvatar")]
        public IActionResult UploadAvatar()
        {
            string code = "000000";
            var returnUpload = new ReturnUpload();
            try
            {
                IFormFileCollection Files = Request.Form.Files;
                if (Files == null || Files.Count == 0)
                {
                    code = "100051";
                }
                else
                {
                    string uploadStr = _uploadFileService.UploadImg(Files[0], _uploadSettings.path, _uploadSettings.avatar, HttpContext.GetIp(), _userId);
                    returnUpload = JSONHelper.Dseriallize<ReturnUpload>(uploadStr);
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                code = "100050";
            }
            return ReturnJson(code, returnUpload);
        }

        /// <summary>
        /// 修改图片状态为可删除
        /// </summary>
        /// <param name="filePath">图片路径</param>
        /// <returns></returns>
        [HttpPost("updatestatus")]
        public async Task<IActionResult> DeleteUploadStatusAsync(string filePath)
        {
            if (filePath.StartsWith("upload/"))
            {
                await Task.Run(() =>
                {
                    _uploadFileService.UpdateUploadStatusAsync(UploadTypeEnum.image, filePath, UploadStatusEnum.可删除);
                });

                return ReturnJson("000000");
            }
            else
                return ReturnJson("000001");
        }

        #endregion

    }
}