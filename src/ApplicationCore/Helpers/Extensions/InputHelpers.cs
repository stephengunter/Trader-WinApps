using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Helpers
{
    public static class InputHelpers
    {
        public static string GetTrimedValue(this string input) => String.IsNullOrEmpty(input) ? "" : input.Trim();

    }
}
