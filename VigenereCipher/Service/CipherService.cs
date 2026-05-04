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

        //Encrypt и Decrypt абсолютно одиннаковые, даже формулу в Encrypt можно использовать такую же как и в Decrypt. Нужен рефакторинг
        public string Encrypt(string message, string key)
        {
            var preparedMessage = prepareTextBeforeEncrypt(message);
            var preparedKey = prepareTextBeforeEncrypt(key);

            var cipher = new StringBuilder();
            for (var i = 0; i < preparedMessage.Length; i++)
            {
                var messageCharacter = preparedMessage[i];
                var keyCharacter = preparedKey[i % preparedKey.Length];
                
                var messageCharacterIndexInAlphabet = _alphabet.IndexOf(messageCharacter);
                var keyCharacterIndexInAlphabet = _alphabet.IndexOf(keyCharacter);

                var cipherCharacterIndexInAlphabet = (messageCharacterIndexInAlphabet + keyCharacterIndexInAlphabet) % _alphabet.Length;
                var cipherCharacter = _alphabet[cipherCharacterIndexInAlphabet];
                cipher.Append(cipherCharacter);
            }
            return cipher.ToString();
        }

        public string Decrypt(string cipher, string key)
        {
            var preparedCipher = prepareTextBeforeEncrypt(cipher);
            var preparedKey = prepareTextBeforeEncrypt(key);

            var message = new StringBuilder();
            for (var i = 0; i < preparedCipher.Length; i++)
            {
                var cipherCharacter = preparedCipher[i];
                var keyCharacter = preparedKey[i % preparedKey.Length];

                var cipherCharacterIndexInAlphabet = _alphabet.IndexOf(cipherCharacter);
                var keyCharacterIndexInAlphabet = _alphabet.IndexOf(keyCharacter);

                var messageCharacterIndexInAlphabet = (cipherCharacterIndexInAlphabet - keyCharacterIndexInAlphabet + _alphabet.Length) % _alphabet.Length;
                var messageCharacter = _alphabet[messageCharacterIndexInAlphabet];
                message.Append(messageCharacter);
            }
            return message.ToString();
        }

        public (string message, string key) Hack(string cipher)
        {
            return ("hack message", "hack key");
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
