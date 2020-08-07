using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FAQ2_Api.Models
{
    public static class Extensions
    {
        public static bool MySearch (this string searchIn, string Search, StringComparison comparisonMode)
        {
            return (searchIn.IndexOf(Search, comparisonMode) != -1);
        }
    }
}
