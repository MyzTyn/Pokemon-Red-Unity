using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PokemonEssentials.Interface;
using PokemonEssentials.Interface.PokeBattle;
using PokemonUnity;
using PokemonUnity.Monster;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class
    PartyAnim //maybe make this into a struct instead? (and other similar small classes with only variables) do more research and experimentation w/ structs in the future
{
    public List<Sprite> anim;
}

// ToDo: Redesign this class to avoid using IsNotNullOrNone()
public class PokemonMenu : MonoBehaviour
{
    public GameObject mainwindow, switchstats, stats1, stats2;
    public GameObject currentMenu;
    public GameObject[] allmenus;
    public List<GameObject> partyslots;
    public int selectedOption;
    public GameCursor cursor;
    public int selectedMon;
    public int numberofPokemon;
    public bool switching, selectingPokemon;
    public Sprite[] switchMenuSprites;
    public Image switchMenuImage;
    public Image[] healthbars = new Image[6];
    public GameObject[] fieldMoveObj;

    public CustomText[] fieldMoveText;

    //STATS1DATA
    public Image stats1portrait;
    public Image stat1bar;

    public CustomText pokedexNO,
        attacktext,
        speedtext,
        specialtext,
        defensetext,
        MonStatustext,
        monhptext,
        monnametext,
        montype1,
        montype2,
        ownerSpeciestext,
        ownernametext,
        monLeveltext,
        monstatustext;

    //STATS2DATA
    public Image stats2portrait;
    public CustomText movetext, exptext, explefttoLeveltext, nextLeveltext, monname2text, pokedexno2;
    public List<PartyAnim> partyanims;
    IPokemon highlightedmon;
    public float partyAnimTimer = 0;
    public int switchMenuOffset, switchMenuOffsetX;
    public string[] fieldMoveNames = new string[4];
    public static PokemonMenu instance;


    void Awake()
    {
        instance = this;
    }

    public void Initialize()
    {
        switchMenuOffset = 0;
        if (!Game.GameData.Trainer.party.Any())
        {
            currentMenu = mainwindow;
            MainMenu.instance.currentmenu = MainMenu.instance.thismenu;
            this.gameObject.SetActive(false);
            return;
        }

        for (int i = 0; i < PokemonUnity.Game.GameData.Trainer.party.Count(); i++)
        {
            if (!PokemonUnity.Game.GameData.Trainer.party[i].IsNotNullOrNone())
                continue;
            
            int pixelCount = Mathf.RoundToInt((float)PokemonUnity.Game.GameData.Trainer.party[i].HP * 48 /
                                              (float)PokemonUnity.Game.GameData.Trainer.party[i].TotalHP);
            healthbars[i] = partyslots[i].transform.GetChild(1).GetChild(0).GetComponent<Image>();
            healthbars[i].fillAmount = (float)pixelCount / 48;
            UpdateMainMenu();
        }

        UpdateScreen();
        Dialogue.instance.Deactivate();
        Dialogue.instance.fastText = true;
        Dialogue.instance.keepTextOnScreen = true;
        Dialogue.instance.needButtonPress = false;
        StartCoroutine(Dialogue.instance.text("Choose a POKéMON."));
        Dialogue.instance.finishedText = true;
    }

    public void UpdateScreen()
    {
        int index = 0;
        foreach (IPokemon pokemon in PokemonUnity.Game.GameData.Trainer.party)
        {
            if (!pokemon.IsNotNullOrNone())
                continue;
            
            Transform slottransform = partyslots[index].transform;
            int pixelCount = Mathf.RoundToInt((float)pokemon.HP * 48 / (float)pokemon.TotalHP);
            slottransform.GetChild(1).GetChild(0).GetComponent<Image>().fillAmount = (float)pixelCount / 48;
            slottransform.GetChild(2).GetComponent<CustomText>().text = pokemon.Name;
            slottransform.GetChild(3).GetComponent<CustomText>().text =
                ((pokemon.Level < 100) ? "<LEVEL>" : "") + pokemon.Level;
            slottransform.GetChild(4).GetComponent<CustomText>().text =
                (pokemon.HP > 99 ? "" : pokemon.HP > 9 ? " " : "  ") + pokemon.HP +
                (pokemon.TotalHP > 99 ? "/" : pokemon.TotalHP > 9 ? "/ " : "/  ") + pokemon.TotalHP;
            index++;
        }
    }

