using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace jaytwo.FileSizeFormatting
{
    public class FileSizeFormatter : IFileSizeFormatter
    {
        private readonly IDictionary<double, string> _scale;

        internal FileSizeFormatter(IDictionary<double, string> scale)
        {
            _scale = scale;
        }

        public static IFileSizeFormatter CreateWithBinaryScale()
            => new FileSizeFormatter(new Dictionary<double, string>()
            {
                // "ibi" bytes... kibibyte, mibibyte, etc. (files, ram are sized this way)
                { Math.Pow(1024, 1), "KiB" },
                { Math.Pow(1024, 2), "MiB" },
                { Math.Pow(1024, 3), "GiB" },
                { Math.Pow(1024, 4), "TiB" },
            });

        public static IFileSizeFormatter CreateWithDecimalScale()
            => new FileSizeFormatter(new Dictionary<double, string>()
            {
                // Metric prefixes... kilobyte, megabyte, etc. (hard drives are sized this way)
                { Math.Pow(1000, 1), "KB" },
                { Math.Pow(1000, 2), "MB" },
                { Math.Pow(1000, 3), "GB" },
                { Math.Pow(1000, 4), "TB" },
            });

        public string GetCaption(long length, CultureInfo cultureInfo)
        {
            foreach (var entry in _scale.OrderByDescending(x => x.Key))
            {
                var ratio = length / entry.Key;

                if (ratio >= 0.7) // 699 B, then 0.70 KB
                {
                    var decimalPlaces = GetDecimalPlaces(ratio);

                    // convert to decimal right before the rounding to avoid floating point quirks... (ex: $"{1115d / 1000d:n2}" returns "1.11" instead of "1.12")
                    return string.Format(cultureInfo, "{0:n" + decimalPlaces + "} {1}", (decimal)ratio, entry.Value);
                }
            }

            return $"{length} B";
        }

        private int GetDecimalPlaces(double ratio)
        {
            if (ratio >= 100)
            {
                return 0;
            }
            else if (ratio >= 10)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
    }
}
