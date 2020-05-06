#nullable enable
#pragma warning disable 8600
#pragma warning disable 8602
#pragma warning disable 8603
#pragma warning disable 8605
#pragma warning disable 8618

namespace PrimitiveEngine
{
    using System;
    using Newtonsoft.Json;
    

    /// <summary>
    /// Used to reference a component attached to a specific entity.
    /// Can lazily cache references to Entity and IEntityComponent.
    /// Includes various implicit conversions for ease of use.
    /// </summary>
    /// <typeparam name="T">Concrete type of the component referenced.</typeparam>
    [JsonConverter(typeof(ComponentReferenceSerialization))]
    public sealed class ComponentReference<T> : ComponentReference,
                                                IEquatable<ComponentReference<T>>
        where T: class, IEntityComponent
    {
        #region Constructors
        public ComponentReference(int entityId, T component) : base(entityId, component)
        {
        }


        [JsonConstructor]
        public ComponentReference(int entityId) : base(entityId)
        {
        }
        #endregion


        #region Properties
        public T Component
        {
            get
            {
                return this.component as T;
            }
            set { this.component = value; }
        }
        #endregion


        public bool Equals(ComponentReference<T> other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return this.EntityId == other.EntityId && this.component.Equals(other.component);
        }


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((ComponentReference)obj);
        }


        public override int GetHashCode()
        {
            return HashCode.Combine(this.EntityId, this.component);
        }
    }
    
    
    /// <summary>
    /// WARNING: DO NO USE THIS DIRECTLY.
    /// Loose base for typed ComponentReference, used only for serialization/deserialization purposes.
    /// </summary>
    [JsonConverter(typeof(ComponentReferenceSerialization))]
    public class ComponentReference : IEquatable<ComponentReference>
    {
        public readonly int EntityId;

        protected Entity entity;
        protected IEntityComponent component;


        #region Constructors
        public ComponentReference(int entityId, IEntityComponent component)
        {
            this.EntityId = entityId;
            this.component = component;
        }


        [JsonConstructor]
        public ComponentReference(int entityId)
        {
            this.EntityId = entityId;
        }
        #endregion


        #region Operators
        public static implicit operator int(ComponentReference reference)
        {
            return reference.EntityId;
        }


        public static bool operator ==(ComponentReference left, ComponentReference right)
        {
            return Equals(left, right);
        }


        public static bool operator !=(ComponentReference left, ComponentReference right)
        {
            return !Equals(left, right);
        }
        #endregion


        public bool Equals(ComponentReference other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return this.EntityId == other.EntityId && this.component.Equals(other.component);
        }


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((ComponentReference)obj);
        }


        public override int GetHashCode()
        {
            return HashCode.Combine(this.EntityId, this.component);
        }
    }
    
    
    public class ComponentReferenceSerialization : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(ComponentReference).IsAssignableFrom(objectType);
        }


        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            int entityId = (int)reader.Value;
            ComponentReference componentReference = new ComponentReference(entityId);
            return componentReference;
        }


        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            ComponentReference componentReference = (ComponentReference)value;
            writer.WriteValue(componentReference.EntityId);
        }
    } 
}
#pragma warning restore 8600
#pragma warning restore 8602
#pragma warning restore 8603
#pragma warning restore 8605
#pragma warning restore 8618 