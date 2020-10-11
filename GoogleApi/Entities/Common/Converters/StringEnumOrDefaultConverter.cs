using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleApi.Entities.Common.Converters
{
    public class StringEnumOrDefaultConverter<T> : StringEnumConverter
        where T : struct
    {
        /// <inheritdoc />
        public override bool CanConvert(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return base.CanConvert(typeof(T));
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

            try
            {
                return base.ReadJson(reader, objectType, existingValue, serializer);
            }
            catch
            {
                return default(T);
            }
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

            base.WriteJson(writer, value, serializer);
        }
    }
}
