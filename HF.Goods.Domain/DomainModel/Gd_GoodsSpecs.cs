using System;
using System.Linq;
using System.Text;

namespace HF.Goods.Domain.DomainModel
{
    ///<summary>
    ///商品规格
    ///</summary>
    public partial class Gd_GoodsSpecs
    {
        public Gd_GoodsSpecs()
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
        /// Desc:商品规格json数据，如：{"颜色":"金色","内存":"32G"}
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string SpecsJosn { get; set; }

        /// <summary>
        /// Desc:规格库存
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int Stock { get; set; }

        /// <summary>
        /// Desc:规格缩略图
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SkuImgUrl { get; set; }

        /// <summary>
        /// Desc:创建时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime CrtDate { get; set; }

        /// <summary>
        /// Desc:修改日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? ModifyDate { get; set; }

        /// <summary>
        /// Desc:规格价格
        /// Default:0.0000
        /// Nullable:False
        /// </summary>           
        public decimal Price { get; set; }

        /// <summary>
        /// Desc:
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int IsDelete { get; set; }

    }
}
