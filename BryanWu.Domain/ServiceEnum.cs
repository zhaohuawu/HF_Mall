using System;
using System.Collections.Generic;
using System.Text;

namespace BryanWu.Domain
{
    #region Enum
    public enum UploadTypeEnum
    {
        image = 1,
        file = 5,
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