    public void UpdateMainMenu()
    {
        selectedOption = selectedMon;
        cursor.SetActive(true);
        for (int l = 0; l < 6; l++)
        {
            partyslots[l].SetActive(false);
        }

        for (int i = 0; i < PokemonUnity.Game.GameData.Trainer.party.Count(); i++)
        {
            if (i == 0)
            {
                numberofPokemon = 0;
            }
            
            if (PokemonUnity.Game.GameData.Trainer.party[i].IsNotNullOrNone())
            {
                partyslots[i].SetActive(true);
                numberofPokemon++;
            }
        }

        UpdateMenus();
    }

    public void UpdateSwitch()
    {
        cursor.SetPosition(96 - 16 * switchMenuOffsetX, 40 - 16 * selectedOption + switchMenuOffset * 16);
        UpdateMenus();
    }

    public void UpdateStats1()
    {
        cursor.SetActive(false);
        stats1portrait.sprite = GameData.instance.frontMonSprites[(int)PokemonUnity.Game.GameData.Trainer.party[selectedMon].Species - 1];
        int Species = (int)PokemonUnity.Game.GameData.Trainer.party[selectedMon].Species;
        pokedexNO.text = (Species > 99 ? "" : Species > 9 ? "0" : "00") + Species.ToString();
        attacktext.text = PokemonUnity.Game.GameData.Trainer.party[selectedMon].ATK.ToString();
        speedtext.text = PokemonUnity.Game.GameData.Trainer.party[selectedMon].SPE.ToString();
        specialtext.text = PokemonUnity.Game.GameData.Trainer.party[selectedMon].SPA.ToString();
        defensetext.text = PokemonUnity.Game.GameData.Trainer.party[selectedMon].DEF.ToString();
        PokemonUnity.Types type1 = PokemonUnity.Game.GameData.Trainer.party[selectedMon].Type1,
            type2 = PokemonUnity.Game.GameData.Trainer.party[selectedMon].Type2;
        montype1.text = type1.ToString();
        string type2String = type2.ToString();
        montype2.text = type2String != "" ? ("TYPE2/" + "\n " + type2String) : "";
        monnametext.text = PokemonUnity.Game.GameData.Trainer.party[selectedMon].Name;
        // ToDo: Fix the OT
        //ownerSpeciestext.text = PokemonUnity.Game.GameData.Trainer.party[selectedMon].ownerSpecies.ToString();
        ownerSpeciestext.text = "";
        //ownernametext.text = PokemonUnity.Game.GameData.Trainer.party[selectedMon].OT.name;
        ownernametext.text = "";
        int pixelCount = Mathf.RoundToInt((float)PokemonUnity.Game.GameData.Trainer.party[selectedMon].HP * 48 /
                                          (float)PokemonUnity.Game.GameData.Trainer.party[selectedMon].TotalHP);
        stat1bar.fillAmount = (float)pixelCount / 48;
        monhptext.text =
            (PokemonUnity.Game.GameData.Trainer.party[selectedMon].HP > 99 ? "" :
                PokemonUnity.Game.GameData.Trainer.party[selectedMon].HP > 9 ? " " : "  ") +
            PokemonUnity.Game.GameData.Trainer.party[selectedMon].HP + " " + PokemonUnity.Game.GameData.Trainer.party[selectedMon].TotalHP;
        monLeveltext.text = ((PokemonUnity.Game.GameData.Trainer.party[selectedMon].Level < 100) ? "<Level>" : "") +
                            PokemonUnity.Game.GameData.Trainer.party[selectedMon].Level.ToString();

        switch (PokemonUnity.Game.GameData.Trainer.party[selectedMon].Status)
        {
            case PokemonUnity.Status.NONE:
                monstatustext.text = "OK";
                break;
            case PokemonUnity.Status.FAINT:
                monstatustext.text = "FNT";
                break;
            case PokemonUnity.Status.FROZEN:
                monstatustext.text = "FRZ";
                break;
            case PokemonUnity.Status.BURN:
                monstatustext.text = "BRN";
                break;
            case PokemonUnity.Status.PARALYSIS:
                monstatustext.text = "PRZ";
                break;
            case PokemonUnity.Status.POISON:
                monstatustext.text = "PSN";
                break;
            case PokemonUnity.Status.SLEEP:
                monstatustext.text = "SLP";
                break;
        }

        UpdateMenus();
    }

