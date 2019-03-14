using System;
using Common;

namespace Bryan.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            WordsHelper.why_pinyin = 1;
            WordsHelper.last_hz_no = 0;
            string words = "伍昭华是的法规";
            Console.WriteLine(words);
            Console.WriteLine(WordsHelper.hz_pinyin(words, "", true));
            Console.WriteLine(WordsHelper.index_hz_pinyin(words, "", true));
           
            Console.ReadKey();
        }
    }
}
