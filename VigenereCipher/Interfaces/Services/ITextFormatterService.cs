using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VigenereCipher.Interfaces.Services
{
    internal interface ITextFormatterService
    {
        public string ClearText(string text);
        public string SplitTextIntoGroups(string text, int groupLength);
    }
}
