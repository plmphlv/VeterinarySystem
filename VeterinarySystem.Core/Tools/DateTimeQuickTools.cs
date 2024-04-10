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

		public static DateTime GetDateAndTime()
		{
			DateTime dateTime = DateTime.Now;

			// Create a new DateTime object with the same date and time but formatted as needed
			DateTime formattedDateTime = new DateTime(
				dateTime.Year,
				dateTime.Month,
				dateTime.Day,
				dateTime.Hour,
				dateTime.Minute,
				0);

			return formattedDateTime;
		}

        public static DateTime GetDateOnly(this DateTime dateTime)
        {
            string dateTimeString = dateTime.ToString(EntityConstants.DateFormat);

            DateTime.TryParseExact(
                dateTimeString,
                EntityConstants.DateOnlyFormat,
                CultureInfo.InvariantCulture,
            DateTimeStyles.None,
                out dateTime);

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
