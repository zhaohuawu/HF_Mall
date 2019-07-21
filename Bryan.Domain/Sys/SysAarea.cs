using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Bryan.Domain.Sys
{
    ///<summary>
    ///全国省市区
    ///</summary>
    [Table("Sys_Area")]
    public partial class SysArea
    {
           public SysArea(){


           }
           /// <summary>
           /// Desc:地址主键Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int Id {get;set;}

           /// <summary>
           /// Desc:地址父Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int Pid {get;set;}

           /// <summary>
           /// Desc:地址编码
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int Code {get;set;}

           /// <summary>
           /// Desc:地址名
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Name {get;set;}

           /// <summary>
           /// Desc:地址省市区
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string NameMerger {get;set;}

           /// <summary>
           /// Desc:地址简称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string NameShort {get;set;}

           /// <summary>
           /// Desc:地址省市区简称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string NameShortMerger {get;set;}

           /// <summary>
           /// Desc:级别
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int Levels { get;set;}

           /// <summary>
           /// Desc:电话编码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CityCode {get;set;}

           /// <summary>
           /// Desc:邮编
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZipCode {get;set;}

           /// <summary>
           /// Desc:地址拼音
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PinYin {get;set;}

           /// <summary>
           /// Desc:地址简拼
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string JianPin {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Letter {get;set;}

           /// <summary>
           /// Desc:经度
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Lng {get;set;}

           /// <summary>
           /// Desc:纬度
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Lat {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Remark {get;set;}

    }
}
