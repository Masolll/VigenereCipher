using System.Collections.Generic;
using System.Linq;
using VigenereCipher.Interfaces.Services;

namespace VigenereCipher.Services
{
    internal class KasiskiService : IKasiskiService
    {
        private readonly ICaesarService _caesarService = new CaesarService();
        public (string message, string key) Hack(string cipher)
        {
            var keyLength = getKeyLength(cipher);
            var groupsByKeyLenght = new List<char>[keyLength];
            var frequencyCharsInGroups = new Dictionary<char, int>[keyLength];

            for (var i = 0; i < keyLength; i++)
            {
                groupsByKeyLenght[i] = new List<char>();
                frequencyCharsInGroups[i] = new Dictionary<char, int>();
            }

            for (var i = 0; i < cipher.Length; i++)
            {
                groupsByKeyLenght[i % keyLength].Add(cipher[i]);
                frequencyCharsInGroups[i % keyLength][cipher[i]] = frequencyCharsInGroups[i % keyLength].GetValueOrDefault(cipher[i]) + 1;
            }

            return (message: "hack message", key: $"{keyLength}");
        }

        private int getKeyLength(string cipher)
        {
            var nGramms = getNGramms(cipher, 10, 3);
            var nGrammsDistances = getNGrammsDistances(nGramms);
            var distances = nGrammsDistances.SelectMany(e => e.Value).ToList();
            var divisors = distances.Select(e => getDivisors(e)).SelectMany(e => e).ToList();
            //словарь<делитель, частота>
            var divisorsAndFrequence = new Dictionary<int, int>();
            foreach(var e in divisors)
            {
                divisorsAndFrequence[e] = divisorsAndFrequence.GetValueOrDefault(e) + 1;
            }

            var mostPopularDivisors = divisorsAndFrequence.OrderByDescending(e => e.Value).ThenByDescending(e => e.Key).ToList();
            var likelyKeyLength = mostPopularDivisors.First().Key;
            foreach(var e in mostPopularDivisors)
            {
                //Если частота с которой встречается делитель близка к максимальной (более 80% от максимальной частоты)
                //то рассматриваю такой делитель как потенциально возможный. Среди таких делителей нахожу максимальный.
                if (e.Value >= mostPopularDivisors.First().Value * 0.8 && e.Key > likelyKeyLength)
                    likelyKeyLength = e.Key;
            }
            return likelyKeyLength;
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

        //нахожу все N-граммы от максимального N до минимального N
        private Dictionary<string, List<int>> getNGramms(string text, int maxN, int minN)
        {
            var allNGramms = new Dictionary<string, List<int>>();
            for (var n = maxN; n >= minN; n--)
            {
                var currentNGramms = getNGramms(text, n);
                allNGramms = allNGramms.Union(currentNGramms).ToDictionary();
            }
            return allNGramms;
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
            for (var divisor = 2; divisor * divisor <= number; divisor++)
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
