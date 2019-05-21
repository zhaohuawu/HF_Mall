using System;
using System.Reflection;
using Bryan.Common;
using Bryan.Common.Enums;
using Bryan.Common.Repository;
using System.Collections.Generic;
using Bryan.BaseService;
using Bryan.BaseService.Model;
using System.Threading.Tasks;

namespace Bryan.Test
{
    class Program
    {
        private static bool _noPayFlag = true; //抽奖团标示
        static void Main(string[] args)
        {
            WordsHelper.why_pinyin = 1;
            WordsHelper.last_hz_no = 0;
            DBManager.ConnectionString = "server=193.112.41.35;Database=hfmall;Uid=xintaoke;Pwd=xtk920706;SslMode=None";
            //注册redis
            //var csredis = new CSRedis.CSRedisClient("193.112.41.35:6379,allowAdmin=true,password=wzh920706");
            //RedisHelper.Initialization(csredis);
            //string result = "{\"code\":\"200\",\"msg\":\"kkk\"}";
            //var statecode = ((Newtonsoft.Json.Linq.JValue)((Newtonsoft.Json.Linq.JProperty)((Newtonsoft.Json.Linq.JContainer)Newtonsoft.Json.JsonConvert.DeserializeObject(result)).First).Value).Value;
            //Console.WriteLine(statecode.ToString());

            //var kkList = new List<string>() { "kkk", "kkk", "kkk" };

            //Console.WriteLine(Enum.IsDefined(typeof(UploadStatusEnum), 1));
            //Console.WriteLine(Enum.IsDefined(typeof(UploadStatusEnum), "1"));
            //Console.WriteLine(Enum.IsDefined(typeof(UploadStatusEnum), "可删除"));

            var list = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
            Parallel.ForEach(list, async item =>
            {
                if (_noPayFlag)
                {
                    _noPayFlag = false;
                    Console.WriteLine(_noPayFlag);
                    try
                    {

                        for (int i = 0; i < 10000; i++)
                        {
                            await Task.Run(() =>
                            {
                                Console.WriteLine(i);
                            });
                        }
                        //_noPayFlag = true;
                        Console.WriteLine("KK");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"处理最后5分钟还未支付的订单失败：{ex.Message}");
                    }
                    finally
                    {
                        _noPayFlag = true;
                        Console.WriteLine(_noPayFlag);

                    }
                }
            });



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
