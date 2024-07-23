using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using PokemonUnity;

public class PokemonsEnumConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(Pokemons) || objectType == typeof(Pokemons[]);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.String)
        {
            string enumString = reader.Value.ToString();
            switch (enumString)
            {
                case "NidoranM":
                    return Pokemons.NIDORAN_M;
                case "NidoranF":
                    return Pokemons.NIDORAN_F;
                default:
                    return Enum.Parse(typeof(Pokemons), enumString.ToUpper(), true);
            }
        }
        else if (reader.TokenType == JsonToken.StartArray)
        {
            JArray array = JArray.Load(reader);
            Pokemons[] enumArray = new Pokemons[array.Count];
            for (int i = 0; i < array.Count; i++)
            {
                enumArray[i] = (Pokemons)ReadJson(array[i].CreateReader(), typeof(Pokemons), null, serializer);
            }
            return enumArray;
        }

        throw new JsonSerializationException("Unexpected token type when parsing enum.");
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value is Pokemons)
        {
            Pokemons enumValue = (Pokemons)value;
            switch (enumValue)
            {
                case Pokemons.NIDORAN_M:
                    writer.WriteValue("NidoranM");
                    break;
                case Pokemons.NIDORAN_F:
                    writer.WriteValue("NidoranF");
                    break;
                default:
                    writer.WriteValue(enumValue.ToString());
                    break;
            }
        }
        else if (value is Pokemons[])
        {
            Pokemons[] enumArray = (Pokemons[])value;
            writer.WriteStartArray();
            foreach (var enumValue in enumArray)
            {
                WriteJson(writer, enumValue, serializer);
            }
            writer.WriteEndArray();
        }
    }
}