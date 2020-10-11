using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GoogleApi.Entities.Common.Converters
{
    public class StringBooleanConverter : JsonConverter
    {
        /// <inheritdoc />
        public override bool CanConvert(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return type == typeof(string);
        }

        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            if (objectType == null)
                throw new ArgumentNullException(nameof(objectType));

            if (serializer == null)
                throw new ArgumentNullException(nameof(serializer));

            var token = JToken.Load(reader);

            bool.TryParse(token.ToString() == "0" ? "false" : "true", out var result);

            return result;
        }

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (serializer == null)
                throw new ArgumentNullException(nameof(serializer));

            throw new NotImplementedException();
        }
    }
}
