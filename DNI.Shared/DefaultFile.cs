using DNI.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared
{
    public class DefaultFile : IFile
    {
        public DefaultFile(string fullPath)
        {
            FullPath = fullPath;
        }

        public string FullPath { get; }

        public FileStream GetFileStream(FileMode mode)
        {
            return new FileStream(FullPath, FileMode.Open);
        }
    }
}