    public void UpdateStats2()
    {
        cursor.SetActive(false);
        string movestr = "";
        for (int i = 0; i < 4; i++)
        {
            if (PokemonUnity.Game.GameData.Trainer.party[selectedMon].moves[i].IsNotNullOrNone())
            {
                IMove move = PokemonUnity.Game.GameData.Trainer.party[selectedMon].moves[i];
                movestr += (i > 0 ? "\n" : "") + move.id.ToString().ToUpper() + "\n" + "         " + "PP " +
                           (move.PP < 10 ? " " : "") + move.PP + "/" + (move.TotalPP < 10 ? " " : "") + move.TotalPP;
            }
            else
                movestr += (i > 0 ? "\n" : "") + "-" + "\n" + "         " + "--";
        }

        movetext.text = movestr;
        stats2portrait.sprite = GameData.instance.frontMonSprites[(int)PokemonUnity.Game.GameData.Trainer.party[selectedMon].Species - 1];
        monname2text.text = PokemonUnity.Game.GameData.Trainer.party[selectedMon].Name;

        exptext.text = TruncateExpNumber(PokemonUnity.Game.GameData.Trainer.party[selectedMon].Exp.ToString());
        explefttoLeveltext.text = TruncateExpNumber(((Pokemon)PokemonUnity.Game.GameData.Trainer.party[selectedMon]).Experience.NextLevel.ToString());
        nextLeveltext.text = (PokemonUnity.Game.GameData.Trainer.party[selectedMon].Level < 99
            ? "<Level>" + (PokemonUnity.Game.GameData.Trainer.party[selectedMon].Level + 1).ToString()
            : 100.ToString());
        int Species = (int)PokemonUnity.Game.GameData.Trainer.party[selectedMon].Species;
        pokedexno2.text = (Species > 99 ? "" : Species > 9 ? "0" : "00") + Species.ToString();
        UpdateMenus();
    }

    string TruncateExpNumber(string num)
    {
        if (num.Length > 6)
        {
            return num.Substring(num.Length - 6); //truncate the number to the last 6 digits
        }
        else return num;
    }

    void UpdateMenus()
    {
        foreach (GameObject menu in allmenus)
        {
            if (menu != currentMenu)
            {
                menu.SetActive(false);
            }
            else
            {
                menu.SetActive(true);
            }

            if (menu == switchstats && (currentMenu == mainwindow))
            {
                menu.SetActive(false);
            }

            if (menu == mainwindow && (currentMenu == switchstats))
            {
                menu.SetActive(true);
            }
        }

        if (currentMenu != switchstats) DisableFieldText();
    }

    void DisableFieldText()
    {
        switchMenuOffsetX = 0;
        for (int i = 0; i < 4; i++)
        {
            fieldMoveObj[i].SetActive(false);
        }
    }

