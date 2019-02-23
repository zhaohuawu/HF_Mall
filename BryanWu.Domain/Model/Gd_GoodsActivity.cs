using System;
using System.Linq;
using System.Text;

namespace BryanWu.Domain.Model
{
    ///<summary>
    ///商品参加的活动
    ///</summary>
    public partial class Gd_GoodsActivity
    {
        public Gd_GoodsActivity()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Id { get; set; }

        /// <summary>
        /// Desc:商品ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string GoodsId { get; set; }

        /// <summary>
        /// Desc:活动类型：1拼团，5秒杀
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public int TypeId { get; set; }

        /// <summary>
        /// Desc:活动ID
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int ActivityId { get; set; }

        /// <summary>
        /// Desc:活动价格
        /// Default:0.0000
        /// Nullable:False
        /// </summary>           
        public decimal Price { get; set; }

        /// <summary>
        /// Desc:活动商品库存
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int Stock { get; set; }

        /// <summary>
        /// Desc:拼团活动成团人数
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int GroupMin { get; set; }

        /// <summary>
        /// Desc:活动开始时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// Desc:活动结束时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? EndTime { get; set; }

    }
}
