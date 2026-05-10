namespace VigenereCipher.Interfaces.Services
{
    internal interface ICipherService
    {
        public string Encrypt(string message, string key);
        public string Decrypt(string cipher, string key);
    }
}
