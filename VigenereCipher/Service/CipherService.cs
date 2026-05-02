using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VigenereCipher.Service
{
    internal class CipherService : ICipherService
    {
        private readonly string _alphabet = "абвгдежзийклмнопрстуфхцчшщъыьэюя";
        public string Encrypt(string message, string key)
        {
            var preparedMessage = prepareTextBeforeEncrypt(message);
            var preparedKey = prepareTextBeforeEncrypt(key);

            var cipher = new StringBuilder();
            for (var i = 0; i < preparedMessage.Length; i++)
            {
                var sourceCharacter = preparedMessage[i];
                var keyCharacter = preparedKey[i % preparedKey.Length];
                
                var sourceCharacterIndexInAlphabet = _alphabet.IndexOf(sourceCharacter);
                var keyCharacterIndexInAlphabet = _alphabet.IndexOf(keyCharacter);

                var cipherCharacterIndexInAlphabet = (sourceCharacterIndexInAlphabet + keyCharacterIndexInAlphabet) % _alphabet.Length;
                var cipherCharacter = _alphabet[cipherCharacterIndexInAlphabet];
                cipher.Append(cipherCharacter);
            }
            return cipher.ToString();
        }

        public (string message, string key) Decrypt(string cipher)
        {
            return ("decryption", "some key");
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
