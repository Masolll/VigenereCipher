using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VigenereCipher.Interfaces.Services;

namespace VigenereCipher.Services
{
    internal class TextFormatterService : ITextFormatterService
    {
        private readonly string _alphabet = "абвгдежзийклмнопрстуфхцчшщъыьэюя";
        public string ClearText(string text)
        {
            return new string(text.ToLower()
                .Replace('ё', 'е')
                .Where(e => _alphabet.Contains(e))
                .ToArray());
        }

        public string SplitTextIntoGroups(string text, int groupLength)
        {
            var resultText = new StringBuilder();

            for (var i = 0; i < text.Length; i++)
            {
                resultText.Append(text[i]);
                if ((i + 1) % groupLength == 0 && i + 1 < text.Length)
                {
                    resultText.Append(' ');
                }
            }
            return resultText.ToString();
        }
    }
}
