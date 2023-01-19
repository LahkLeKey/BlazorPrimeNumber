// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Index.razor.cs" company="Kyle Halek">
// Copyright 2023 Kyle Halek
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </copyright>
//
// <summary>
//   The index.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace BlazorPrimeNumber.Pages
{
    #region

    using System.Diagnostics.CodeAnalysis;

    #endregion

    /// <summary>
    ///     The index.
    /// </summary>
    public partial class Index
    {
        /// <summary>
        ///     The knownPrimes up to int.MaxValue
        /// </summary>
        private readonly IEnumerable<int> knownPrimes = Enumerable
            .Range(2, int.MaxValue - 1) // Count between 2 - 2147483646
            .Where(
                number => Enumerable.Range(2, (int) Math.Sqrt(number) - 1) // -1 to avoid out of bounds.
                    .All(divisor => number % divisor != 0)); // Prime test

        /// <summary>
        ///     The number of knownPrimes.
        /// </summary>
        private int numberOfPrimes = DefaultSet.Count();

        /// <summary>
        ///     The prime numbers.
        /// </summary>
        private IEnumerable<int>? primeNumbers = new List<int>(DefaultSet.ToList());

        /// <summary>
        ///     The prime numbers.
        /// </summary>
        private IEnumerable<long>? testNumbers =>
            new List<long>
            {
                long.MinValue,
                int.MinValue,
                -2,
                -1,
                0,
                1,
                2,
                int.MaxValue,
                long.MaxValue,
            };

        /// <summary>
        ///     The default set.
        /// </summary>
        private static IEnumerable<int> DefaultSet =>
            new SortedSet<int>
                {
                    2,
                    3,
                    5,
                    7,
                    11,
                    13,
                    17,
                    19,
                    23,
                    29,
                    31,
                    37,
                    41,
                    43,
                    47,
                    53,
                    59,
                    61,
                    67,
                    71,
                    73,
                    79,
                    83,
                    89,
                    97,
                    101
                };

        /// <summary>
        /// The is prime edge case.
        /// </summary>
        /// <param name="number">
        /// The number.
        /// </param>
        /// <param name="isPrime">
        /// The is prime.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool IsPrimeEdgeCase(long number, out bool isPrime)
        {
            switch (number)
            {
                // 1 is not prime
                case 1:
                {
                    isPrime = false;
                    return isPrime;
                }

                // 2 is prime
                case 2:
                {
                    isPrime = true;
                    return isPrime;
                }
            }

            isPrime = false;
            return isPrime;
        }

        // ReSharper disable once InconsistentNaming
        /// <summary>
        /// The is prime.
        /// </summary>
        /// <param name="number">
        /// The number.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool isPrime(long number)
        {
            return number >= 2 && this.IsPrime(number);
        }

        /// <summary>
        /// The is prime.
        /// </summary>
        /// <param name="number">
        /// The number.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool IsPrime(long number)
        {
            if (number > int.MaxValue)
            {
                return this.IsPrimeSlow((int) (number % int.MaxValue));
            }

            return Enumerable
                .Range(
                    2,
                    (int) Math.Sqrt(number)
                    - 1) // Square root of any prime number Sqrt(n) is always less than or equal to n/2.
                         // So, we only need to check up to n/2 for factors.  This is a huge optimization.
                .All(divisor => number % divisor > 0);

            // Prime test
        }

        /// <summary>
        /// The is prime async.
        /// </summary>
        /// <param name="number">
        /// The number.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [SuppressMessage(
            "StyleCop.CSharp.ReadabilityRules",
            "SA1101:PrefixLocalCallsWithThis",
            Justification = "Reviewed. Suppression is OK here.")]
        private bool IsPrimeSlow(long number)
        {
            if (IsPrimeEdgeCase(number, out var isPrime))
                return isPrime;

            // even numbers are not prime
            if (number % 2 == 0)
                return false;

            // check odd numbers
            for (long i = 3; i < number; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }

        /// <summary>
        /// The set prime numbers.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        private void SetPrimeNumbers(int obj)
        {
            this.numberOfPrimes = obj;
            this.primeNumbers = this.knownPrimes.Take(this.numberOfPrimes);
        }
    }
}