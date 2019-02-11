using System;
using System.Collections.Generic;
using System.Text;

namespace RatedMoviesDemo.Api.Extensions
{
    public static class DecimalExtensions
    {
        public static decimal RoundToNearestPoint5(this decimal decimalNumber)
        {
            return Math.Round(decimalNumber * 2, MidpointRounding.AwayFromZero) / 2;
        }
    }
}
