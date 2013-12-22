using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS.InternetTime
{
	public static class DateTimeExtensions
	{
		public static double ToInternetTimeDouble(this DateTime time)
		{
			return time.ToUniversalTime().AddHours(1).TimeOfDay.TotalMilliseconds / 86400d;
		}

		public static int ToInternetTimeInt(this DateTime time)
		{
			return Convert.ToInt32(Math.Floor(time.ToInternetTimeDouble()));
		}

		public static string ToInternetTimeStr(this DateTime time)
		{
			return time.ToInternetTimeStr(false);
		}

		public static string ToInternetTimeStr(this DateTime time, bool decimals)
		{
			return string.Format(CultureInfo.InvariantCulture, "@{0}", decimals ? string.Format(CultureInfo.InvariantCulture, "{0:0.00}", time.ToInternetTimeDouble()) : time.ToInternetTimeInt().ToString(CultureInfo.InvariantCulture));
		}
	}
}