    void Update()
    {
        if (currentMenu == switchstats)
        {
            if (InputManager.Pressed(Button.Down))
            {
                selectedOption++;
                MathE.Clamp(ref selectedOption, 0, 2 + switchMenuOffset);
                UpdateSwitch();
            }

            if (InputManager.Pressed(Button.Up))
            {
                selectedOption--;
                MathE.Clamp(ref selectedOption, 0, 2 + switchMenuOffset);
                UpdateSwitch();
            }
        }

        if (currentMenu == mainwindow)
        {
            highlightedmon = PokemonUnity.Game.GameData.Trainer.party[selectedOption];
            partyAnimTimer += 1;
            float hpratio = (float)highlightedmon.HP / (float)highlightedmon.TotalHP;
            float animLoopTime =
                hpratio > .5f ? 10 : hpratio > .21f ? 32 : 64; //the animation takes 10,32, or 64 frames

            if (partyAnimTimer == animLoopTime)
            {
                partyAnimTimer = 0;
            }

            foreach (IPokemon pokemon in PokemonUnity.Game.GameData.Trainer.party)
            {
                if (!pokemon.IsNotNullOrNone())
                    continue;
                
                // ToDo: Remove .ToList()
                Transform slottransform = partyslots[PokemonUnity.Game.GameData.Trainer.party.ToList().IndexOf(pokemon)].transform;
                if (PokemonUnity.Game.GameData.Trainer.party.ToList().IndexOf(pokemon) != selectedOption)
                {
                    slottransform.GetChild(0).GetComponent<Image>().sprite =
                        partyanims[PokemonData.pokemonData[(int)pokemon.Species - 1].partySprite].anim[0];
                }
                else
                {
                    slottransform.GetChild(0).GetComponent<Image>().sprite =
                        partyanims[PokemonData.pokemonData[(int)pokemon.Species - 1].partySprite]
                            .anim[Mathf.FloorToInt(2f * partyAnimTimer / animLoopTime)];
                }
            }


            cursor.SetPosition(0, 128 - 16 * selectedOption);


            if (Dialogue.instance.finishedText || selectingPokemon)
            {
                if (InputManager.Pressed(Button.Down))
                {
                    selectedOption++;
                    MathE.Clamp(ref selectedOption, 0, numberofPokemon - 1);
                }

                if (InputManager.Pressed(Button.Up))
                {
                    selectedOption--;
                    MathE.Clamp(ref selectedOption, 0, numberofPokemon - 1);
                }
            }
        }

        if (InputManager.Pressed(Button.B) && Dialogue.instance.finishedText)
        {
            SoundManager.instance.PlayABSound();
            if (currentMenu == mainwindow)
            {
                InputManager.instance.DisableForSeconds(Button.B, 0.2f);
                Dialogue.instance.fastText = false;
                Dialogue.instance.Deactivate();
                InputManager.Enable(Button.Start);
                MainMenu.instance.currentmenu = MainMenu.instance.thismenu;
                this.gameObject.SetActive(false);
            }
            else if (currentMenu == switchstats)
            {
                selectedOption = selectedMon;
                currentMenu = mainwindow;
                UpdateMainMenu();
            }
        }

        if (InputManager.Pressed(Button.A) && Dialogue.instance.finishedText)
        {
            SoundManager.instance.PlayABSound();

            if (currentMenu == mainwindow)
            {
                if (!switching)
                {
                    int textOffsetLength = 4;
                    switchMenuOffsetX = 0;
                    switchMenuOffset = 0;
                    int numberOfFieldMoves = 0;
                    int selectedMenu = 0;
                    selectedMon = selectedOption;
                    IPokemon selectedPokemon = PokemonUnity.Game.GameData.Trainer.party[selectedMon];

                    for (int i = 0; i < 4; i++)
                    {
                        fieldMoveNames[i] = "";
                        fieldMoveObj[i].SetActive(false);
                    }

                    for (int i = 0; i < 4; i++)
                    {
                        if (selectedPokemon.moves[i].IsNotNullOrNone())
                        {
                            IMove move = selectedPokemon.moves[i];

                            if (isFieldMove(move))
                            {
                                numberOfFieldMoves++;

                                if (move.id.ToString().Length > 6 && selectedMenu != 8)
                                {
                                    selectedMenu = 4;
                                    textOffsetLength = 2;
                                }

                                if (move.id.ToString().Length > 8)
                                {
                                    selectedMenu = 8;
                                    textOffsetLength = 0;
                                }

                                fieldMoveNames[switchMenuOffset] = move.id.ToString();
                                fieldMoveObj[3 - switchMenuOffset].SetActive(true);

                                for (int j = 0; j < textOffsetLength; j++)
                                {
                                    fieldMoveText[3 - switchMenuOffset].text += " ";
                                }

                                switchMenuOffset++;
                            }
                        }
                    }

                    for (int i = 0; i < numberOfFieldMoves; i++)
                    {
                        fieldMoveText[(4 - switchMenuOffset) + i].text += fieldMoveNames[i].ToUpper();
                    }

                    switchMenuOffsetX = selectedMenu / 4;
                    selectedMenu += switchMenuOffset;
                    switchMenuImage.sprite = switchMenuSprites[selectedMenu];

                    selectedOption = 0;
                    currentMenu = switchstats;
                    UpdateSwitch();
                }
                else
                {
                    Dialogue.instance.Deactivate();
                    StartCoroutine(Switch());
                    UpdateScreen();
                    StartCoroutine(Dialogue.instance.text("Choose a POKéMON."));
                    Dialogue.instance.finishedText = true;
                    switching = false;
                }
            }
            else if (currentMenu == switchstats)
            {
                if (selectedOption < switchMenuOffset)
                {
                    StartCoroutine(UseFieldMove(fieldMoveNames[selectedOption]));
                }

                if (selectedOption == switchMenuOffset)
                {
                    SoundManager.instance.SetMusicLow();
                    SoundManager.instance.PlayCry((int)PokemonUnity.Game.GameData.Trainer.party[selectedMon].Species - 1);
                    Dialogue.instance.Deactivate();
                    currentMenu = stats1;
                    UpdateStats1();
                }
                else if (selectedOption == switchMenuOffset + 1)
                {
                    switching = true;
                    selectedOption = selectedMon;
                    currentMenu = mainwindow;
                    StartCoroutine(Dialogue.instance.text("Move #MON&lwhere?"));
                    Dialogue.instance.finishedText = true;
                    UpdateMainMenu();
                }
                else if (selectedOption == switchMenuOffset + 2)
                {
                    selectedOption = selectedMon;
                    currentMenu = mainwindow;
                    UpdateMainMenu();
                }
            }
            else if (currentMenu == stats1)
            {
                currentMenu = stats2;
                UpdateStats2();
            }
            else if (currentMenu == stats2)
            {
                SoundManager.instance.SetMusicNormal();
                Dialogue.instance.Deactivate();
                Dialogue.instance.fastText = true;
                Dialogue.instance.keepTextOnScreen = true;
                Dialogue.instance.needButtonPress = false;
                StartCoroutine(Dialogue.instance.text("Choose a POKéMON."));
                Dialogue.instance.finishedText = true;
                selectedOption = selectedMon;
                currentMenu = mainwindow;
                UpdateMainMenu();
            }
        }
    }

