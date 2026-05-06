using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VigenereCipher.Models;

namespace VigenereCipher.Service
{
    internal class CipherService : ICipherService
    {
        private readonly string _alphabet = "абвгдежзийклмнопрстуфхцчшщъыьэюя";

        public string Encrypt(string message, string key)
            => EncryptOrDecrypt(message, key, CipherMode.Encrypt);

        public string Decrypt(string cipher, string key)
            => EncryptOrDecrypt(cipher, key, CipherMode.Decrypt);

        public (string message, string key) Hack(string cipher)
        {
            getKeyLenght(cipher);
            return ("hack message", "hack key");
        }
        
        private string EncryptOrDecrypt (string text, string key, CipherMode mode)
        {
            var preparedText = prepareTextBeforeEncrypt(text);
            var preparedKey = prepareTextBeforeEncrypt(key);

            var resultText = new StringBuilder();
            for (var i = 0; i < preparedText.Length; i++)
            {
                var textCharacter = preparedText[i];
                var keyCharacter = preparedKey[i % preparedKey.Length];

                var textCharacterIndexInAlphabet = _alphabet.IndexOf(textCharacter);
                var keyCharacterIndexInAlphabet = _alphabet.IndexOf(keyCharacter);

                var resultTextCharacterIndexInAlphabet = 0;
                if (mode == CipherMode.Encrypt)
                {
                    resultTextCharacterIndexInAlphabet = (textCharacterIndexInAlphabet + keyCharacterIndexInAlphabet) % _alphabet.Length;
                }
                else
                {
                    resultTextCharacterIndexInAlphabet = (textCharacterIndexInAlphabet - keyCharacterIndexInAlphabet + _alphabet.Length) % _alphabet.Length;
                }
                var resultTextCharacter = _alphabet[resultTextCharacterIndexInAlphabet];
                resultText.Append(resultTextCharacter);
            }
            return resultText.ToString();
        }

        private int getKeyLenght(string cipher)
        {
            var cipherCopy = new string(cipher);
            var repeatitions = new int[cipher.Length];
            //двигаю копию шифровки и накладываю на исходную шифровку для поиска повторов
            for (var shift = 0; shift < cipher.Length; shift++)
            {
                for (var i = 0; i < cipher.Length; i++)
                {
                    if (cipher[i] == cipherCopy[(i + shift) % cipher.Length])
                    {
                        repeatitions[shift] += 1;
                    }
                }
            }

            var avarageCountRepeatitions = repeatitions.Average(e => e);
            var keyLenghtСandidates = new List<int>();
            //начинаю с 1 так как сдвиг 0 рассматривать нет смысла
            for (var i = 1; i < repeatitions.Length; i++)
            {
                //Если при сдвиге i количество повторов первышает среднее число повторений в 1.5 раза, то считаю что сдвиг возможно крает длине ключа
                if (repeatitions[i] > avarageCountRepeatitions * 1.5)
                    keyLenghtСandidates.Add(i);
            }


            return 0;
        }

        private string prepareTextBeforeEncrypt(string message)
        {
            var a = new string(message.Replace('ё', 'е')
            .ToLower()
            .Where(e => _alphabet.Contains(e))
            .ToArray());
            return a;
        }
    }
}
