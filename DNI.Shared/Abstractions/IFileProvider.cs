using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Abstractions
{
    public interface IFileProvider
    {
        IEnumerable<IFile> GetFiles(string path, string pattern);
        IFile GetFile(string fileName);
    }
}
