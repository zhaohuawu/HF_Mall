using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bryan.Common
{
    public class RandomHelper
    {
        /// <summary>
        /// 简单6位随即数字密码
        /// </summary>
        /// <returns></returns>
        public static int GetSimpleNum()
        {
            Random rnd = new Random();
            int i = rnd.Next(100000, 999999);
            return i;
        }
        
        public static string GetExchangeCoder(int num = 8)
        {
            return GenerateRandom(num);
        }

        private static char[] constant =
        {
           '0','1','2','3','4','5','6','7','8','9',
           'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
        };

        public static string GenerateRandomNum(int Length)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(62);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(10)]);
            }
            return newRandom.ToString();
        }
        public static string GenerateRandom(int Length)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(62);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(36)]);
            }
            return newRandom.ToString();
        }



        public static string CreateRandomCode(int num = 6)
        {
            string code = DateTime.Now.ToString("yyMMddHHmmss");
            code += GetSimpleNum();
            return code;
        }


        /// <summary>
        /// 简单6位随即数字密码
        /// </summary>
        /// <returns></returns>
        public static int GetRandomNum(int num = 9)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());//短时间内生成随机会会重复，加上Guid.NewGuid().GetHashCode()就不会重复了
            int i = rnd.Next(10000000, 999999999);
            return i;
        }


        /// <summary>
        /// 得到0~1之间的值（maxpercent：最大允许值的可能性，Count：此值越大，则大于maxpercent的值的可能性越小）
        /// </summary>
        /// <param name="maxpercent"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public static decimal GetRandomPercent(double maxpercent=0.5,int Count=1)
        {
            Guid guid = Guid.NewGuid();

            int key1 = guid.GetHashCode();
            int key2 = unchecked((int)DateTime.Now.Ticks);

            int seed = unchecked(key1 * key2);
            Random rnd = new Random(seed);
            //Random rnd = new Random();
            int i =rnd.Next(1, 100);
            while (Count > 1)// && i >(int) (maxpercent * 100)
            {
                i = rnd.Next(1, i);
                Count--;
            }
            return i / 100.0M;
        }

        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <param name="strPwChar">传入生成的随机字符串可以使用哪些字符</param>
        /// <param name="intlen">传入生成的随机字符串的长度</param>
        public static string MakePassword(int intlen)
        {
            string strPwChar = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            string strRe = "";
            int iRandNum;
            Random rnd = new Random();
            for (int i = 0; i < intlen; i++)
            {
                iRandNum = rnd.Next(strPwChar.Length);
                strRe += strPwChar[iRandNum];
            }
            return strRe;
        } 
    }
}
