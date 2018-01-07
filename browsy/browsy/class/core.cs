using System;
namespace browsy
{
    public class core
    {
        public core()
        {
        }

        public static string relative_time(DateTime t) {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;
            var ts = new TimeSpan(DateTime.UtcNow.Ticks - t.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);
            if (delta < 1 * MINUTE)return ts.Seconds == 1 ? "1 sec" : ts.Seconds + " sec";
            if (delta < 2 * MINUTE)return "1 minute";
            if (delta < 45 * MINUTE)return ts.Minutes + " minutes";
            if (delta < 90 * MINUTE)return "1 hour";
            if (delta < 24 * HOUR)return ts.Hours + " hours";
            if (delta < 48 * HOUR)return "yesterday";
            if (delta < 30 * DAY) return ts.Days + " days";
            if (delta < 12 * MONTH){
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "1 month" : months + " months";
            }else{
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "1 year" : years + " years";
            }
        }

        public static string get_bytes_readable(long i){    // Get absolute value
            long absolute_i = (i < 0 ? -i : i);             // Determine the suffix and readable value
            string suffix;
            double readable;
            if (absolute_i >= 0x1000000000000000) {         // Exabyte
                suffix = "EB";
                readable = (i >> 50);
            } else if (absolute_i >= 0x4000000000000) {      // Petabyte
                suffix = "PB";
                readable = (i >> 40);
            } else if (absolute_i >= 0x10000000000) {         // Terabyte
                suffix = "TB";
                readable = (i >> 30);
            } else if (absolute_i >= 0x40000000) {           // Gigabyte
                suffix = "GB";
                readable = (i >> 20);
            } else if (absolute_i >= 0x100000) {             // Megabyte
                suffix = "MB";
                readable = (i >> 10);
            } else if (absolute_i >= 0x400) {                // Kilobyte
                suffix = "KB";
                readable = i;
            } else{
                return i.ToString("0 B"); // Byte
            }

            readable = (readable / 1024);                       // Divide by 1024 to get fractional value
            return readable.ToString("0.# ") + suffix;          // Return formatted number with suffix
        }
    }
}
