using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Bryan.Test.DTO
{
    public class UserDto
    {
        private static Dictionary<int, string> _StatusDic = new Dictionary<int, string>();
        public string Id { get; set; }
        public int Status { get; set; }
        public string StatusName
        {
            get
            {
                if (_StatusDic.Count == 0)
                {
                    _StatusDic = EnumHelper.GetDescriptionsOfEnum<SysUserStatusEnum>();
                    Console.WriteLine("账号状态类型数：" + _StatusDic.Count);
                }

                if (_StatusDic.ContainsKey(Status))
                    return _StatusDic[Status];
                else
                    return Status.ToString();
            }
        }
    }

    public enum SysUserStatusEnum
    {
        [Description("正常")]
        Normal = 1,
        [Description("注销")]
        Logout = 5,
    }
}
