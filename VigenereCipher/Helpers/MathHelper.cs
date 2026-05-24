using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VigenereCipher.Helpers
{
    public static class MathHelper
    {
        public static List<int> GetDivisors(int number)
        {
            var divisors = new List<int>();
            for (var divisor = 2; divisor * divisor <= number; divisor++)
            {
                if (number % divisor == 0)
                {
                    divisors.Add(divisor);
                    if (divisor * divisor != number)
                    {
                        divisors.Add(number / divisor);
                    }
                }
            }
            return divisors;
        }
    }
}
