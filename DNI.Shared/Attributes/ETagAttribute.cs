using System;

namespace DNI.Shared.Attributes
{
    /// <summary>
    /// <para>Marks a property or field as an ETag.</para>
    /// <para>It will be excluded in checksum calculations, and be used as a target to update the ETag for a model</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ETagAttribute : Attribute
    {
        
    }
}
