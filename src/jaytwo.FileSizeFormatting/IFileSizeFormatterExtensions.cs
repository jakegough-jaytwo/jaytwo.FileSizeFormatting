using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jaytwo.FileSizeFormatting
{
    public static class IFileSizeFormatterExtensions
    {
        public static string GetCaption(this IFileSizeFormatter fileSizeFormatter, long length)
            => fileSizeFormatter.GetCaption(length, CultureInfo.InvariantCulture);
    }
}
