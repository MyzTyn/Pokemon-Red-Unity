using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Serialization;
using PokemonUnity;
using UnityEngine;

// Custom converter for the Moves enum
public class MovesEnumConverter : JsonConverter
{
    private static SnakeCaseNamingStrategy _snakeCaseStrategy = new SnakeCaseNamingStrategy();
    
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(Moves) || objectType == typeof(Moves[]);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.None)
            return Moves.NONE;
        
        if (reader.TokenType == JsonToken.String)
        {
            string enumString = reader.Value.ToString();
            // Convert PascalCase to UPPER_SNAKE_CASE
            string normalizedString = ConvertToUpperSnakeCase(enumString);
            // Moves.PIN_MISSILE
            return Enum.Parse(typeof(Moves), normalizedString, true);
        }
        else if (reader.TokenType == JsonToken.StartArray)
        {
            JArray array = JArray.Load(reader);
            Moves[] enumArray = new Moves[array.Count];
            for (int i = 0; i < array.Count; i++)
            {
                if (array[i].Type == JTokenType.String)
                {
                    string enumString = array[i].ToString();
                    // Convert PascalCase to UPPER_SNAKE_CASE
                    string normalizedString = ConvertToUpperSnakeCase(enumString);
                    enumArray[i] = (Moves)Enum.Parse(typeof(Moves), normalizedString, true);
                }
                else
                {
                    throw new JsonSerializationException("Unexpected token type when parsing enum array.");
                }
            }

            return enumArray;
        }

        throw new JsonSerializationException("Unexpected token type when parsing enum.");
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value is Moves)
        {
            Moves enumValue = (Moves)value;
            string enumString = ConvertToPascalCase(enumValue.ToString());
            writer.WriteValue(enumString);
        }
        else if (value is Moves[])
        {
            Moves[] enumArray = (Moves[])value;
            writer.WriteStartArray();
            foreach (var enumValue in enumArray)
            {
                WriteJson(writer, enumValue, serializer);
            }

            writer.WriteEndArray();
        }
        else
        {
            throw new JsonSerializationException("Unexpected value type when writing enum.");
        }
    }

    private static string ConvertToUpperSnakeCase(string pascalCase)
    {
        switch (pascalCase)
        {
            case "Poisonpowder":
                return "POISON_POWDER";
            case "Solarbeam":
                return "SOLAR_BEAM";
            case "PinMissle":
                return "PIN_MISSILE";
            case "Thundershock":
                return "THUNDER_SHOCK";
            case "Doubleslap":
                return "DOUBLE_SLAP";
            case "Selfdestruct":
                return "SELF_DESTRUCT";
            case "Sonicboom":
                return "SONIC_BOOM";
            case "Vicegrip":
                return "VICE_GRIP";
            case "HiJumpKick":
                return "HIGH_JUMP_KICK";
            case "Thunderpunch":
                return "THUNDER_PUNCH";
        }

        return _snakeCaseStrategy.GetPropertyName(pascalCase, false).ToUpper();
    }
    
    // ToDo: Test this
    private static string ConvertToPascalCase(string upperSnakeCase)
    {
        return Regex.Replace(upperSnakeCase.ToLower(), @"_([a-z])", m => m.Groups[1].Value.ToUpper());
    }
}
