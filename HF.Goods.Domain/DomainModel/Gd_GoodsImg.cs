using System;
using System.Linq;
using System.Text;

namespace HF.Goods.Domain.DomainModel
{
    ///<summary>
    ///商品图片
    ///</summary>
    public partial class Gd_GoodsImg
    {
        public Gd_GoodsImg()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Id { get; set; }

        /// <summary>
        /// Desc:商品Id
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string GoodsId { get; set; }

        /// <summary>
        /// Desc:图片类型
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int TypeId { get; set; }

        /// <summary>
        /// Desc:上传文件ID（sys_uploadfile主键）
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int UploadFileId { get; set; }

        /// <summary>
        /// Desc:排序
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int Orders { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string ImgUrl { get; set; }

        /// <summary>
        /// Desc:创建时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime CrtDate { get; set; }

        /// <summary>
        /// Desc:是否删除
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int IsDelete { get; set; }

    }
}
