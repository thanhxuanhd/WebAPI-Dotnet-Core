using Newtonsoft.Json.Serialization;
using Service.DataContracts;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Service.Extensions
{
    public class SerializeContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, Newtonsoft.Json.MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            if (property.DeclaringType == typeof(BaseDto) || property.DeclaringType.GetTypeInfo().BaseType == typeof(BaseDto) ||
                property.DeclaringType.GetTypeInfo().BaseType.GetTypeInfo().BaseType == typeof(BaseDto))
            {
                if (property.PropertyName == "SerializableProperties")
                {
                    property.ShouldSerialize = instance => { return false; };
                }
                else
                {
                    property.ShouldSerialize = instance =>
                    {
                        var p = (BaseDto)instance;
                        return p.SerializableProperties.Contains(property.PropertyName);
                    };
                }
            }
            return property;
        }
    }
}
