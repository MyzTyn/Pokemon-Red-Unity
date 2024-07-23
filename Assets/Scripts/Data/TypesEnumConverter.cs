using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;
using Types = PokemonUnity.Types;

// To handle the "Bird" enum value
public class TypesEnumConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(Types) || objectType == typeof(Types[]);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.None)
            return Types.NONE;
        
        if (reader.TokenType == JsonToken.String)
        {
            string enumString = reader.Value.ToString();
            switch (enumString)
            {
                case "Bird":
                    return Types.FLYING;
                default:
                    return Enum.Parse(typeof(Types), enumString.ToUpper(), true);
            }
        }
        
        if (reader.TokenType == JsonToken.StartArray)
        {
            JArray array = JArray.Load(reader);
            Types[] enumArray = new Types[array.Count];
            for (int i = 0; i < array.Count; i++)
            {
                enumArray[i] = (Types)ReadJson(array[i].CreateReader(), typeof(Types), null, serializer);
            }
            return enumArray;
        } 

        throw new JsonSerializationException("Unexpected token type when parsing enum.");
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value is Types)
        {
            Types enumValue = (Types)value;
            switch (enumValue)
            {
                case Types.FLYING:
                    writer.WriteValue("Bird");
                    break;
                default:
                    writer.WriteValue(enumValue.ToString());
                    break;
            }
        }
        else if (value is Types[])
        {
            Types[] enumArray = (Types[])value;
            writer.WriteStartArray();
            foreach (var enumValue in enumArray)
            {
                WriteJson(writer, enumValue, serializer);
            }
            writer.WriteEndArray();
        }
    }
}
