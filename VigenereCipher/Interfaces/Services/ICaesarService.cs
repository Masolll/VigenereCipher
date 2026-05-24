using VigenereCipher.Models;

namespace VigenereCipher.Interfaces.Services
{
    internal interface ICaesarService
    {
        public CaesarHackData Hack(string cipher);
    }
}