    IEnumerator UseFieldMove(string whatFieldMove)
    {
        string monName = PokemonUnity.Game.GameData.Trainer.party[selectedMon].Name;

        if (whatFieldMove == "Cut")
        {
            if (Player.instance.facingTree)
            {
                currentMenu = mainwindow;
                UpdateMenus();
                CloseMenu();
                Player.instance.Cut(monName);
                this.gameObject.SetActive(false);
                yield return 0;
            }
            else
            {
                currentMenu = mainwindow;
                UpdateMainMenu();
                yield return Dialogue.instance.text("There isn't&lanything to CUT!");
                //TODO: implement cutting grass
            }
        }

        if (whatFieldMove == "Surf")
        {
            Player.instance.UpdateFacedTile();
            if (Player.instance.facedTile.hasTile && Player.instance.facedTile.isWater)
            {
                SoundManager.instance.PlaySong(Music.Ocean);
                currentMenu = mainwindow;
                UpdateMainMenu();
                yield return Dialogue.instance.text(PokemonUnity.Game.GameData.Trainer.name + " got on&l" + monName + "!");
                Player.instance.Surf();
                CloseMenu();
                this.gameObject.SetActive(false);
                yield return 0;
            }
            else
            {
                currentMenu = mainwindow;
                UpdateMainMenu();
                yield return Dialogue.instance.text("No SURFing on&l" + monName + " here!");
            }
        }

        if (whatFieldMove == "Softboiled")
        {
            while (selectedOption == selectedMon)
            {
                selectingPokemon = true;
                currentMenu = mainwindow;
                UpdateMainMenu();
                Dialogue.instance.fastText = true;
                Dialogue.instance.keepTextOnScreen = true;
                Dialogue.instance.needButtonPress = false;
                StartCoroutine(Dialogue.instance.text("Use item on which&l#MON?"));

                while (selectingPokemon)
                {
                    yield return new WaitForSeconds(0.01f);
                    if (InputManager.Pressed(Button.B)) yield return 0;
                    if (InputManager.Pressed(Button.A)) break;
                }

                selectingPokemon = false;
                IPokemon pokemon = PokemonUnity.Game.GameData.Trainer.party[selectedOption];

                if (selectedOption != selectedMon)
                {
                    if (pokemon.HP != pokemon.TotalHP)
                    {
                        int amount = PokemonUnity.Game.GameData.Trainer.party[selectedMon].TotalHP / 5;
                        yield return AnimateOurHealth(-amount, selectedMon);
                        yield return AnimateOurHealth(amount, selectedOption);
                        yield return Dialogue.instance.text(pokemon.Name + "&lrecovered by " + amount + "!");
                    }
                    else
                    {
                        yield return NoEffectText();
                    }
                }
            }
        }

        currentMenu = mainwindow;
        UpdateMainMenu();
    }


