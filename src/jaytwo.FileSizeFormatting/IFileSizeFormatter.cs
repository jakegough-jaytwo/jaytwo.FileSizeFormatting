using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jaytwo.FileSizeFormatting
{
    public interface IFileSizeFormatter
    {
        string GetCaption(long length, CultureInfo cultureInfo);
    }
}
