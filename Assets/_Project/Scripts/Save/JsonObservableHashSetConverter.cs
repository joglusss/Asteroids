using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using NUnit.Framework;
using ObservableCollections;

namespace Asteroids.Total
{
    public class JsonObservableHashSetConverter<T> : JsonConverter<ObservableHashSet<T>>
    {   
        public override ObservableHashSet<T> ReadJson(JsonReader reader, Type objectType, ObservableHashSet<T> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            List<T> list = serializer.Deserialize<List<T>>(reader);
            
            return new ObservableHashSet<T>(list);
        }

        public override void WriteJson(JsonWriter writer, ObservableHashSet<T> value, JsonSerializer serializer)
        {
            var set = (ObservableHashSet<T>)value;
            serializer.Serialize(writer, new List<T>(set));
        }
    }
}


