using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Bryan.Domain.Sys
{
    ///<summary>
    ///文件上传记录
    ///</summary>
    [Table("Sys_UploadFile")]
    public partial class SysUploadFile
    {
        public SysUploadFile()
        {

            this.TypeId = 0;
            this.Status = 1;
        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Id { get; set; }

        /// <summary>
        /// Desc:文件类型
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int TypeId { get; set; }

        /// <summary>
        /// Desc:上传者Id
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int UserId { get; set; }

        /// <summary>
        /// Desc:文件名
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FileName { get; set; }

        /// <summary>
        /// Desc:上传路径
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FilePath { get; set; }

        /// <summary>
        /// Desc:上传文件的大小
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int FileSize { get; set; }

        /// <summary>
        /// Desc:上传文件类别
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string FileType { get; set; }

        /// <summary>
        /// Desc:图片宽度
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? ImgWidth { get; set; }

        /// <summary>
        /// Desc:图片高度
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? ImgHeight { get; set; }

        /// <summary>
        /// Desc:上传时ip
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Ip { get; set; }
        /// <summary>
        /// 图片状态：1未用，3可删除，15已删除，20使用中
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Desc:上传时间
        /// Default:CURRENT_TIMESTAMP
        /// Nullable:False
        /// </summary>           
        public DateTime CrtDate { get; set; }

    }
}
