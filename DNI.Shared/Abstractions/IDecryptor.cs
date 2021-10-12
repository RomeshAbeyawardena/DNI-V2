﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Abstractions
{
    public interface IDecryptor : ICryptographicProvider
    {
        string Decrypt(string value);
    }
}