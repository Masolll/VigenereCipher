using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VigenereCipher.Service
{
    internal interface ICipherService
    {
        public string Encrypt(string message, string key);
        public (string message, string key) Decrypt(string cipher);
    }
}
