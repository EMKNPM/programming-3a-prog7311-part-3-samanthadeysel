using System;
using TechMoves_WebAPI.Utilities;
using Xunit;

namespace Logistics_Test
{
    public class FileValidatorTests
    {
        [Fact]
        public void Validate_PdfFile_DoesNotThrow()
        {
            FileValidator.Validate("agreement.pdf");
        }

        [Fact]
        public void Validate_ExeFile_ThrowsError()
        {
            Assert.Throws<InvalidOperationException>(() => FileValidator.Validate("virus.exe"));
        }

        [Fact]
        public void Validate_DocxFile_ThrowsError()
        {
            Assert.Throws<InvalidOperationException>(() => FileValidator.Validate("report.docx"));
        }

        [Fact]
        public void Validate_PdfFileWithUppercaseExtension_DoesNotThrow()
        {
            FileValidator.Validate("contract.PDF");
        }
    }
}
