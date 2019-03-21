using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kompresja
{
    public class Probability
    {

        public Dictionary<char, float> GetLettersOccurrences(string sentence)
        {
            Dictionary<char, float> occurances = new Dictionary<char, float>();

            var splited = sentence.ToCharArray();

            for (int i = 0; i < splited.Length; i++)
            {
                if (!occurances.Keys.ToList().Exists(x => x == splited[i]))
                    occurances.Add(splited[i], 1);
                else
                    occurances[splited[i]]++;
            }
            return occurances;
        }

        public List<Letter> GetLetters(string sentence)
        {
            List<Letter> result = new List<Letter>();
            
            var occurances = GetLettersOccurrences(sentence);

            var totalNumberOfCharacters = occurances.Values.Sum();

            for(int i = 0; i < occurances.Count; i++)
            {
                var letter = new Letter();
                letter.Name = occurances.Keys.ElementAt(i).ToString();
                letter.HowMany = occurances.Values.ElementAt(i);
                letter.Probability = (occurances.Values.ElementAt(i) / totalNumberOfCharacters);

                result.Add(letter);
            }
            return result;
        }

        public double CalculateEntropy(string sentence)
        {
            var occurances = GetLettersOccurrences(sentence);

            var entropy = 0.0d;
            foreach(var pair in occurances)
            {
                var prob = (double)pair.Value / sentence.Length;
                entropy -= prob * (Math.Log(prob) / Math.Log(2));
            }

            return Math.Round(entropy, 3);
        }
    }
}
