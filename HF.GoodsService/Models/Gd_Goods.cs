using System;
using System.Linq;
using System.Text;

namespace HF.GoodsService.Models
{
    ///<summary>
    ///商品信息
    ///</summary>
    public partial class Gd_Goods
    {
        public Gd_Goods()
        {


        }
        /// <summary>
        /// Desc:商品主键（G+门店简称+商品类目简称+创建日期+时分秒+当天该门店增加的商品数量）
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Id { get; set; }

        /// <summary>
        /// Desc:商品状态：1暂存，2删除，3审核驳回，5审核中，10通过审核
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public int Status { get; set; }

        /// <summary>
        /// Desc:类目Id（gd_goodscategory主键）
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int CategoryId { get; set; }

        /// <summary>
        /// Desc:卖家Id
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public int SellerId { get; set; }

        /// <summary>
        /// Desc:商品标题
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Title { get; set; }

        /// <summary>
        /// Desc:品牌Id
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int BrandId { get; set; }

        /// <summary>
        /// Desc:商品单价
        /// Default:0.0000
        /// Nullable:False
        /// </summary>           
        public decimal Price { get; set; }

        /// <summary>
        /// Desc:市场价格（原价）
        /// Default:0.0000
        /// Nullable:False
        /// </summary>           
        public decimal PriceMarket { get; set; }

        /// <summary>
        /// Desc:运费
        /// Default:0.0000
        /// Nullable:False
        /// </summary>           
        public decimal PriceFreight { get; set; }

        /// <summary>
        /// Desc:偏远地区附加运费
        /// Default:0.0000
        /// Nullable:False
        /// </summary>           
        public decimal PriceFreightAdditional { get; set; }

        /// <summary>
        /// Desc:商品库存
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int Stock { get; set; }

        /// <summary>
        /// Desc:规格数量
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public int SpecsNumber { get; set; }

        /// <summary>
        /// Desc:规格类型Json，如：[{Key："颜色","Value":["黑色","红色"]},{Key:"内存","Value":["16G","32G"]}}]
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string SpecsJson { get; set; }

        /// <summary>
        /// Desc:偏远地区
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string RemoteRegion { get; set; }

        /// <summary>
        /// Desc:图片缩略图
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string ImgSmall { get; set; }

        /// <summary>
        /// Desc:商品描述
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
        /// Desc:修改日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? ModifyDate { get; set; }

    }
}
