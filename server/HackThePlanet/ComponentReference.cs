#nullable enable
namespace HackThePlanet
{
    using System;
    using Newtonsoft.Json;
    using PrimitiveEngine;


    /// <summary>
    /// Used to reference a component attached to a specific entity.
    /// Includes various implicit conversion for ease of use.
    /// </summary>
    /// <typeparam name="T">Concrete type of the component referenced.</typeparam>
    [JsonConverter(typeof(ComponentReferenceSerialization))]
    public sealed class ComponentReference<T> : ComponentReference
        where T: class, IEntityComponent
    {
        #region Constructors
        public ComponentReference(int entityId, T component) : base(entityId, component)
         {
         }


        public ComponentReference(int entityId) : base(entityId)
         {
         }


        public ComponentReference(T component) : base(component)
         {
         }
        #endregion


        #region Properties
        public T Component
        {
            get
            {
                if (this.component == null)
                    this.component = GetEntity().GetComponent<T>();
                return this.component as T;
            }
            set { this.component = value; }
        }
        #endregion


        #region Operators
        public static implicit operator int(ComponentReference<T> reference)
        {
            return reference.EntityId;
        }


        public static implicit operator T(ComponentReference<T> reference)
        {
            return reference.Component;
        }


        public static implicit operator Entity(ComponentReference<T> reference)
        {
            return reference.GetEntity();
        }
        #endregion


        public override bool Equals(object obj)
        {
            ComponentReference<T> componentReference = obj as ComponentReference<T>;
            if (componentReference != null)
            {
                return componentReference.EntityId == this.EntityId
                       && componentReference.Component == this.component;
            }

            return false;
        }
    }
    
    
    /// <summary>
    /// WARNING: DO NO USE THIS DIRECTLY.
    /// Loose base type for ComponentReference<T>.
    /// Used only for serialization/deserialization purposes.
    /// </summary>
    [JsonConverter(typeof(ComponentReferenceSerialization))]
    public class ComponentReference
    {
        public int EntityId;
        protected IEntityComponent component;


        #region Constructors
        public ComponentReference(int entityId, IEntityComponent component)
        {
            this.EntityId = entityId;
            this.component = component;
        }


        public ComponentReference(int entityId)
        {
            this.EntityId = entityId;
        }


        public ComponentReference(IEntityComponent component)
        {
            Entity componentEntity = component.GetEntity();
            if (componentEntity != null)
                this.EntityId = componentEntity.Id;
            this.component = component;
        }
        #endregion


        #region Operators
        public static implicit operator int(ComponentReference reference)
        {
            return reference.EntityId;
        }


        public static implicit operator Entity(ComponentReference reference)
        {
            return reference.GetEntity();
        }
        #endregion


        public override bool Equals(object obj)
        {
            ComponentReference componentReference = obj as ComponentReference;
            if (componentReference != null)
            {
                return componentReference.EntityId == this.EntityId
                       && componentReference.component == this.component;
            }

            return false;
        }


        public Entity GetEntity()
        {
            return Game.World.GetEntityById(this.EntityId);
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