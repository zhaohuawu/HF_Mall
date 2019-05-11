using Bryan.WebApi.Models.SysRole;
using Bryan.BaseService.Model;
using System.Collections.Generic;
using System.Linq;

namespace Bryan.WebApi.Common
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
                mTree.Children = new List<ReturnMenuTree>();
                MenuTree(mTree, list, childenList[i].Id);
                menuTree.Children.Add(mTree);
            }
        }
        /// <summary>
        /// 菜单递归
        /// </summary>
        /// <param name="menuTree"></param>
        /// <param name="list"></param>
        /// <param name="pid"></param>
        /// <param name="btnList"></param>
        public static void RoleMenuTree(ReturnRoleMenuTree menuTree, List<Sys_AdminMenu> list, int pid, List<Sys_AdminMenuBtn> btnList)
        {
            var childenList = list.Where(p => p.Pid == pid).ToList();

            for (int i = 0; i < childenList.Count; i++)
            {
                var child = childenList[i];
                var chilBtnList = btnList.Where(p => p.MenuId == child.Id).ToList();

                ReturnRoleMenuTree mTree = new ReturnRoleMenuTree();
                mTree.Id = child.Id;
                mTree.MenuName = child.MenuName;
                mTree.Type = "url";
                mTree.TypeId = "url_" + child.Id;
                mTree.Children = new List<ReturnRoleMenuTree>();
                foreach (var btn in chilBtnList)
                {
                    ReturnRoleMenuTree btnTree = new ReturnRoleMenuTree();
                    btnTree.Id = btn.Id;
                    btnTree.MenuName = btn.Name;
                    btnTree.Type = "btn";
                    btnTree.TypeId = "btn_" + btn.Id;
                    btnTree.Children = new List<ReturnRoleMenuTree>();
                    mTree.Children.Add(btnTree);
                }
                RoleMenuTree(mTree, list, child.Id, btnList);
                menuTree.Children.Add(mTree);
            }
        }
    }
}
