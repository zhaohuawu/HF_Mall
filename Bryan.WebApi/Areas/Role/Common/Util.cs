using Bryan.WebApi.Areas.Role.Models;
using BryanWu.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bryan.WebApi.Areas.Role.Common
{
    public class Util
    {
        /// <summary>
        /// 菜单递归
        /// </summary>
        /// <param name="menuTree"></param>
        /// <param name="list"></param>
        /// <param name="pid"></param>
        public static void MenuTree(ReturnMenuTree menuTree, List<Sys_AdminMenu> list, int pid)
        {
            var childenList = list.Where(p => p.Pid == pid).ToList();

            for (int i = 0; i < childenList.Count; i++)
            {
                ReturnMenuTree mTree = new ReturnMenuTree();
                mTree.Id = childenList[i].Id;
                mTree.MenuName = childenList[i].MenuName;
                MenuTree(mTree, list, childenList[i].Id);
                menuTree.Children.Add(mTree);
            }
        }
    }
}
