using System;
using System.Linq;
using System.Text;

namespace BryanWu.Domain.Model
{
    ///<summary>
    ///记录后台操作日志
    ///</summary>
    public partial class Log_Admin
    {
        public Log_Admin()
        {

        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Id { get; set; }

        /// <summary>
        /// Desc:日志类型
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int TypeId { get; set; }

        /// <summary>
        /// Desc:创建人ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int CrtUserId { get; set; }

        /// <summary>
        /// Desc:创建人账号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string CrtUserName { get; set; }

        /// <summary>
        /// Desc:影响数据的ID
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public string OtherId { get; set; }

        /// <summary>
        /// Desc:描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Remark { get; set; }

        /// <summary>
        /// Desc:url
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Url { get; set; }

        /// <summary>
        /// Desc:ip
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Ip { get; set; }

        /// <summary>
        /// Desc:
        /// Default:CURRENT_TIMESTAMP
        /// Nullable:False
        /// </summary>           
        public DateTime CrtDate { get; set; }

    }
}
