using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VigenereCipher.Helpers;
using VigenereCipher.Interfaces.Services;
using VigenereCipher.Models;

namespace VigenereCipher.Services
{
    internal class KasiskiService : IKasiskiService
    {
        private readonly string _alphabet = "абвгдежзийклмнопрстуфхцчшщъыьэюя";
        private readonly ICaesarService _caesarService = new CaesarService();
        private readonly ITextFormatterService _textFormatterService = new TextFormatterService();

        public bool TryHack(string cipher, out VigenereHackData vigenereHackData)
        {
            vigenereHackData = new VigenereHackData();
            cipher = _textFormatterService.ClearText(cipher);
            var keyLength = 0;

            if (!TryGetKeyLength(cipher, out keyLength))
            {
                vigenereHackData.ErrorMessage = "Не удалось определить длину ключа. Возможно, текст слишком короткий или не содержит повторяющихся фрагментов.";
                return false;
            } 

            var encryptedGroupsByKeyLength = new StringBuilder[keyLength];
            var decryptedGroupsByKeyLength = new string[keyLength];
            var keyString = new StringBuilder(keyLength);
            var message = new StringBuilder();

            //Формирую зашифрованные группы
            for (var i = 0; i < cipher.Length; i++)
            {
                if (encryptedGroupsByKeyLength[i % keyLength] == null)
                    encryptedGroupsByKeyLength[i] = new StringBuilder();

                encryptedGroupsByKeyLength[i % keyLength].Append(cipher[i]);
            }
            //Расшифровываю каждую группу и нахожу ключ
            for (var i = 0; i < keyLength; i++)
            {
                var caesarHackData = _caesarService.Hack(encryptedGroupsByKeyLength[i].ToString());
                decryptedGroupsByKeyLength[i] = caesarHackData.Message;
                //Сдвиг для текущей группы равен индексу буквы ключа в алфавите
                keyString.Append(_alphabet[caesarHackData.Shift]);
            }
            //Формирую общее расшифрованное сообщение из всех расшифрованных групп
            for (var i = 0; i < cipher.Length; i++)
            {
                var groupIndex = i % keyLength;
                var itemIndexInGroup = i / keyLength;
                var messageChar = decryptedGroupsByKeyLength[groupIndex][itemIndexInGroup];

                message.Append(messageChar);
            }
            var messageDividedIntoGroups = _textFormatterService.SplitTextIntoGroups(message.ToString(), 5);

            vigenereHackData.Message = messageDividedIntoGroups;
            vigenereHackData.Key = keyString.ToString();
            return true;
        }

        private bool TryGetKeyLength(string cipher, out int keyLength)
        {
            keyLength = 0;
            var nGramms = GetNGramms(cipher, 10, 3);
            var nGrammsDistances = GetNGrammsDistances(nGramms);
            var distances = nGrammsDistances.SelectMany(e => e.Value).ToList();
            var divisors = distances.Select(e => MathHelper.GetDivisors(e)).SelectMany(e => e).ToList();
            //словарь<делитель, частота>
            var divisorsAndFrequence = new Dictionary<int, int>();
            foreach(var e in divisors)
            {
                divisorsAndFrequence[e] = divisorsAndFrequence.GetValueOrDefault(e) + 1;
            }

            var mostPopularDivisors = divisorsAndFrequence.OrderByDescending(e => e.Value).ThenByDescending(e => e.Key).ToList();

            if (mostPopularDivisors.Count() == 0)
            {
                return false;
            }
            var likelyKeyLength = mostPopularDivisors.First().Key;
            foreach(var e in mostPopularDivisors)
            {
                //Если частота с которой встречается делитель близка к максимальной (не менее 80% от максимальной частоты)
                //то рассматриваю такой делитель как потенциально возможный. Среди таких делителей нахожу максимальный.
                if (e.Value >= mostPopularDivisors.First().Value * 0.8 && e.Key > likelyKeyLength)
                    likelyKeyLength = e.Key;
            }

            keyLength = likelyKeyLength;
            return true;
        }

        private Dictionary<string, List<int>> GetNGrammsDistances(Dictionary<string, List<int>> nGramms)
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
        private Dictionary<string, List<int>> GetNGramms(string text, int maxN, int minN)
        {
            var allNGramms = new Dictionary<string, List<int>>();
            for (var n = maxN; n >= minN; n--)
            {
                var currentNGramms = GetNGramms(text, n);
                allNGramms = allNGramms.Union(currentNGramms).ToDictionary();
            }
            return allNGramms;
        }

        private Dictionary<string, List<int>> GetNGramms(string text, int n)
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
    }
}
