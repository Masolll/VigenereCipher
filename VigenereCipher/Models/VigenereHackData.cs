using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VigenereCipher.Models
{
    internal class VigenereHackData
    {
        public string Message { get; set; }
        public string Key { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
