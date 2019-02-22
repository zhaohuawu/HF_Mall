using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class FileHelperParam
    {
        /// <summary>
        /// 应用类型
        /// </summary>
        public AppType appType { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public FileType fileType { get; set; }

        /// <summary>
        /// 图片类型
        /// </summary>
        public ImageType imageType { get; set; }

        /// <summary>
        /// 用户ID，当用户上传文件时此值为必填
        /// </summary>
        public Guid guid { get; set; }
    }

    /// <summary>
    /// 上传文件到临时目录公用方法返回值
    /// </summary>
    public class UploadPath
    {
        /// <summary>
        /// 上传至临时目录相对路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 上传至临时目录绝对路径
        /// </summary>
        public string SavePath { get; set; }
    }

    public enum AppType
    {
        Users = 1,
        Holiday = 2,
        Hotel = 3,
    }

    public enum FileType
    {
        image = 1,
        doc = 2,
        video = 3,
    }

    public enum ImageType
    {
        logo = 1,
        product = 2,
        supplybuy = 3,
    }
}
