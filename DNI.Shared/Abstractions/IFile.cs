using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Abstractions
{
    public interface IFile
    {
        string FullPath { get; }
        FileStream GetFileStream(FileMode mode);
    }
}
