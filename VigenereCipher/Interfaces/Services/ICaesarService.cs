namespace VigenereCipher.Interfaces.Services
{
    internal interface ICaesarService
    {
        public (string message, int shift) Hack(string cipher);
    }
}
