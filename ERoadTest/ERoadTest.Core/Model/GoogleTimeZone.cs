using System;

namespace ERoadTest
{
    public class GoogleTimeZone
    {
        public double dstOffset { get; set; }

        public double rawOffset { get; set; }

        public string status { get; set; }

        public string timeZoneId { get; set; }

        public string timeZoneName { get; set; }
    }

    public static class TimeExtension
    {
        public static double ToTimestamp(this DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }
    }
}

