using System.Collections.Generic;
using UnityEngine;
using System;
using PokemonUnity;

public static class IntExtensions
{
    /*
    The first two should either be improved or just removed and have number formatting
    for different things be done directly (just use string.Format or ToString instead directly
    rather than use these functions)
    */
    public static string ZeroFormat(this int input, string zeroformat)
    {
        switch (zeroformat)
        {
            case "0x":
                return (input > 9 ? "" : "0") + input;
            case "00x":
                return (input > 99 ? "" : input > 9 ? "0" : "00") + input;
            case "000x":
                return (input > 999 ? "" : input > 99 ? "0" : input > 9 ? "00" : "000") + input;
        }

        throw new UnityException("Incorrect format");
    }


    public static string SpaceFormat(this int input, int format)
    {
        switch (format)
        {
            case 2:
                return (input < 10 ? " " : "") + input;
            case 3:
                return (input < 10 ? "  " : input < 100 ? " " : "") + input;
        }

        throw new UnityException("Incorrect format");
    }

    public static int UnderflowUInt24(this int input)
    {
        //underflow function for the exp bug
        if (input < 0)
        {
            input += (int)Mathf.Pow(2, 24);
        }
        else if (input >= Mathf.Pow(2, 24)) input %= (int)Mathf.Pow(2, 24);

        return input;
    }
}

//Class containing all the core data of the game.
public class GameData : Singleton<GameData>
{
    public List<Moves> fieldMoves = new List<Moves>(new Moves[]
    {
        Moves.TELEPORT, Moves.FLY, Moves.CUT, Moves.SURF, Moves.DIG, Moves.STRENGTH, Moves.FLASH, Moves.SOFT_BOILED
    });

    [HideInInspector] public Sprite[] frontMonSprites, backMonSprites;
    public bool isPaused, inGame, atTitleScreen;
    public int coins;
    public int textChoice, animationChoice, battleChoice;
    public string rivalName;
    public int hours, minutes, seconds;
    public bool hasMetBill; //should Bill's PC use his name?
    public bool isPlayingCredits;
    public Version version;
    public FontAtlas fontAtlas;
    public bool[] eventsArray;

    public void SetEvent(Events eventToSet, bool state)
    {
        eventsArray[(int)eventToSet] = state;
    }

    public bool CheckEvent(Events eventToCheck)
    {
        return eventsArray[(int)eventToCheck];
    }


    public void Init()
    {
        frontMonSprites = Resources.LoadAll<Sprite>("frontmon");
        backMonSprites = Resources.LoadAll<Sprite>("backmon");
        //Set the events bool array to have an entry for each event according to the enum
        eventsArray = new bool[Enum.GetNames(typeof(Events)).Length];

        //The default name in the original if no name was given is NINTEN, and SONY for the rival
        if (rivalName == "") rivalName = "GARY";
    }

    //encounter table indices for all maps
    [HideInInspector] public int[] MapGrassEncounterTableIndices =
    {
        -1, //Pallet Town
        -1, //Oak's Lab
        16, //Route 1
        -1, //Viridian City
        -1, //Poke Center
        -1, //Poke Mart
        -1, //Pokemon Gym
        35, //Route 22
        36, //Route 23
        51, //VR 1
        52, //VR 2
        53, //VR 3
        -1, //Indigo Plateau
        -1, //Lorelei
        -1, //Bruno
        -1, //Agatha
        -1, //Hall of Fame Room
        17, //Route 2
        54, //Viridian Forest
        0, //Diglett Cave
        -1, //Pewter City
        18, //Route 3
        3, //Mt. Moon 1
        4, //Mt Moon B1
        5, //Mt Moon B2
        19, //Route 4
        -1, //Cerulean City
        37, //Route 24
        38, //Route 25
        20, //Route 5
        -1, //Underground Road
        21, //Route 6
        -1, //Vermillion City
        -1, //S.S. Anne
        26, //Route 11
        24, //Route 9
        25, //Route 10
        14, //Rock Tunnel 1
        15, //Rock Tunnel 2
        13, //Power Plant
        -1, //Lavender Town
        -1, //Pokemon Tower 1
        -1, //Pokemon Tower 2
        8, //Pokemon Tower 3
        9, //Pokemon Tower 4
        10, //Pokemon Tower 5
        11, //Pokemon Tower 6
        12, //Pokemon Tower 7
        23, //Route 8
        22, //Route 7
        -1, //Celadon City
        -1, //Game Corner
        -1, //Rocket Hideout
        31, //Route 16
        32, //Route 17
        33, //Route 18
        -1, //Fuchsia City
        42, //Safari Zone Center
        39, //Safari Zone East
        40, //Safari Zone North
        41, //Safari Zone West
        -1, //Safari Zone House
        30, //Route 15
        29, //Route 14
        28, //Route 13
        27, //Route 12
        -1, //Saffron City
        -1, //Silph Co.
        -1, //Sea Route 19
        43, //Seafoam Islands 1
        44, //Seafoam Islands B1
        45, //Seafoam Islands B2
        46, //Seafoam Islands B3
        47, //Seafoam Islands B4
        -1, //Sea Route 20
        -1, //Cinnabar Island
        1, //Mansion 1
        2, //Mansion 2
        3, //Mansion 3
        4, //Mansion 4
        34, //Sea Route 21
        48, //Unknown 1
        49, //Unknown 2
        50, //Unknown 3
        -1, //Trade Center
        -1, //Colloseum
        -1, //Bill's House
        -1, //Houses
        -1,
        -1
    };

    //List of maps that have water encounters
    [HideInInspector] public List<Map> WaterEncounterMaps = new List<Map>(
        new Map[]
        {
            Map.Route19,
            Map.Route20,
            Map.Route21,
            Map.Route23,
            Map.Route12,
            Map.SeafoamIslands1,
            Map.SeafoamIslands2,
            Map.SeafoamIslands3,
            Map.SeafoamIslands4,
            Map.SeafoamIslands5,
            Map.Unknown1,
            Map.Unknown3,
        }
    );
}