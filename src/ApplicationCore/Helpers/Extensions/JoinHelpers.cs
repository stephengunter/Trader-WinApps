using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Helpers
{
    public static class JoinHelpers
    {
		public static List<string> SplitToList(this string val)
		{
			if (String.IsNullOrEmpty(val)) return new List<string>();
			return val.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
		}
		public static string JoinToString(this IEnumerable<string> list)
		{
			if (list.IsNullOrEmpty()) return "";
			return String.Join(",", list.Where(x => !String.IsNullOrEmpty(x)));
		}
		public static List<int> SplitToIntList(this string val)
		{
			if (String.IsNullOrEmpty(val)) return new List<int>();
			return val.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.ToInt()).ToList();
		}

		public static List<int> SplitToIds(this string val, char splitBy = ',')
		{
			var list = val.SplitToIntList();

			if (!list.IsNullOrEmpty()) list.RemoveAll(item => item == 0);

			return list;
		}

		public static string JoinToStringIntegers(this List<int> list, bool greaterThanZero = false)
		{
			if (greaterThanZero) list = list.Where(id => id > 0).ToList();
			return String.Join(",", list.Select(x => x.ToString()));
		}
	}
}
