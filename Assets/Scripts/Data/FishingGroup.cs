using System;

public class FishingGroup
{
    public Tuple<string, int>[] slots;

    public FishingGroup(Tuple<string, int>[] slots)
    {
        this.slots = slots;
    }
}

// May need to reference back to the original code

// //fishing group for good rod
//     public FishingGroup goodRodFishingGroup = new FishingGroup(new Tuple<string, int>[]
//         { new Tuple<string, int>("Goldeen", 10), new Tuple<string, int>("Poliwag", 10) });
//
//     //groups for the super rod
//     public static List<FishingGroup> superRodFishingGroups = new List<FishingGroup>(new FishingGroup[]
//     {
//         new FishingGroup(new Tuple<string, int>[]
//             { new Tuple<string, int>("Tentacool", 15), new Tuple<string, int>("Poliwag", 15) }),
//         new FishingGroup(new Tuple<string, int>[]
//             { new Tuple<string, int>("Goldeen", 15), new Tuple<string, int>("Poliwag", 15) }),
//         new FishingGroup(new Tuple<string, int>[]
//         {
//             new Tuple<string, int>("Psyduck", 15), new Tuple<string, int>("Goldeen", 15),
//             new Tuple<string, int>("Krabby", 15)
//         }),
//         new FishingGroup(new Tuple<string, int>[]
//             { new Tuple<string, int>("Poliwhirl", 23), new Tuple<string, int>("Slowpoke", 15) }),
//         new FishingGroup(new Tuple<string, int>[]
//             { new Tuple<string, int>("Krabby", 15), new Tuple<string, int>("Shellder", 15) }),
//         new FishingGroup(new Tuple<string, int>[]
//         {
//             new Tuple<string, int>("Dratini", 15), new Tuple<string, int>("Krabby", 15),
//             new Tuple<string, int>("Psyduck", 15), new Tuple<string, int>("Slowpoke", 15)
//         }),
//         new FishingGroup(new Tuple<string, int>[]
//         {
//             new Tuple<string, int>("Tentacool", 5), new Tuple<string, int>("Krabby", 15),
//             new Tuple<string, int>("Goldeen", 15), new Tuple<string, int>("Magikarp", 15)
//         }),
//         new FishingGroup(new Tuple<string, int>[]
//         {
//             new Tuple<string, int>("Staryu", 15), new Tuple<string, int>("Horsea", 15),
//             new Tuple<string, int>("Shellder", 15), new Tuple<string, int>("Goldeen", 15)
//         }),
//         new FishingGroup(new Tuple<string, int>[]
//         {
//             new Tuple<string, int>("Slowbro", 23), new Tuple<string, int>("Seaking", 23),
//             new Tuple<string, int>("Kingler", 23), new Tuple<string, int>("Seadra", 23)
//         }),
//         new FishingGroup(new Tuple<string, int>[]
//         {
//             new Tuple<string, int>("Seaking", 23), new Tuple<string, int>("Krabby", 15),
//             new Tuple<string, int>("Goldeen", 15), new Tuple<string, int>("Magikarp", 15)
//         }),
//     });
//     
//     // ToDo: Remove TypeEffectiveness and Move shopItemsLists somewhere
//     public static Dictionary<Types, Dictionary<Types, float>> TypeEffectiveness =
//         new Dictionary<Types, Dictionary<Types, float>>();
//     public static Dictionary<string, string[]> shopItemsLists = new Dictionary<string, string[]>();
//
//     public static Moves[] TMHMMoves =
//     {
//         Moves.MEGA_PUNCH,
//         Moves.RAZOR_WIND,
//         Moves.SWORDS_DANCE,
//         Moves.WHIRLWIND,
//         Moves.MEGA_KICK,
//         Moves.TOXIC,
//         Moves.HORN_DRILL,
//         Moves.BODY_SLAM,
//         Moves.TAKE_DOWN,
//         Moves.DOUBLE_EDGE,
//         Moves.BUBBLE_BEAM,
//         Moves.WATER_GUN,
//         Moves.ICE_BEAM,
//         Moves.BLIZZARD,
//         Moves.HYPER_BEAM,
//         Moves.PAY_DAY,
//         Moves.SUBMISSION,
//         Moves.COUNTER,
//         Moves.SEISMIC_TOSS,
//         Moves.RAGE,
//         Moves.MEGA_DRAIN,
//         Moves.SOLAR_BEAM,
//         Moves.DRAGON_RAGE,
//         Moves.THUNDERBOLT,
//         Moves.THUNDER,
//         Moves.EARTHQUAKE,
//         Moves.FISSURE,
//         Moves.DIG,
//         Moves.PSYCHIC,
//         Moves.TELEPORT,
//         Moves.MIMIC,
//         Moves.DOUBLE_TEAM,
//         Moves.REFLECT,
//         Moves.BIDE,
//         Moves.METRONOME,
//         Moves.SELF_DESTRUCT,
//         Moves.EGG_BOMB,
//         Moves.FIRE_BLAST,
//         Moves.SWIFT,
//         Moves.SKULL_BASH,
//         Moves.SOFT_BOILED,
//         Moves.DREAM_EATER,
//         Moves.SKY_ATTACK,
//         Moves.REST,
//         Moves.THUNDER_WAVE,
//         Moves.PSYWAVE,
//         Moves.EXPLOSION,
//         Moves.ROCK_SLIDE,
//         Moves.TRI_ATTACK,
//         Moves.SUBSTITUTE,
//         Moves.CUT,
//         Moves.FLY,
//         Moves.SURF,
//         Moves.STRENGTH,
//         Moves.FLASH
//     };