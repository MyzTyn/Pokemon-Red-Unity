//Class for encounter tables.

using System;
using PokemonUnity;

[System.Serializable]
public class EncounterData
{
    public int encounterChance;
    public Tuple<Pokemons, int>[] slots;
}