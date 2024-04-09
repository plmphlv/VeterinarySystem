using System.Globalization;
using VeterinarySystem.Common;

namespace VeterinarySystem.Core.Tools.ExtenshionMethods
{
	public static class DateTimeQuickTools
	{
		public static bool DateFormatIsValid(this string dateString)
		{
			DateTime dateAndTime = DateTime.Now;

			if (!DateTime.TryParseExact(
				dateString,
				EntityConstants.DateFormat,
				CultureInfo.InvariantCulture,
				DateTimeStyles.None,
				out dateAndTime))
			{
				return false;
			}
			return true;
		}

		public static DateTime GetDate()
		{
			DateTime dateTime = DateTime.Now;

			dateTime = dateTime.AddTicks(-(dateTime.Ticks % TimeSpan.TicksPerSecond));

			return dateTime;
		}

		public static bool CompareDate(this DateTime appointmentDate)
		{
			DateTime now = DateTime.Now;

			if (DateTime.Compare(appointmentDate, now) == 1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public static bool CompareDate(DateTime Date1, DateTime Date2)
		{
			if (DateTime.Compare(Date1, Date2) == 1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
