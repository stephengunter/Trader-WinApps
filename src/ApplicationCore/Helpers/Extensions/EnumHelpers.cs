using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationCore.Helpers
{
	public static class EnumHelpers
	{
		public static List<T> ToList<T>() => Enum.GetValues(typeof(T)).Cast<T>().ToList();

		public static T ToEnum<T>(this string inString, bool ignoreCase = true, bool throwException = true) where T : struct
		{
			return (T)ParseEnum<T>(inString, ignoreCase, throwException);
		}

		public static T ToEnum<T>(this string inString, T defaultValue, bool ignoreCase = true, bool throwException = false) where T : struct
		{
			return (T)ParseEnum<T>(inString, defaultValue, ignoreCase, throwException);
		}



		public static T ParseEnum<T>(string inString, bool ignoreCase = true, bool throwException = true) where T : struct
		{
			return (T)ParseEnum<T>(inString, default(T), ignoreCase, throwException);
		}

		public static T ParseEnum<T>(string inString, T defaultValue,
							   bool ignoreCase = true, bool throwException = false) where T : struct
		{

			T returnEnum = defaultValue;

			if (!typeof(T).IsEnum || String.IsNullOrEmpty(inString))
			{
				throw new InvalidOperationException("Invalid Enum Type or Input String 'inString'. " + typeof(T).ToString() + "  must be an Enum");
			}

			try
			{
				if (Enum.TryParse<T>(inString, ignoreCase, out returnEnum))
				{
					if (Enum.IsDefined(typeof(T), returnEnum) | returnEnum.ToString().Contains(","))
					{
						return returnEnum;
					}
					else
					{
						throw new Exception("Invalid Cast.");
					}
				}
				else
				{
					throw new Exception();
				}
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Invalid Cast", ex);
			}


		}
	}
}
