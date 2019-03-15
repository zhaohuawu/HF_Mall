using BryanWu.Domain.Model;
using Common;
using Common.Enums;
using Common.Interface;
using Common.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bryan.Test
{
    public class RedisTest
    {
        private static RedisRepository _redis = new RedisRepository();

        public static void HashMo()
        {
            //Console.WriteLine(_redis.HashSet(RedisKeysEnum.TestHash.ToString(), "1", "sdgsg"));
            Console.WriteLine(_redis.HashGet(RedisKeysEnum.TestHash.ToString(), "1"));
        }

        public static void HashDelete()
        {
            Console.WriteLine(_redis.HashDelete(RedisKeysEnum.TestHash.ToString(), "1"));
            Console.WriteLine(_redis.HashGet(RedisKeysEnum.TestHash.ToString(), "1"));
        }
        
        public static void Sys_UserHash()
        {
            var rep = new SqlsugarRepository();
            var userList = rep.SqlSugarDB.Queryable<Sys_User>().ToList();
            //foreach (var item in userList)
            //{
            //    _redis.HashSet(RedisKeysEnum.sys_userHash.ToString(), item.Id + ":Id", item.Id);
            //    _redis.HashSet(RedisKeysEnum.sys_userHash.ToString(), item.Id + ":CrtDate", item.CrtDate);
            //    _redis.HashSet(RedisKeysEnum.sys_userHash.ToString(), item.Id + ":CrtUser", item.CrtUser);
            //    _redis.HashSet(RedisKeysEnum.sys_userHash.ToString(), item.Id + ":Email", item.Email);
            //    _redis.HashSet(RedisKeysEnum.sys_userHash.ToString(), item.Id + ":HeadImgUrl", item.HeadImgUrl);
            //    _redis.HashSet(RedisKeysEnum.sys_userHash.ToString(), item.Id + ":LastIp", item.LastIp);
            //    _redis.HashSet(RedisKeysEnum.sys_userHash.ToString(), item.Id + ":LastLogDate", item.LastLogDate);
            //    _redis.HashSet(RedisKeysEnum.sys_userHash.ToString(), item.Id + ":Mobile", item.Mobile);
            //    _redis.HashSet(RedisKeysEnum.sys_userHash.ToString(), item.Id + ":ModifyDate", item.ModifyDate);
            //    _redis.HashSet(RedisKeysEnum.sys_userHash.ToString(), item.Id + ":Password", item.Password);
            //    _redis.HashSet(RedisKeysEnum.sys_userHash.ToString(), item.Id + ":RealName", item.RealName);
            //    _redis.HashSet(RedisKeysEnum.sys_userHash.ToString(), item.Id + ":RoleId", item.RoleId);
            //    _redis.HashSet(RedisKeysEnum.sys_userHash.ToString(), item.Id + ":Sex", item.Sex);
            //    _redis.HashSet(RedisKeysEnum.sys_userHash.ToString(), item.Id + ":Status", item.Status);
            //    _redis.HashSet(RedisKeysEnum.sys_userHash.ToString(), item.Id + ":UserName", item.UserName);
            //}


            foreach (var item in userList)
            {
                //_redis.HashSet<Sys_User>(RedisKeysEnum.sys_userHash.ToString(), item.Id.ToString(), item);
                Console.WriteLine(_redis.HashGet(RedisKeysEnum.SysUserHash.ToString(), item.Id.ToString()));
            }
            
        }

        public static void ListInsert()
        {
            var rep = new SqlsugarRepository();
            var userList = rep.SqlSugarDB.Queryable<Sys_User>().ToList();
            foreach (var item in userList)
            {
                Console.WriteLine(_redis.ListRightPush<Sys_User>(RedisKeysEnum.TestList.ToString(), item));
            }
        }

        public static void GetList()
        {
            var list = _redis.GetListRange<Sys_User>(RedisKeysEnum.TestList.ToString(), 2, 4);
            foreach (var item in list)
            {
                Console.WriteLine(JSONHelper.Seriallize(item));
            }
        }
    }
}
