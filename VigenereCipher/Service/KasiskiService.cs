using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VigenereCipher.Service
{
    internal class KasiskiService : IKasiskiService
    {
        public (string message, string key) Hack(string cipher)
        {
            var keyLength = getKeyLength(cipher);
            
            return (message: "hack message", key: "hack key");
        }

        private int getKeyLength(string cipher)
        {
            var allNGramms = getAllNGramms(cipher, 5, 2);
            var allNGrammsDistances = getNGrammsDistances(allNGramms);
            return 0;
        }

        //нахожу все N-граммы от максимального N до минимального N
        private Dictionary<string, List<int>> getAllNGramms(string text, int maxN, int minN)
        {
            var allNGramms = new Dictionary<string, List<int>>();
            for (var n = maxN; n >= minN; n--)
            {
                var currentNGramms = getNGramms(text, n);
                allNGramms = allNGramms.Union(currentNGramms).ToDictionary();
            }
            return allNGramms;
        }

        private Dictionary<string, List<int>> getNGrammsDistances(Dictionary<string, List<int>> nGramms)
        {
            var nGrammsDistances = new Dictionary<string, List<int>>();
            foreach (var e in nGramms)
            {
                var grammaString = e.Key;
                var grammaIndices = e.Value;
                for (var i = 0; i < grammaIndices.Count - 1; i++)
                {
                    for (var j = i + 1; j < grammaIndices.Count; j++)
                    {
                        var currentDistance = grammaIndices[j] - grammaIndices[i];
                        //Расстояние между н-граммами должно быть >= длине самой н-граммы, чтобы избежать наложений н-грамм друг на друга.
                        if (currentDistance >= grammaString.Length)
                        {
                            if (nGrammsDistances.ContainsKey(grammaString))
                                nGrammsDistances[grammaString].Add(currentDistance);
                            else
                                nGrammsDistances[grammaString] = new List<int>() { currentDistance };
                        }
                    }
                }
            }
            return nGrammsDistances;
        }

        private Dictionary<string, List<int>> getNGramms(string text, int n)
        {
            var nGramms = new Dictionary<string, List<int>>();
            //Прохожу скользящим окном длиною n
            for (var i = 0; i < text.Length - n + 1; i++)
            {
                var gramma = text.Substring(i, n);
                if (nGramms.ContainsKey(gramma))
                    nGramms[gramma].Add(i);
                else
                    nGramms[gramma] = new List<int>() { i };
            }
            //Оставляю n-граммы которые встречаются более чем 1 раз
            nGramms = nGramms.Where(e => e.Value.Count > 1).ToDictionary<string, List<int>>();
            return nGramms;
        }

        private List<int> getDivisors(int number)
        {
            var divisors = new List<int>();
            for (var divisor = 1; divisor * divisor <= number; divisor++)
            {
                if (number % divisor == 0)
                {
                    divisors.Add(divisor);
                    if (divisor * divisor != number)
                    {
                        divisors.Add(number / divisor);
                    }
                }
            }
            return divisors;
        }
    }
}
