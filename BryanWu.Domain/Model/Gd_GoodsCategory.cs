using System;
using System.Linq;
using System.Text;

namespace BryanWu.Domain.Model
{
    ///<summary>
    ///商品类目
    ///</summary>
    public partial class Gd_GoodsCategory
    {
        public Gd_GoodsCategory()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Id { get; set; }

        /// <summary>
        /// Desc:类目父Id
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Pid { get; set; }

        /// <summary>
        /// Desc:类目名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Name { get; set; }

        /// <summary>
        /// Desc:类目级别
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Level { get; set; }

        /// <summary>
        /// Desc:排序
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Orders { get; set; }

        /// <summary>
        /// Desc:类目缩写
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Abbreviation { get; set; }

        /// <summary>
        /// Desc:是否商城显示
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public string IsShow { get; set; }

        /// <summary>
        /// Desc:图片路径
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string ImgUrl { get; set; }

        /// <summary>
        /// Desc:备注
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Remark { get; set; }

        /// <summary>
        /// Desc:创建时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime CrtDate { get; set; }

        /// <summary>
        /// Desc:修改时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? ModifyDate { get; set; }

    }
}
