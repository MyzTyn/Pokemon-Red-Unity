using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using PokemonUnity;
using UnityEngine;

// The DataLoader is responsible for loading all the data needed for the game to run
public class DataLoader : Singleton<DataLoader>
{
    public RenderTexture mainRender, postRender;
    [HideInInspector] public RenderTexture templateRenderTexture;
    
    // Do we need this?
    public VersionManager versionManager;
    
    [HideInInspector] public Sprite[] frontMonSprites, backMonSprites;

    /// <summary>
    /// The opponent name
    /// </summary>
    public string rivalName;

    public int coins;
    public int hours, minutes, seconds;
    public FontAtlas fontAtlas;
    
    // Data
    public List<EncounterData> encounters = new List<EncounterData>();
    /* Encounter Table Indices:
    0:Diglett Cave
    1:Mansion 1
    2:Mansion 2
    3:Mansion 3
    4:Mansion B1
    5:Mt Moon 1
    6:Mt Moon B1
    7:Mt Moon B2
    8:Pokemon Tower 1
    9:Pokemon Tower 2
    10:Pokemon Tower 3
    11:Pokemon Tower 4
    12:Pokemon Tower 5
    13:Power Plant
    14:Rock Tunnel 1
    15:Rock Tunnel 2
    16:Route 1
    17:Route 2
    18:Route 3
    19:Route 4
    20:Route 5
    21:Route 6
    22:Route 7
    23:Route 8
    24:Route 9
    25:Route 10
    26:Route 11
    27:Route 12
    28:Route 13
    29:Route 14
    30:Route 15
    31:Route 16
    32:Route 17
    33:Route 18
    34:Route 21
    35:Route 22
    36:Route 23
    37:Route 24
    38:Route 25
    39:Safari Zone 1
    40:Safari Zone 2
    41:Safari Zone 3
    42:Safari Zone Center
    43:Seafoam Island 1
    44:Seafoam Island B1
    45:Seafoam Island B2
    46:Seafoam Island B3
    47:Seafoam Island B4
    48:Unknown Dungeon 1
    49:Unknown Dungeon 2
    50:Unknown Dungeon B1
    51:Victory Road 1
    52:Victory Road 2
    53:Victory Road 3
    54:Viridian Forest
    55:Water Pokemon
     */
    // Just for sprite ToDo: Refactor this
    public List<PokemonDataEntry> pokemonData = new List<PokemonDataEntry>();
    
    private void Awake()
    {
        // ToDo: Remove the PokemonDataJSON
        pokemonData = Serializer.JSONtoObject<List<PokemonDataEntry>>("pokemonData.json");
        
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
            
            // Initialize the Encounter Data 
            encounters = Serializer.JSONtoObject<List<EncounterData>>(versionManager.version == Version.Red ? "encounterDataRed.json" : "encounterDataBlue.json");
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
        
        // Load Assets
        frontMonSprites = Resources.LoadAll<Sprite>("frontmon");
        backMonSprites = Resources.LoadAll<Sprite>("backmon");
        
        // Create Render Textures
        postRender = new RenderTexture(160, 144, 1);
        postRender.filterMode = FilterMode.Point;
        templateRenderTexture = new RenderTexture(mainRender);
        Camera.main.targetTexture = mainRender;
        
        // Load Events
        // ...
        
        // InitVersion
        FontAtlasInit();
        
        // Set the default:
        rivalName = "GARY";
        PokemonUnity.Game.GameData.Trainer.Money = 3000;
        //GameData.instance.coins = 300;
    }
    
    private void FontAtlasInit()
    {
        if (versionManager.version == Version.Blue)
        {
            for (int i = 0; i < 6; i++)
            {
                fontAtlas.fontChars[i + 92] = fontAtlas.blueSlotsChars[i];
            }
        }
        else
        {
            for (int i = 0; i < 6; i++)
            {
                fontAtlas.fontChars[i + 92] = fontAtlas.redSlotsChars[i];
            }
        }
    }
}