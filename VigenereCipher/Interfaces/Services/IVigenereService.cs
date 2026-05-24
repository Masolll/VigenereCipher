namespace VigenereCipher.Interfaces.Services
{
    internal interface IVigenereService
    {
        public string Encrypt(string message, string key);
        public string Decrypt(string cipher, string key);
    }
}
