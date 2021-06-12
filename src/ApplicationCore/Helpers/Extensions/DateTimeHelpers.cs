using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Helpers
{
	public static class DateTimeHelpers
    {
		public static DateTime? ToStartDate(this string input)
		{
			var startDate = input.ToDatetimeOrNull();
			if (startDate.HasValue)
			{
				var dateStart = Convert.ToDateTime(startDate);
				return new DateTime(dateStart.Year, dateStart.Month, dateStart.Day, 0, 0, 0);
			}
			else return null;
		}

		public static DateTime? ToEndDate(this string input)
		{
			var endDate = input.ToDatetimeOrNull();
			if (endDate.HasValue)
			{
				var dateEnd = Convert.ToDateTime(endDate);
				return dateEnd.ToEndDate();
			}
			else return null;
		}

		public static DateTime ToEndDate(this DateTime date)
		{
			return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
		}

		public static DateTime ConvertToTaipeiTime(this DateTime input)
		{
			string taipeiTimeZoneId = "Taipei Standard Time";
			TimeZoneInfo taipeiTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(taipeiTimeZoneId);
			return TimeZoneInfo.ConvertTimeFromUtc(input.ToUniversalTime(), taipeiTimeZoneInfo);

		}
		public static int ToDateNumber(this DateTime input)
		{
			return Convert.ToInt32(GetDateString(input.Date));
		}
		public static int ToTimeNumber(this DateTime input)
		{			
			return Convert.ToInt32(GetTimeString(input));
		}

        

		public static string ToTimeString(this int val)
		{
			string str = val.ToString();
			if (str.Length != 6) str = "0" + str;

			string h = str.Substring(0, 2);
			string m = str.Substring(2, 2);
			string s = str.Substring(4, 2);

			return String.Format("{0}:{1}:{2}", h, m, s);

		}

		public static string ToTimeString(this DateTime input)
		{
			return input.ToString("H:mm:ss");
		}

		public static string ToDateString(this DateTime input)
		{
			return input.ToString("yyyy-MM-dd");
		}

		public static string ToDateString(this DateTime? input)
		{
			if (input.HasValue) return input.Value.ToDateString();
			return "";
		}

		public static string ToDateTimeString(this DateTime input)
		{
			return input.ToString("yyyy-MM-dd H:mm:ss");
		}

		public static string ToDateTimeString(this DateTime? input)
		{
			if (input.HasValue) return input.Value.ToDateTimeString();
			return "";
		}

		public static string GetDateString(DateTime dateTime)
		{
			string year = dateTime.Year.ToString();
			string month = dateTime.Month.ToString();
			string day = dateTime.Day.ToString();

			if (dateTime.Month < 10) month = "0" + month;
			if (dateTime.Day < 10) day = day = "0" + day;


			return year + month + day;

		}

		static string GetTimeString(DateTime dateTime, bool toMileSecond=false)
		{
			string hour = dateTime.Hour.ToString();
			string minute = dateTime.Minute.ToString();
			string second = dateTime.Second.ToString();
			string mileSecond = dateTime.Millisecond.ToString();

			if (dateTime.Hour < 10) hour = "0" + hour;
			if (dateTime.Minute < 10) minute = "0" + minute;
			if (dateTime.Second < 10) second = "0" + second;

			if (!toMileSecond) return hour + minute + second;


			if (dateTime.Millisecond < 10)
			{
				mileSecond = "00" + mileSecond;
			}
			else if (dateTime.Millisecond < 100)
			{
				mileSecond = "0" + mileSecond;
			}

			return hour + minute + second + mileSecond;

		}



		#region OrderMaker
		public static DateTime FindThirdWedOfMonth(this DateTime date)
		{
			DateTime thirdWed = new DateTime(date.Year, date.Month, 15);

			while (thirdWed.DayOfWeek != DayOfWeek.Wednesday)
			{
				thirdWed = thirdWed.AddDays(1);
			}

			return thirdWed;

		}

		public static int[] ToTimes(this string strVal)
		{
			if (strVal.Length < 6) strVal = "0" + strVal;

			var hour = strVal.Substring(0, 2).ToInt();
			var minute = strVal.Substring(2, 2).ToInt();
			var second = strVal.Substring(4, 2).ToInt();


			return new int[] { hour, minute, second };
		}
		#endregion

	}
}
