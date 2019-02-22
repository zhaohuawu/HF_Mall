using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace Common.Infrastructure.Repository
{
    /// <summary>
    /// 分页后的数据集合
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public class PageList<T>
    {
        public PageList()
        {
            DataList = new List<T>();
        }

        public PageList<T> TranFromList(List<T> data, PageSet pageset)
        {
            this.PageIndex = pageset.PageIndex;
            this.RecordCount = data.Count;
            //this.SetPageCount();
            this.DataList = data.Skip((pageset.PageIndex - 1) * pageset.PageSize).Take(pageset.PageSize).ToList<T>();
            return this;
        }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int RecordCount { get; set; }
        /// <summary>
        /// 总页面数
        /// </summary>
        public int PageCount { get; set; }
        /// <summary>
        /// 分页html代码
        /// </summary>
        public string PageNavStr { get; set; }

        public string HrefString { get; set; }

        public void SetQueryString()
        {
            HrefString = "";
            //TODO需要获取当前路径
            NameValueCollection names = new NameValueCollection(); //HttpContext.Current.Request.QueryString;
            for (int i = 0; i < names.Count; i++)
            {
                string key = names.Keys[i];
                string val = names[key];
                if (key != null && key.ToLower() != "page")
                {
                    if (HrefString != "")
                        HrefString += "&";
                    HrefString += key + "=" + HttpUtility.UrlEncode(val);
                }
            }

            if (HrefString != "")
                HrefString = "?" + HrefString + "&";
            else
                HrefString = "?";
        }

        public void SetPageCount(int PageSize)
        {
            if (PageSize <= 0) this.PageCount = 1;
            var c = (int)(this.RecordCount / PageSize);
            if (this.RecordCount % PageSize == 0) this.PageCount = c;
            this.PageCount = c + 1;
        }

        public void SetPageNavStr()
        {
            if (RecordCount > 0)
            {
                SetQueryString();

                StringBuilder pageNavStr = new StringBuilder();

                #region New
                //pageNavStr.Append("<div class=\"layui-table-page\">");
                //pageNavStr.Append("  <div id=\"layui-table-page1\">");
                //pageNavStr.Append("    <div class=\"layui-box layui-laypage layui-laypage-default\" id=\"layui-laypage-10\">");
                //pageNavStr.Append("      <span class=\"layui-laypage-count\">").Append("共<span id=\"pageCount\">" + PageCount + "</span>页/" + RecordCount + "条").Append("</span>");

                //if (PageIndex <= PageCount)
                //{
                //    //上一页
                //    //pageNavStr.Append("<li class=\"paginate_button page-item previous");

                //    string preDisableStr = "";
                //    string preHref = "javascripte:;";
                //    if (PageIndex == 1)
                //    {
                //        preDisableStr = " layui-disabled";
                //        //pageNavStr.Append(" disabled\">");
                //        //pageNavStr.Append("<a href=\"").Append("javascripte:");
                //    }
                //    else
                //    {
                //        preDisableStr = "";
                //        preHref = HrefString + "page=" + (PageIndex - 1);
                //        //pageNavStr.Append("\">");
                //        //pageNavStr.Append("<a href=\"").Append(HrefString).Append("page=").Append(PageIndex - 1);
                //    }
                //    //pageNavStr.Append("\" aria-label=\"Previous\" class=\"page-link\"><span aria-hidden=\"true\">&laquo;</span></a>");
                //    //pageNavStr.Append("</li>");
                //    pageNavStr.Append("      <a href=\"" + preHref + "\" class=\"layui-laypage-prev" + preDisableStr + "\" data-page=\"" + (PageIndex - 1) + "\">上一页</a>");

                //    //选页
                //    #region 页码的显示算法
                //    int startPage = 1;//每页显示5页数据，每次是5的整数倍
                //    if (PageIndex % 5 > 0)
                //        startPage = (PageIndex / 5) * 5 + 1;
                //    else
                //        startPage = (PageIndex / 5 - 1) * 5 + 1;

                //    int endPage = startPage + 4;//非最后一个5的整数倍循环
                //    if (PageCount < 5)//总是小于5的循环
                //    {
                //        startPage = 1;
                //        endPage = PageCount;
                //    }

                //    int pageTTT = PageCount % 5;
                //    if (PageCount >= 5 && pageTTT > 0)
                //    {
                //        if (PageCount - startPage < 5)  //最后一个5的整数倍循环
                //            endPage = PageCount - pageTTT + 1;
                //    }
                //    #endregion

                //    for (int i = startPage; i <= endPage; i++)
                //    {
                //        if (i == PageIndex)
                //        {
                //            //pageNavStr.Append("<li class=\"paginate_button page-item active\"><a class=\"page-link\" href=\"").Append(HrefString).Append("page=").Append(i).Append("\">").Append(i).Append("</a></li>");
                //            pageNavStr.Append("      <span class=\"layui-laypage-curr\"><em class=\"layui-laypage-em\"></em><em>" + i + "</em></span>");
                //        }
                //        else
                //        {
                //            //pageNavStr.Append("<li class=\"paginate_button page-item\"><a class=\"page-link\" href=\"").Append(HrefString).Append("page=").Append(i).Append("\">").Append(i).Append("</a></li>");
                //            pageNavStr.Append("      <a href=\"" + HrefString + "page=" + i + "\" data-page=\"" + i + "\">" + i + "</a>");
                //        }
                //    }

                //    //下一页
                //    //pageNavStr.Append("<li class=\"paginate_button page-item next");
                //    string nextDisableStr = "";
                //    string nextHref = "javascripte:;";
                //    if (PageIndex >= PageCount)
                //    {
                //        nextDisableStr = " layui-disabled";
                //        //pageNavStr.Append(" disabled\">");
                //        //pageNavStr.Append("<a href=\"").Append("javascripte:");
                //    }
                //    else
                //    {
                //        nextDisableStr = "";
                //        nextHref = HrefString + "page=" + (PageIndex + 1);
                //        //pageNavStr.Append("\">");
                //        //pageNavStr.Append("<a href=\"").Append(HrefString).Append("page=").Append(PageIndex + 1);
                //    }
                //    //pageNavStr.Append("\" aria-label=\"Next\" class=\"page-link\"><span aria-hidden=\"true\">&raquo;</span></a>");
                //    //pageNavStr.Append("</li>");
                //    pageNavStr.Append("      <a href=\"" + nextHref + "\" class=\"layui-laypage-next" + nextDisableStr + "\" data-page=\"" + (PageIndex + 1) + "\">下一页</a>");

                //}

                //pageNavStr.Append("      <a href=\"" + HrefString + "page=" + PageIndex + "\" data-page=\"" + PageIndex + "\" class=\"layui-laypage-refresh\">");
                //pageNavStr.Append("         <i class=\"layui-icon layui-icon-refresh\"></i>");
                //pageNavStr.Append("      </a>");
                //pageNavStr.Append("      <span class=\"layui-laypage-skip\">到第<input type=\"number\" name=\"pageSkip\" min=\"1\" value=\"" + PageIndex + "\" class=\"layui-input\">页");
                //pageNavStr.Append("          <a href=\"" + HrefString + "page=" + PageIndex + "\" class=\"layui-laypage-btn\" id=\"pageSkipSure\">确定</a>");
                //pageNavStr.Append("      </span>");

                //pageNavStr.Append("    </div>");
                //pageNavStr.Append("  </div>");
                //pageNavStr.Append("</div>");
                #endregion

                #region Old
                pageNavStr.Append("<div class=\"row\">");//1
                pageNavStr.Append("  <div class=\"col-sm-12 col-md-5\">");
                pageNavStr.Append("    <div class=\"dataTables_info\" id=\"example1_info\" role=\"status\" aria-live=\"polite\">");//2
                pageNavStr.Append("      第<b>").Append(PageIndex).Append("</b>页/共<b>").Append(PageCount).Append("</b>页 (共<b>").Append(RecordCount).Append("</b>条)");//3
                pageNavStr.Append("    </div>");
                pageNavStr.Append("  </div>");

                pageNavStr.Append("  <div class=\"col-sm-12 col-md-7\">");//2
                pageNavStr.Append("    <div class=\"dataTables_paginate paging_simple_numbers\" id=\"example1_paginate\">");//3
                pageNavStr.Append("      <ul class=\"pagination\">");//4

                if (PageIndex <= PageCount)
                {
                    //上一页
                    pageNavStr.Append("<li class=\"paginate_button page-item previous");
                    if (PageIndex == 1)
                    {
                        pageNavStr.Append(" disabled\">");
                        pageNavStr.Append("<a href=\"").Append("javascripte:");
                    }
                    else
                    {
                        pageNavStr.Append("\">");
                        pageNavStr.Append("<a href=\"").Append(HrefString).Append("page=").Append(PageIndex - 1);
                    }
                    pageNavStr.Append("\" aria-label=\"Previous\" class=\"page-link\"><span aria-hidden=\"true\">&laquo;</span></a>");
                    pageNavStr.Append("</li>");

                    //选页
                    int startPage = 1;//每页显示5页数据，每次是5的整数倍
                    if (PageIndex % 5 > 0)
                        startPage = (PageIndex / 5) * 5 + 1;
                    else
                        startPage = (PageIndex / 5 - 1) * 5 + 1;

                    int endPage = startPage + 4;//非最后一个5的整数倍循环
                    if (PageCount <= 5)//总是小于5的循环
                    {
                        startPage = 1;
                        endPage = PageCount;
                    }

                    int pageTTT = PageCount % 5;
                    if (PageCount > 5 && pageTTT > 0)
                    {
                        if (PageCount - startPage < 5)  //最后一个5的整数倍循环
                            endPage = startPage + pageTTT - 1;
                    }

                    for (int i = startPage; i <= endPage; i++)
                    {
                        if (i == PageIndex)
                        {
                            pageNavStr.Append("<li class=\"paginate_button page-item active\"><a class=\"page-link\" href=\"").Append(HrefString).Append("page=").Append(i).Append("\">").Append(i).Append("</a></li>");
                        }
                        else
                        {
                            pageNavStr.Append("<li class=\"paginate_button page-item\"><a class=\"page-link\" href=\"").Append(HrefString).Append("page=").Append(i).Append("\">").Append(i).Append("</a></li>");
                        }
                    }

                    //下一页
                    pageNavStr.Append("<li class=\"paginate_button page-item next");
                    if (PageIndex >= PageCount)
                    {
                        pageNavStr.Append(" disabled\">");
                        pageNavStr.Append("<a href=\"").Append("javascripte:");
                    }
                    else
                    {
                        pageNavStr.Append("\">");
                        pageNavStr.Append("<a href=\"").Append(HrefString).Append("page=").Append(PageIndex + 1);
                    }
                    pageNavStr.Append("\" aria-label=\"Next\" class=\"page-link\"><span aria-hidden=\"true\">&raquo;</span></a>");
                    pageNavStr.Append("</li>");

                }

                pageNavStr.Append("    </ul>");//4
                pageNavStr.Append("  </div>");//3
                pageNavStr.Append("</div>");//2

                pageNavStr.Append("</div>");//1
                #endregion

                PageNavStr = pageNavStr.ToString();
            }
        }
        /// <summary>
        /// 分页数据
        /// </summary>
        public List<T> DataList { get; set; }
        /// <summary>
        /// 统计项
        /// </summary>
        public object TotalData { get; set; }


    }
}
