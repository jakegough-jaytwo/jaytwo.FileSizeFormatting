using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Xunit;

namespace jaytwo.FileSizeFormatting.Tests
{
    public class FileSizeFormatterTests
    {
        [Theory]
        [InlineData(0, "0 B")]
        [InlineData(1, "1 B")]
        [InlineData(10, "10 B")]
        [InlineData(100, "100 B")]
        [InlineData(1024L, "1.00 KiB")]
        [InlineData(1024L * 0.9, "0.90 KiB")]
        [InlineData(1024L * 1.1, "1.10 KiB")]
        [InlineData(1024L * 1.11, "1.11 KiB")]
        [InlineData(1024L * 1.111, "1.11 KiB")]
        [InlineData(1024L * 1.114, "1.11 KiB")]
        [InlineData(1024L * 1.115, "1.12 KiB")]
        [InlineData(1024L * 10.1, "10.1 KiB")]
        [InlineData(1024L * 100.1, "100 KiB")]
        [InlineData(1024L * 1024L, "1.00 MiB")]
        [InlineData(1024L * 1024L * 1024L, "1.00 GiB")]
        [InlineData(1024L * 1024L * 1024L * 1024L, "1.00 TiB")]
        [InlineData(1024L * 1024L * 1024L * 1024L * 1024L, "1,024 TiB")]
        [InlineData(1024L * 1024L * 1024L * 1024L * 1024L * 1024L, "1,048,576 TiB")]
        [InlineData(1000L * 1000L, "0.95 MiB")]
        public void BinaryScale(long sizeBytes, string expected)
        {
            // arrange
            var formatter = FileSizeFormatter.CreateWithBinaryScale();

            // act
            var actual = formatter.GetCaption(sizeBytes);

            // assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, "0 B")]
        [InlineData(1, "1 B")]
        [InlineData(10, "10 B")]
        [InlineData(100, "100 B")]
        [InlineData(1000L, "1.00 KB")]
        [InlineData(1000L * 0.9, "0.90 KB")]
        [InlineData(1000L * 1.1, "1.10 KB")]
        [InlineData(1000L * 1.11, "1.11 KB")]
        [InlineData(1000L * 1.111, "1.11 KB")]
        [InlineData(1000L * 1.114, "1.11 KB")]
        [InlineData(1000L * 1.115, "1.12 KB")]
        [InlineData(1000L * 10.1, "10.1 KB")]
        [InlineData(1000L * 100.1, "100 KB")]
        [InlineData(1000L * 1000L, "1.00 MB")]
        [InlineData(1000L * 1000L * 1000L, "1.00 GB")]
        [InlineData(1000L * 1000L * 1000L * 1000L, "1.00 TB")]
        [InlineData(1000L * 1000L * 1000L * 1000L * 1000L, "1,000 TB")]
        [InlineData(1000L * 1000L * 1000L * 1000L * 1000L * 1000L, "1,000,000 TB")]
        [InlineData(1024L * 1024L, "1.05 MB")]
        public void DecimalScale(long sizeBytes, string expected)
        {
            // arrange
            var formatter = FileSizeFormatter.CreateWithDecimalScale();

            // act
            var actual = formatter.GetCaption(sizeBytes);

            // assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, "en-US", "0 B")]
        [InlineData(0, "pt-BR", "0 B")]
        [InlineData(1500, "en-US", "1.50 KB")]
        [InlineData(1500, "pt-BR", "1,50 KB")]
        [InlineData(1000L * 1000L * 1000L * 1000L * 1000L, "en-US", "1,000 TB")]
        [InlineData(1000L * 1000L * 1000L * 1000L * 1000L, "pt-BR", "1.000 TB")]
        public void WithLocale(long sizeBytes, string locale, string expected)
        {
            // arrange
            var formatter = FileSizeFormatter.CreateWithDecimalScale();

            // act
            var actual = formatter.GetCaption(sizeBytes, new CultureInfo(locale));

            // assert
            Assert.Equal(expected, actual);
        }
    }
}
