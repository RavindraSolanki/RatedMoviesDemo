using System;
using System.Collections.Generic;
using System.Text;

namespace RatedMoviesDemo.Api.Tests.Extensions
{
    public static class DecimalExtensions
    {
        public static bool IsInDescendingOrEqualsOrder(this IEnumerable<decimal> decimalNumbers)
        {
            var enumerator = decimalNumbers.GetEnumerator();

            var firstOrPrevious = decimal.MaxValue;
            while(enumerator.MoveNext())
            {
                if (firstOrPrevious < enumerator.Current)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
