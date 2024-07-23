﻿using System.Collections.Generic;
using PokemonUnity;
using UnityEngine;

// ToDo: Is this class good?
/// <summary>
/// Hold the Game States such as Events, UI, etc
/// </summary>
public class GameState : Singleton<GameState>
{
    /// <summary>
    /// If true then the game will boot to Oak Intro Scene
    /// </summary>
    public bool startIntroScene;

    public bool isPaused, inGame, atTitleScreen;
    
    public int textChoice, animationChoice, battleChoice;
    
    public bool isPlayingCredits;
    
    
    // ToDo: Where should I put those below:
    
    /// <summary>
    /// The Field Moves on the map
    /// </summary>
    public List<Moves> fieldMoves = new List<Moves>(new Moves[]
    {
        Moves.TELEPORT, Moves.FLY, Moves.CUT, Moves.SURF, Moves.DIG, Moves.STRENGTH, Moves.FLASH, Moves.SOFT_BOILED
    });

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