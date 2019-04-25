using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bryan.Common.Interface
{
    public interface IDBManager
    {
        SqlSugarClient GetClient();
    }
}
