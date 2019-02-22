﻿using System;
using System.Collections.Generic;

namespace Common.Infrastructure.Repository
{
    /// <summary>
    /// 分页参数设置
    /// </summary>
    public class PageSet
    {
        public PageSet()
        {
            this.PageIndex = 1;
            this.PageSize = 10;
        }

        public PageSet(int pageIndex,int pageSize)
        {
            this.PageIndex = PageIndex;
            this.PageSize = pageSize;
        }

        /// <summary>
        /// 当前页面序号
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页显示的记录数
        /// </summary>
        public int PageSize { get; set; }
    }
}
