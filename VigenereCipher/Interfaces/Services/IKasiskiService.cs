namespace VigenereCipher.Interfaces.Services
{
    internal interface IKasiskiService
    {
        public (string message, string key) Hack(string cipher);
    }
}
