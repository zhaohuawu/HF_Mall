using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Common.Enums
{
    public enum RedisKeysEnum
    {
        [Description("hfmall默认关键字")]
        hfmall,
        [Description("String字符串测试")]
        TestString,
        [Description("Hash散列测试")]
        TestHash,
        [Description("List列表测试")]
        TestList,
        [Description("Set集合测试")]
        TestSet,
        [Description("Sorted Set有序集合测试")]
        TestSortedSet,
        [Description("User Table")]
        SysUserHash,
        [Description("接口结果返回码")]
        ReturnCodeHash,
    }
}
