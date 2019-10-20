using System;
using System.Threading.Tasks;
using Bryan.Base.Models;
using Bryan.Base.Models.AppSettings;
using Bryan.BaseService.Interface;
using Bryan.BaseService;
using Bryan.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Bryan.Common.Extension;
using Bryan.MicroService;
using System.IO;

namespace Bryan.Base.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UploadController : BaseController
    {
        private ISys_UploadFileService _uploadFileService;
        private UploadSettings _uploadSettings;
        public UploadController(ISys_UploadFileService uploadFileService, IOptions<UploadSettings> uploadSettings, ILogger<UploadController> log)
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
        [HttpPost]
        [ProducesResponseType(typeof(ReturnUpload), 200)]
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
                    string uploadStr = _uploadFileService.UploadImg(Files[0], _uploadSettings.path, _uploadSettings.avatar, HttpContext.GetIp(), GetJwtIEntity().UserId);
                    returnUpload = JSONHelper.Dseriallize<ReturnUpload>(uploadStr);
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                code = "100050";
            }
            return ReturnJson(code, returnUpload);
        }

        /// <summary>
        /// 修改图片状态为可删除
        /// </summary>
        /// <param name="filePath">图片路径</param>
        /// <returns></returns>
        [HttpPost]
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