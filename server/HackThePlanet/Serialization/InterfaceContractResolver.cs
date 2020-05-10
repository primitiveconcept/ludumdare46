namespace HackThePlanet
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;


    // Source: http://www.tomdupont.net/2015/09/how-to-only-serialize-interface.html
    public class InterfaceContractResolver : DefaultContractResolver
    {
        private readonly Type[] _interfaceTypes;

        private readonly ConcurrentDictionary<Type, Type> _typeToSerializeMap;


        #region Constructors
        public InterfaceContractResolver(params Type[] interfaceTypes)
        {
            this._interfaceTypes = interfaceTypes;

            this._typeToSerializeMap = new ConcurrentDictionary<Type, Type>();
        }
        #endregion


        protected override IList<JsonProperty> CreateProperties(
            Type type,
            MemberSerialization memberSerialization)
        {
            Type typeToSerialize = this._typeToSerializeMap.GetOrAdd(
                key: type,
                valueFactory: t => this._interfaceTypes.FirstOrDefault(
                                       it => it.IsAssignableFrom(t)) ?? t);

            IList<JsonProperty> props = base.CreateProperties(typeToSerialize, memberSerialization);

            // mark all props as not ignored
            foreach (JsonProperty prop in props)
            {
                prop.Ignored = false;
            }

            return props;
        }
    }
}