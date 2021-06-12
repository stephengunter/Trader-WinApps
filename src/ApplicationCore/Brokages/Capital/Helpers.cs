using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Brokages.Capital
{
    public static class CapitalHelpers
    {
        public static string ResolveFullAccountId(this IEnumerable<string> val)
        {
            var parts = val.ToArray();
            return parts[1] + parts[3];
        }


    }
    
}
