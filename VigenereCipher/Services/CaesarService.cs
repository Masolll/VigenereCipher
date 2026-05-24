using System;
using System.Collections.Generic;
using System.Linq;
using VigenereCipher.Interfaces.Services;

namespace VigenereCipher.Services
{
    internal class CaesarService : ICaesarService
    {
        private readonly string _alphabet = "абвгдежзийклмнопрстуфхцчшщъыьэюя";

        private readonly Dictionary<char, double> _alphabetFrequency = new()
        {
            { 'а', 0.062 },
            { 'б', 0.014 },
            { 'в', 0.038 },
            { 'г', 0.013 },
            { 'д', 0.025 },
            { 'е', 0.072 },
            { 'ж', 0.007 },
            { 'з', 0.016 },
            { 'и', 0.062 },
            { 'й', 0.010 },
            { 'к', 0.028 },
            { 'л', 0.035 },
            { 'м', 0.026 },
            { 'н', 0.053 },
            { 'о', 0.090 },
            { 'п', 0.023 },
            { 'р', 0.040 },
            { 'с', 0.045 },
            { 'т', 0.053 },
            { 'у', 0.021 },
            { 'ф', 0.002 },
            { 'х', 0.009 },
            { 'ц', 0.003 },
            { 'ч', 0.012 },
            { 'ш', 0.006 },
            { 'щ', 0.003 },
            { 'ъ', 0.014 },
            { 'ы', 0.016 },
            { 'ь', 0.014 },
            { 'э', 0.003 },
            { 'ю', 0.006 },
            { 'я', 0.018 }
        };


        public (string message, int shift) Hack(string cipher) => LeastSquaresMethod(cipher);

        private (string message, int shift) LeastSquaresMethod(string cipher)
        {
            var minSumSquares = double.MaxValue;
            var resultShift = 0;
            // i - сдвиг. Перебираю все возможные сдвиги.
            for (var shift = 0; shift < _alphabet.Length; shift++)
            {
                var currentSumSquares = 0.0;
                for (var i = 0; i < _alphabet.Length; i++)
                {
                    var tableFrequency = _alphabetFrequency[_alphabet[i]];
                    var realFrequency = cipher.Count(e => e == _alphabet[(i + shift) % _alphabet.Length]) / (double)cipher.Length;
                    currentSumSquares += Math.Pow(tableFrequency - realFrequency, 2);
                }

                if (currentSumSquares < minSumSquares)
                {
                    minSumSquares = currentSumSquares;
                    resultShift = shift;
                }
            }

            var message = cipher.Select(e => _alphabet[(_alphabet.IndexOf(e) - resultShift + _alphabet.Length) % _alphabet.Length])
                .ToArray();
            return (message: new string(message), shift: resultShift);
        }
    }
}
