using System;
using System.Linq;
using System.Text;

namespace BryanWu.Domain.Model
{
    ///<summary>
    ///后台系统管理员
    ///</summary>
    public partial class Sys_User
    {
        public Sys_User()
        {
            this.Status = 1;
            this.RoleId = 1;
            this.Sex = 0;

        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Id { get; set; }

        /// <summary>
        /// Desc:用户状态(1:正常，5:注销)
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public int Status { get; set; }

        /// <summary>
        /// Desc:账号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string UserName { get; set; }

        /// <summary>
        /// Desc:用户真实姓名
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string RealName { get; set; }

        /// <summary>
        /// Desc:密码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Password { get; set; }

        /// <summary>
        /// Desc:最后登录IP
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string LastIp { get; set; }

        /// <summary>
        /// Desc:角色ID
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public int RoleId { get; set; }

        /// <summary>
        /// Desc:创建时间
        /// Default:CURRENT_TIMESTAMP
        /// Nullable:False
        /// </summary>           
        public DateTime CrtDate { get; set; }

        /// <summary>
        /// Desc:最后登录时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? LastLogDate { get; set; }

        /// <summary>
        /// Desc:头像
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string HeadImgUrl { get; set; }

        /// <summary>
        /// Desc:性别
        /// Default:0
        /// Nullable:True
        /// </summary>           
        public int? Sex { get; set; }

        /// <summary>
        /// Desc:联系号码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Mobile { get; set; }

        /// <summary>
        /// Desc:创建人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string CrtUser { get; set; }

    }
}
