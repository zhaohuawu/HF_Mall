using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bryan.WebApi.Areas.Product.Models.Category
{
    public class FromAddCategory
    {
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
    }
}
