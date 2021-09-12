﻿using DNI.Modules.Database.Abstractions;
using DNI.Shared.Abstractions.Factories;
using DNI.Shared.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Database
{
    public class EntityFrameworkAppConfig : ConfigBase, IEntityFrameworkAppConfig
    {
        public const string ConfigName = "EntityFramework";
        public EntityFrameworkAppConfig(ISerializerFactory serializerFactory) 
            : base(serializerFactory, ConfigName)
        {
            Bind(this);
        }

        public string ConnectionString { get; set; }
    }
}