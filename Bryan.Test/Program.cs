using System;
using System.Reflection;
using BryanWu.Domain.Model;
using Bryan.Common;
using Bryan.Common.Enums;
using Bryan.Common.Repository;
using System.Collections.Generic;
using BryanWu.Domain;

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
            //var csredis = new CSRedis.CSRedisClient("193.112.41.35:6379,allowAdmin=true,password=wzh920706");
            //RedisHelper.Initialization(csredis);
            //string result = "{\"code\":\"200\",\"msg\":\"kkk\"}";
            //var statecode = ((Newtonsoft.Json.Linq.JValue)((Newtonsoft.Json.Linq.JProperty)((Newtonsoft.Json.Linq.JContainer)Newtonsoft.Json.JsonConvert.DeserializeObject(result)).First).Value).Value;
            //Console.WriteLine(statecode.ToString());

            //var kkList = new List<string>() { "kkk", "kkk", "kkk" };

            Console.WriteLine(Enum.IsDefined(typeof(UploadStatusEnum), 1));
            Console.WriteLine(Enum.IsDefined(typeof(UploadStatusEnum), "1"));
            Console.WriteLine(Enum.IsDefined(typeof(UploadStatusEnum), "可删除")); 
            
            var user = new Sys_User();
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

            //ObjectTest.SetAndGet();

            Console.ReadKey();
        }
    }
}
