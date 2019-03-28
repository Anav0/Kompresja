using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kompresja
{
    public class RandomTextGenerator
    {
        public string GenerateRandomString(int size)
        {
            var builder = new StringBuilder();
            var rnd = new Random();

            for (int i = 0; i < size; i++)
                builder.Append((char)rnd.Next(32, 127));

            return builder.ToString();
        }

        public string GenerateRandomStringWithEqualProb(int size, string symbols)
        {
            var builder = new StringBuilder();
            var rnd = new Random();
            var chars = symbols.Split(' ');

            for (int i = 0; i < size; i++)
            {
                builder.Append(chars[rnd.Next(0, chars.Length)]);
            }

            return builder.ToString();
        }

        public List<string> GenerateWords(int numberOfWords, int wordLength)
        {
            char[] alphabet = Enumerable.Range('a', 26).Select(x => (char)x).ToArray();

            var prob = 1d / alphabet.Length;
            var rnd = new Random();
            var output = new List<string>();
            var raw = "";
            var word = "";
            Console.WriteLine(prob);

            for (int i = 0; i < numberOfWords; i++)
            {
                do
                {
                    for (int j = 0; j < alphabet.Length; j++)
                    {
                        if (rnd.NextDouble() <= prob) word += alphabet[j];
                        if (word.Length >= wordLength) break;
                    }
                } while (word.Length < wordLength);

                Console.WriteLine(word);
                output.Add(word);
                raw += word;
                word = "";
            }

            return output;
        }

    }
}
