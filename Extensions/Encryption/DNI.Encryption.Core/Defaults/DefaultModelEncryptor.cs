using DNI.Encryption.Shared.Abstractions;
using DNI.Encryption.Shared.Abstractions.Factories;
using DNI.Encryption.Shared.Attributes;
using DNI.Shared.Attributes;
using DNI.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Defaults
{
    [RegisterService]
    public class DefaultModelEncryptor :  IModelEncryptor
    {
        private readonly IEncryptionOptionsFactory encryptionOptionsFactory;
        private readonly IEncryptor encryptor;
        private readonly IDecryptor decryptor;
        private readonly IHasher hasher;

        private object Decrypt(object value, EncryptionProfileAttribute encryptionProfileAttribute, IEncryptionOptions encryptionOptions)
        {
            if (encryptionProfileAttribute.EncryptionType == Encryption.Shared.Enumerations.EncryptionType.Encryption)
            {
                return decryptor.Decrypt(value?.ToString(), encryptionOptions);
            }
            else if (encryptionProfileAttribute.EncryptionType == Encryption.Shared.Enumerations.EncryptionType.Hash)
            {
                return value;
            }

            throw new NotSupportedException();
        }

        private object Encrypt(object value, EncryptionProfileAttribute encryptionProfileAttribute, IEncryptionOptions encryptionOptions)
        {
            if (encryptionProfileAttribute.EncryptionType == Encryption.Shared.Enumerations.EncryptionType.Encryption)
            {
                return encryptor.Encrypt(value?.ToString(), encryptionOptions);
            }
            else if (encryptionProfileAttribute.EncryptionType == Encryption.Shared.Enumerations.EncryptionType.Hash)
            {
                return hasher.HashString(value?.ToString());
            }

            throw new NotSupportedException();
        }

        private T Apply<T>(T model, Func<object, EncryptionProfileAttribute, IEncryptionOptions, object> apply)
        {
            var newInstance = Activator.CreateInstance<T>();

            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var encryptionProfileAttribute = property.PropertyType.GetCustomAttribute<EncryptionProfileAttribute>();

                var propertyValue = property.GetValue(model);

                if(propertyValue == null || propertyValue.IsDefault())
                {
                    continue;
                }

                if (encryptionProfileAttribute != null)
                {
                   property.SetValue(newInstance, apply(propertyValue, encryptionProfileAttribute, encryptionOptionsFactory.GetEncryptionOptions(encryptionProfileAttribute.SectionName)));
                }
                else
                    property.SetValue(newInstance, propertyValue);
            }

            return newInstance;
        }

        public DefaultModelEncryptor(
            IEncryptionOptionsFactory encryptionOptionsFactory,
            IEncryptor encryptor, 
            IDecryptor decryptor,
            IHasher hasher)
        {
            this.encryptionOptionsFactory = encryptionOptionsFactory;
            this.encryptor = encryptor;
            this.decryptor = decryptor;
            this.hasher = hasher;
        }

        public T Decrypt<T>(T model)
        {
            return Apply(model, Decrypt);
        }

        public T Encrypt<T>(T model)
        {
            return Apply(model, Encrypt);
        }
    }
}
