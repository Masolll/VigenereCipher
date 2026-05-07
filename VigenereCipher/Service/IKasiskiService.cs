using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VigenereCipher.Service
{
    internal interface IKasiskiService
    {
        public (string message, string key) Hack(string cipher);
    }
}
