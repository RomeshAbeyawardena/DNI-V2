using DNI.Shared;
using DNI.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Providers
{
    public class LocalFileProvider : IFileProvider
    {
        public IFile GetFile(string fileName, FileAccess fileAccess)
        {
            var file = new FileInfo(fileName);
            return new DefaultFile(file.FullName);
        }

        public IEnumerable<IFile> GetFiles(string path, string pattern)
        {
            throw new NotImplementedException();
        }
    }
}
