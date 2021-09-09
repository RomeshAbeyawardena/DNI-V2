using DNI.Shared.Abstractions;
using DNI.Shared.Abstractions.Factories;
using DNI.Shared.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Base
{
    public abstract class ConfigBase : DictionaryBase<string, object>, IConfig
    {
        private readonly ISerializerFactory serializerFactory;
        private readonly IFileProvider fileProvider;

        public ConfigBase(ISerializerFactory serializerFactory)
        {
            this.serializerFactory = serializerFactory;
            this.fileProvider = fileProvider;
        }

        public void Load(IFile file, SerializerType serializerType)
        {
            serializerFactory.GetSerializer(serializerType)
                .Deserialize<IDictionary<string, object>>(file.ReadAlLines());
        }

        public void Bind(object target)
        {
            var targetType = target.GetType();

            foreach(var (key, value) in this)
            {
                var property = targetType.GetProperty(key);

                if(property == null)
                {
                    continue;
                }

                property.SetValue(target, value);
            }
        }
    }
}
