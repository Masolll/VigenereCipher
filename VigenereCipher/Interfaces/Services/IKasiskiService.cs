using VigenereCipher.Models;

namespace VigenereCipher.Interfaces.Services
{
    internal interface IKasiskiService
    {
        public bool TryHack(string cipher, out VigenereHackData vigenereHackData);
    }
}
