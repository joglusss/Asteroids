using System;
using Newtonsoft.Json;
using R3;

namespace Asteroids.Total
{
    public class JsonReactivePropertyConvertor<T> : JsonConverter<ReactiveProperty<T>>
    {
        public override ReactiveProperty<T> ReadJson(JsonReader reader, Type objectType, ReactiveProperty<T> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
			T value = serializer.Deserialize<T>(reader);
        	return new ReactiveProperty<T>(value);
        }

        public override void WriteJson(JsonWriter writer, ReactiveProperty<T> value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value.Value);
        }
    }
}

