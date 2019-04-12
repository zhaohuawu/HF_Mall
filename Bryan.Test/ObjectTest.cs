using Bryan.Test.DTO;
using Bryan.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Bryan.Test
{
    public class ObjectTest
    {
        public static void SetAndGet()
        {
            var list = new List<UserDto>();
            for (int i = 0; i < 1000; i++)
            {
                list.Add(new UserDto { Id = GUIDHelper.GetUniqueId(), Status = i % 2 == 0 ? 1 : 5 });
            }
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var rList = new ConcurrentQueue<UserDto>(list);
            Parallel.ForEach(rList, model =>
             {
                 Console.WriteLine(JSONHelper.Seriallize(model));
             });
            sw.Stop();
            Console.WriteLine("Parallel.ForEach：" + sw.ElapsedMilliseconds);

            //sw.Restart();
            //// var rList = new ConcurrentQueue<UserDto>(list);
            //foreach (var model in list)
            //{
            //    Console.WriteLine(JSONHelper.Seriallize(model));
            //}
            //sw.Stop();
            //Console.WriteLine("foreach：" + sw.ElapsedMilliseconds);
        }
    }
}
