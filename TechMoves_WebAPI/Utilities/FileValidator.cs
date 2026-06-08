using System;

namespace TechMoves_WebAPI.Utilities
{
    public static class FileValidator
    {
        public static void Validate(string fileName)
        {
            if (!fileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Only PDF files are allowed.");
        }
    }
}