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

        private string EncryptOrDecrypt(string text, string key, CipherMode mode)
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