    IEnumerator NoEffectText()
    {
        yield return Dialogue.instance.text("It won't have any&leffect.");
    }

    IEnumerator Switch()
    {
        //Swap selected Pokemon.
        IPokemon pokemon = PokemonUnity.Game.GameData.Trainer.party[selectedOption];
        PokemonUnity.Game.GameData.Trainer.party[selectedOption] = PokemonUnity.Game.GameData.Trainer.party[selectedMon];
        PokemonUnity.Game.GameData.Trainer.party[selectedMon] = pokemon;
        yield return null;
    }

    IEnumerator AnimateOurHealth(int amount, int i)
    {
        if (amount + PokemonUnity.Game.GameData.Trainer.party[i].HP < 0) amount = PokemonUnity.Game.GameData.Trainer.party[i].HP;
        if (amount + PokemonUnity.Game.GameData.Trainer.party[i].HP > PokemonUnity.Game.GameData.Trainer.party[i].TotalHP)
            amount = PokemonUnity.Game.GameData.Trainer.party[i].TotalHP - PokemonUnity.Game.GameData.Trainer.party[i].HP;
        int result = amount;
        WaitForSeconds wait = new WaitForSeconds(5 / PokemonUnity.Game.GameData.Trainer.party[i].TotalHP);

        for (int l = 0; l < Mathf.Abs(result); l++)
        {
            yield return wait;

            PokemonUnity.Game.GameData.Trainer.party[i].HP += 1 * Mathf.Clamp(result, -1, 1);

            int pixelCount = Mathf.RoundToInt((float)PokemonUnity.Game.GameData.Trainer.party[i].HP * 48 /
                                              (float)PokemonUnity.Game.GameData.Trainer.party[i].TotalHP);
            healthbars[i].fillAmount = (float)pixelCount / 48;
            UpdateScreen();
        }

        yield return null;
    }

    public bool hasFieldMove(Pokemon pokemon)
    {
        foreach (IMove move in pokemon.moves)
        {
            if (GameData.instance.fieldMoves.Contains(move.id)) 
                return true;
        }

        return false;
    }

    public bool isFieldMove(IMove move)
    {
        if (GameData.instance.fieldMoves.Contains(move.id)) 
            return true;
        
        else return false;
    }

    public void CloseMenu()
    {
        InputManager.instance.DisableForSeconds(Button.B, 0.2f);
        Dialogue.instance.fastText = false;
        Dialogue.instance.Deactivate();
        InputManager.Enable(Button.Start);
        MainMenu.instance.currentmenu = MainMenu.instance.thismenu;
    }
}