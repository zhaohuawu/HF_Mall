using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BryanWu.Domain
{
    #region Enum
    /// <summary>
    /// 上传文件的类型
    /// </summary>
    public enum UploadTypeEnum
    {
        [Description("图片")]
        image = 1,
        [Description("文件")]
        file = 5,
        [Description("Excel")]
        excel = 15
    }
    public enum UploadStatusEnum
    {
        未使用 = 1,
        可删除 = 3,
        已删除 = 15,
        使用中 = 20
    }
    #endregion
}
