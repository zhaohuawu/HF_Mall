using System;
using System.Linq;
using System.Text;

namespace HF.Goods.Domain.DomainModel
{
    ///<summary>
    ///商品详细信息
    ///</summary>
    public partial class Gd_GoodsInfo
    {
        public Gd_GoodsInfo()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string GoodsId { get; set; }

        /// <summary>
        /// Desc:商品详情
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string GoodsDetails { get; set; }

        /// <summary>
        /// Desc:商品参数，如：{“品牌”:"新百伦",“尺码”:"38/39/40/41"}
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string GoodsParamsJson { get; set; }

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
