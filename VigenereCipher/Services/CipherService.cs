using System.Linq;
using System.Text;
using VigenereCipher.Interfaces.Services;
using VigenereCipher.Models;

namespace VigenereCipher.Services
{
    internal class CipherService : ICipherService
    {
        private readonly string _alphabet = "абвгдежзийклмнопрстуфхцчшщъыьэюя";
        private ITextFormatterService _textFormatterService = new TextFormatterService();

        public string Encrypt(string message, string key)
            => EncryptOrDecrypt(message, key, CipherMode.Encrypt);

        public string Decrypt(string cipher, string key)
        {
            var decryptedText = EncryptOrDecrypt(cipher, key, CipherMode.Decrypt);
            return _textFormatterService.SplitTextIntoGroups(decryptedText, 5);
        }
            

        private string EncryptOrDecrypt(string text, string key, CipherMode mode)
        {
            var preparedText = _textFormatterService.ClearText(text);
            var preparedKey = _textFormatterService.ClearText(key);

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
    }
}
