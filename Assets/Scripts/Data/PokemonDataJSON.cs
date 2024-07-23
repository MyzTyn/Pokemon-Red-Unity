using System;
using System.Collections.Generic;
using System.Data.SQLite;
using PokemonUnity;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Windows;
using Types = PokemonUnity.Types;


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
        try
        {
            Debug.Assert(File.Exists($"{Application.streamingAssetsPath}/veekun-pokedex.sqlite"),
                "Database file not found");
            Game.DatabasePath = $"Data Source={Application.streamingAssetsPath}/veekun-pokedex.sqlite";
            Game.con = new SQLiteConnection(Game.DatabasePath);
            Game.ResetSqlConnection(Game.DatabasePath);
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
    }

    public static void InitVersion()
    {
        PokemonData.encounters = Serializer.JSONtoObject<List<EncounterData>>(GameData.instance.version == Version.Red ? "encounterDataRed.json" : "encounterDataBlue.json");
    }
}