using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ApplicationCore.Helpers
{
	public static class CommonHelpers
	{
		public static bool HasValue(this string text) => !String.IsNullOrEmpty(text);

		public static bool EqualTo(this string val, string other) => String.Compare(val, other, true) == 0;

		public static bool CaseInsensitiveContains(this string text, string value)
		{
			StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase;
			if (text == null) return false;
			if (value == null) return false;
			return text.IndexOf(value, stringComparison) >= 0;
		}

		public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
		{
			if (enumerable == null)
			{
				return true;
			}
			var collection = enumerable as ICollection<T>;
			if (collection != null)
			{
				return collection.Count < 1;
			}
			return !enumerable.Any();
		}

		public static bool HasItems<T>(this IEnumerable<T> enumerable) => !IsNullOrEmpty(enumerable);

		public static IList<string> GetKeywords(this string input)
		{
			if (String.IsNullOrWhiteSpace(input) || String.IsNullOrEmpty(input)) return null;
			return input.Split(new string[] { "-", " ", "," }, StringSplitOptions.RemoveEmptyEntries);

		}

		public static string ReverseString(this string str)
		{
			return String.IsNullOrEmpty(str) ? string.Empty : new string(str.ToCharArray().Reverse().ToArray());
		}
		public static int ToInt(this string str)
		{
			int value = 0;
			if (!int.TryParse(str, out value)) value = 0;

			return value;
		}
		public static decimal ToDecimal(this string str)
		{
			decimal value;
			if (!Decimal.TryParse(str, out value)) value = 0;
			
			return value;

		}

		public static bool ToBoolean(this string str)
		{
			if (String.IsNullOrEmpty(str)) return false;

			return (str.ToLower() == "true");
		}
		public static bool ToBoolean(this int val) => val > 0;

		public static int ToInt(this bool val) => val ? 1 : 0;

		public static bool AllTheSame(this List<int> listA, List<int> listB)
			=> listB.All(listA.Contains) && listA.Count == listB.Count;

		public static DateTime? ToDatetimeOrNull(this string str)
		{
			DateTime dateValue;
			if (DateTime.TryParse(str, out dateValue)) return dateValue;

			return null;
				
		}

		public static DateTime ToDatetimeOrDefault(this string str, DateTime defaultValue)
		{
			DateTime dateValue;
			if (DateTime.TryParse(str, out dateValue)) return dateValue;

			return defaultValue;

		}

		public static DateTime ToDatetime(this int val)
		{
			var strVal = val.ToString();

			int year = strVal.Substring(0, 4).ToInt();
			int month = strVal.Substring(4, 2).ToInt();
			int day = strVal.Substring(6, 2).ToInt();

			return new DateTime(year, month, day);

		}

		public static bool HasDuplicate(this string[] vals)
		{
			return vals.Length != vals.Distinct().Count();
			
		}



	}
}
