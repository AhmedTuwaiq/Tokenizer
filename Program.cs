using System;
using System.Collections.Generic;

namespace Tokenizer
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> hashTags = TokenReader.getHashTag("#test ###");
            List<string> mentions = TokenReader.getMention("@test @test");

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("==========[ hashtags ]==========");

            foreach (var val in hashTags)
            {
                Console.WriteLine(val);
            }

            Console.WriteLine("hashtags count: " + hashTags.Count);

            Console.WriteLine("==========[ mentions ]==========");

            foreach (var val in mentions)
            {
                Console.WriteLine(val);
            }

            Console.WriteLine("mentions count: " + mentions.Count);
        }
    }
}
