using System.Collections.Generic;
using PokemonUnity;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Pokedex : MonoBehaviour
{
    public GameObject entriescontainer;
    public List<GameObject> entries;
    public GameCursor cursor;
    public CustomText seentext, owntext;
    public int selectedSlot, topSlotIndex;
    public bool selectingMon;
    public ViewBio viewBio;

    int seen => PokemonUnity.Game.GameData.Trainer.pokedexSeen();

    int caught => PokemonUnity.Game.GameData.Trainer.pokedexOwned();

    public static Pokedex instance;

    void Awake()
    {
        instance = this;
        entries = new List<GameObject>();
        entries.Clear();
        for (int i = 0; i < 7; i++)
        {
            entries.Add(entriescontainer.transform.GetChild(i).gameObject);
        }
    }

    public void Init()
    {
        topSlotIndex = 1;
        selectedSlot = 0;
        seentext.text = seen.ToString();
        owntext.text = caught.ToString();

        UpdateScreen();
    }

    void UpdateScreen()
    {
        for (int i = 0; i < 7; i++)
        {
            int slotNo = topSlotIndex + i;
            Pokemons pokemon = (Pokemons)slotNo;
            bool hasSeen = PokemonUnity.Game.GameData.Trainer.hasSeen(pokemon);
            bool hasCaught = PokemonUnity.Game.GameData.Trainer.hasOwned(pokemon);

            entries[i].transform.GetChild(0).GetComponent<CustomText>().text = slotNo.ZeroFormat("00x") + "\n" +
                                                                               (!hasSeen
                                                                                   ? "   ----------"
                                                                                   : "   " + PokemonData.IndexToMon(
                                                                                       slotNo));
            entries[i].transform.GetChild(1).gameObject.SetActive(hasCaught);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (!selectingMon) cursor.SetPosition(0, 112 - 16 * selectedSlot);
        if (!cursor.isActive) cursor.SetActive(true);
        if (viewBio.displayingBio) cursor.SetActive(false);

        if (MainMenu.instance.currentmenu == MainMenu.instance.pokedexmenu)
        {
            if (InputManager.Pressed(Button.B) && !viewBio.displayingBio)
            {
                SoundManager.instance.PlayABSound();
                if (Player.instance.isDisabled) Player.instance.isDisabled = false;
                Debug.Log(Player.instance.isDisabled);
                if (selectingMon) selectingMon = false;
                else
                {
                    InputManager.Enable(Button.Start);

                    MainMenu.instance.currentmenu = MainMenu.instance.thismenu;

                    gameObject.SetActive(false);
                }
            }

            if (InputManager.Pressed(Button.A) && !viewBio.displayingBio)
            {
                SoundManager.instance.PlayABSound();

                if (!selectingMon && PokemonUnity.Game.GameData.Trainer.hasSeen((Pokemons)topSlotIndex + selectedSlot - 1))
                {
                    selectingMon = true;
                    cursor.SetPosition(120, 56);
                }
                else if (PokemonUnity.Game.GameData.Trainer.hasSeen((Pokemons)topSlotIndex + selectedSlot - 1))
                {
                    StartCoroutine(viewBio.DisplayABio(topSlotIndex + selectedSlot));
                }
            }

            if (InputManager.Pressed(Button.Down))
            {
                if (!selectingMon)
                {
                    selectedSlot++;

                    if (selectedSlot > 6)
                    {
                        selectedSlot = 6;
                        // ToDo: use Core.PokemonIndexLimit when the DLLs are updated
                        if (topSlotIndex < 151 - 6)
                        {
                            topSlotIndex += 1;
                        }

                        UpdateScreen();
                    }
                }
            }

            if (InputManager.Pressed(Button.Up))
            {
                if (!selectingMon)
                {
                    selectedSlot--;

                    if (selectedSlot < 0)
                    {
                        selectedSlot = 0;
                        if (topSlotIndex > 1)
                        {
                            topSlotIndex -= 1;
                        }

                        UpdateScreen();
                    }
                }
            }

            if (InputManager.Pressed(Button.Right))
            {
                if (!selectingMon)
                {
                    topSlotIndex += 10;
                    // ToDo: use Core.PokemonIndexLimit when the DLLs are updated
                    if (topSlotIndex > 151 - 6)
                        topSlotIndex = 151 - 6;
                    UpdateScreen();
                }
            }

            if (InputManager.Pressed(Button.Left))
            {
                if (!selectingMon)
                {
                    topSlotIndex -= 10;
                    if (topSlotIndex < 1) topSlotIndex = 1;
                    UpdateScreen();
                }
            }
        }
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(Pokedex))]
public class PokedexDebug : Editor
{
    public int OverwriteIndex;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        OverwriteIndex = EditorGUILayout.IntField("Selected Index", OverwriteIndex);
        
        if (GUILayout.Button("Set Pokedex Entry to Seen"))
        {
            PokemonUnity.Game.GameData.Trainer.setSeen((Pokemons)OverwriteIndex + 1);
        }

        if (GUILayout.Button("Set Pokedex Entry to Owned"))
        {
            PokemonUnity.Game.GameData.Trainer.setOwned((Pokemons)OverwriteIndex + 1);
        }
    }
}
#endif