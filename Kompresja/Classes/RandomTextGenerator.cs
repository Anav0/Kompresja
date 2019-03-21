using System;
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

    }
}
