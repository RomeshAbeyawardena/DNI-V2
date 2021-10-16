﻿using DNI.Core.Defaults;
using DNI.Encryption.Shared.Abstractions;
using DNI.Encryption.Shared.Abstractions.Factories;
using DNI.Encryption.Shared.Enumerations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Encryption.Extensions
{
    public static class EncryptionOptionsExtensions
    {
        public static IEncryptionModuleOptions AddEncryptionOption(this IEncryptionModuleOptions options, string keyName, string key, string initialVector, SymmetricAlgorithm algorithm, Encoding encoding)
        {
            options.EncryptionOptions.Add(keyName, new DefaultEncryptionOptions(key, initialVector, algorithm, encoding));
            return options;
        }
        
        public static IEncryptionModuleOptions AddEncryptionOption(this IEncryptionModuleOptions options, string sectionNamekey)
        {
            options.EncryptionOptionsFactory.Add(sectionNamekey, s => s.GetRequiredService<IEncryptionOptionsFactory>().GetEncryptionOptions(sectionNamekey));
            return options;
        }
    }
}
