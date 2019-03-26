using System;
using System.Reflection;
using BryanWu.Domain.Model;
using Common;
using Common.Enums;
using Common.Repository;

namespace Bryan.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            WordsHelper.why_pinyin = 1;
            WordsHelper.last_hz_no = 0;
            string words = "伍昭华是的法规";
            DBManager.ConnectionString = "server=193.112.41.35;Database=hfmall;Uid=xintaoke;Pwd=xtk920706;SslMode=None";
            //注册redis
            RedisRepository._connectionString = "193.112.41.35:6379,allowAdmin=true,password=wzh920706";
            RedisRepository._databaseKey = "hfmall";
            //var user = new Sys_User();
            //user.HeadImgUrl = "sgdsg";

            //foreach (var info in user.GetType().GetProperties())
            //{
            //    Console.WriteLine(info.Name);
            //}

            //RedisTest.HashMo();
            //RedisTest.HashDelete();
            //RedisTest.Sys_UserHash();

            //RedisTest.ListInsert();
            //RedisTest.GetList();

            //Console.WriteLine(WordsHelper.hz_pinyin(words, "", true));
            //Console.WriteLine(WordsHelper.index_hz_pinyin(words, "", true));
            //.net core 使用StackExchange.Redis文档

            ObjectTest.SetAndGet();

            Console.ReadKey();
        }
    }
}
