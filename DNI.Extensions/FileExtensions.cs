using DNI.Shared.Abstractions;
using System;
using System.IO;

namespace DNI.Extensions
{
    public static class FileExtensions
    {
        public static string ReadAllLines(this IFile file)
        {
            using (var fileStream = file.GetFileStream(FileMode.Open))
            using (var streamReader = new StreamReader(fileStream))
                return streamReader.ReadToEnd();
            
        }
    }
}
