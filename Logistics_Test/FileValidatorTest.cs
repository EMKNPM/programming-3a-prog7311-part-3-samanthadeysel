using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics_Test
{
    public static class FileValidator
    {
        public static void Validate(string fileName)
        {
            if (!fileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Only PDF files are allowed.");
        }
    }

    public static class FileValidatorTest
    {
        [Fact]
        public static void ValidatePdfFileDoesNotThrow()
        {
            FileValidator.Validate("agreement.pdf");
        }

        [Fact]
        public static void ValidateExeFileThrowsError()
        {
            Assert.Throws<InvalidOperationException>(() => FileValidator.Validate("virus.exe"));
        }
    }
}
