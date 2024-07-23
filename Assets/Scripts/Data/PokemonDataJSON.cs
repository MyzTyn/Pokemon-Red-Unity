using System;
using System.Collections.Generic;
using System.Data.SQLite;
using PokemonUnity;
using UnityEngine;
using UnityEngine.Windows;

public class PokemonDataJSON : MonoBehaviour
{
    public VersionManager versionManager;
    
    void Awake()
    {
        // ToDo: Remove the PokemonDataJSON
        //this should also be a class list
        //PokemonData.TypeEffectiveness = Serializer.JSONtoObject<Dictionary<Types, Dictionary<Types, float>>>("typeEffectiveness.json");
        PokemonData.pokemonData = Serializer.JSONtoObject<List<PokemonDataEntry>>("pokemonData.json");
        //PokemonData.moves = Serializer.JSONtoObject<List<MoveData>>("moveData.json");
        //PokemonData.itemData = Serializer.JSONtoObject<List<ItemDataEntry>>("itemData.json");
        //also should be changed
        //PokemonData.shopItemsLists = Serializer.JSONtoObject<Dictionary<string, string[]>>("shopItemsData.json");
        
        // Load the PokemonUnity Framework
        Core.Logger = new CustomLogger();
        try
        {
            // Load the Database
            Debug.Assert(File.Exists($"{Application.streamingAssetsPath}/veekun-pokedex.sqlite"),
                "Database file not found");
            Game.DatabasePath = $"Data Source={Application.streamingAssetsPath}/veekun-pokedex.sqlite";
            Game.con = new SQLiteConnection(Game.DatabasePath);
            Game.ResetSqlConnection(Game.DatabasePath);
            
            // Load the Localization
            string frLocalization = $"{Application.streamingAssetsPath}/frLocalization.xml";
            Debug.Assert(File.Exists(frLocalization), "Localization file not found");
            TempLocalizationXML.instance.Initialize(frLocalization, (int)Languages.English);
            //Game.LocalizationDictionary = new XmlStringRes(null);
            // ToDo: Change to English because it is French 😅
            //Game.LocalizationDictionary.Initialize(frLocalization, (int)Languages.English);
            
            // Initialize the GameData
            //Core.pokemonGeneration = (sbyte)Generation.RedBlueYellow;
            Game.GameData.Trainer ??= new PokemonUnity.Trainer("RED", TrainerTypes.PLAYER);
        }
        catch (InvalidOperationException)
        {
            Debug.LogError("Problem executing SQL with connected database");
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            throw;
        }
        
        Debug.Log($"Is Pokemon DB Greater than 0? {(Kernal.PokemonData.Count > 0)} : {Kernal.PokemonData.Count}");
        Debug.Assert(Game.GameData != null, "GameData is null");
    }

    public static void InitVersion()
    {
        PokemonData.encounters = Serializer.JSONtoObject<List<EncounterData>>(GameData.instance.version == Version.Red ? "encounterDataRed.json" : "encounterDataBlue.json");
    }
}