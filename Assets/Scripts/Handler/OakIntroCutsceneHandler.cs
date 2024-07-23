﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OakIntroCutsceneHandler : MonoBehaviour
{
    public GameObject rednamemenu, garynamemenu, nameselectionmenu, currentmenu;
    public GameObject oak, gary, red;
    public GameCursor cursor;
    public Animator tutanim;
    public int selectedOption;
    public GameObject[] allmenus;
    public GameObject overworld, white;
    public bool givingRedAName, givingGaryAName;
    public NameSelection names;
    public Image playerNameMenuImage, rivalNameMenuImage;
    public Sprite redPlayerMenu, bluePlayerMenu;
    public static OakIntroCutsceneHandler instance;


    public void Start()
    {
        Init();
    }

    // Use this for initialization
    public void InitVersion()
    {
        switch (GameData.instance.version)
        {
            case Version.Red:
                playerNameMenuImage.sprite = redPlayerMenu;
                rivalNameMenuImage.sprite = bluePlayerMenu;
                break;
            case Version.Blue:
                playerNameMenuImage.sprite = bluePlayerMenu;
                rivalNameMenuImage.sprite = redPlayerMenu;
                break;
        }
    }

    public void Init()
    {
        GameData.instance.atTitleScreen = true;
        tutanim.SetTrigger("reset");
        cursor.SetActive(false);
        MathE.Clamp(ref selectedOption, 0, 3);
        cursor.SetPosition(8, 120 - 16 * selectedOption);
    }

    // Update is called once per frame
    void Update()
    {
        if ((currentmenu == rednamemenu || currentmenu == garynamemenu) && currentmenu != nameselectionmenu)
        {
            if (InputManager.Pressed(Button.Down))
            {
                selectedOption++;
                MathE.Clamp(ref selectedOption, 0, 3);
                cursor.SetPosition(8, 120 - 16 * selectedOption);
            }

            if (InputManager.Pressed(Button.Up))
            {
                selectedOption--;
                MathE.Clamp(ref selectedOption, 0, 3);
                cursor.SetPosition(8, 120 - 16 * selectedOption);
            }
        }

        foreach (GameObject menu in allmenus)
        {
            if (menu != currentmenu)
            {
                menu.SetActive(false);
            }
            else
            {
                menu.SetActive(true);
            }
        }

        if (InputManager.Pressed(Button.A))
        {
            if (currentmenu == rednamemenu)
            {
                if (selectedOption == 0)
                {
                    currentmenu = nameselectionmenu;
                    nameselectionmenu.SetActive(true);
                    names.futureName = "";
                }
                else
                {
                    currentmenu = null;
                    switch (VersionManager.instance.version)
                    {
                        case Version.Red:
                            switch (selectedOption)
                            {
                                case 1:
                                    PokemonUnity.Game.GameData.Trainer.name = "RED";
                                    break;
                                case 2:
                                    PokemonUnity.Game.GameData.Trainer.name = "ASH";
                                    break;
                                case 3:
                                    PokemonUnity.Game.GameData.Trainer.name = "JACK";
                                    break;
                            }

                            break;
                        case Version.Blue:
                            switch (selectedOption)
                            {
                                case 1:
                                    PokemonUnity.Game.GameData.Trainer.name = "BLUE";
                                    break;
                                case 2:
                                    PokemonUnity.Game.GameData.Trainer.name = "GARY";
                                    break;
                                case 3:
                                    PokemonUnity.Game.GameData.Trainer.name = "JOHN";
                                    break;
                            }

                            break;
                    }

                    tutanim.SetTrigger("transition");
                    Dialogue.instance.enabled = true;
                    givingRedAName = false;
                    cursor.SetActive(false);
                }
            }

            if (currentmenu == garynamemenu)
            {
                if (selectedOption == 0)
                {
                    currentmenu = nameselectionmenu;
                    nameselectionmenu.SetActive(true);
                    names.futureName = "";
                }
                else
                {
                    currentmenu = null;
                    switch (GameData.instance.version)
                    {
                        case Version.Blue:
                            switch (selectedOption)
                            {
                                case 1:
                                    GameData.instance.rivalName = "RED";
                                    break;
                                case 2:
                                    GameData.instance.rivalName = "ASH";
                                    break;
                                case 3:
                                    GameData.instance.rivalName = "JACK";
                                    break;
                            }

                            break;
                        case Version.Red:
                            switch (selectedOption)
                            {
                                case 1:
                                    GameData.instance.rivalName = "BLUE";
                                    break;
                                case 2:
                                    GameData.instance.rivalName = "GARY";
                                    break;
                                case 3:
                                    GameData.instance.rivalName = "JOHN";
                                    break;
                            }

                            break;
                    }

                    tutanim.SetTrigger("transition");
                    Dialogue.instance.enabled = true;
                    givingRedAName = false;
                    cursor.SetActive(false);
                }
            }
        }
    }

    public IEnumerator FirstOakDialogue()
    {
        Player.instance.menuActive = true;
        Dialogue.instance.deactivated = true;
        currentmenu = null;
        yield return Dialogue.instance.text("Hello there!&lWelcome to the&c\nworld of #MON!");
        yield return Dialogue.instance.text("My name is OAK!&lPeople call me&c\nthe #MON PROF!");
        tutanim.SetTrigger("transition");
    }

    public IEnumerator SecondOakDialogue()
    {
        Dialogue.instance.keepTextOnScreen = true;
        yield return Dialogue.instance.text("This world is&linhabited by&c\ncreatures called&c\n#MON!");
        yield return
            SoundManager.instance
                .PlayCryCoroutine(29); //This was meant to play Nidorino's cry, but it instead plays Nidorina's cry
        yield return Dialogue.instance.text("For some people,&l#MON are&c\npets. Others use&c\nthem for fights.");
        yield return Dialogue.instance.text("Myself...");
        yield return Dialogue.instance.text("I study #MON&las a profession.");
        tutanim.SetTrigger("transition");
    }

    public IEnumerator ThirdOakDialogue()
    {
        yield return Dialogue.instance.text("First, what is&lyour name?");
        tutanim.SetTrigger("transition");
        Dialogue.instance.enabled = false;
        while (!tutanim.GetCurrentAnimatorStateInfo(0).IsName("moveredright")) yield return new WaitForEndOfFrame();
        cursor.SetActive(true);
        currentmenu = rednamemenu;
        selectedOption = 0;
        givingRedAName = true;
    }

    public IEnumerator FourthOakDialogue()
    {
        yield return Dialogue.instance.text("Right! So your&lname is " + PokemonUnity.Game.GameData.Trainer.name + "!");
        tutanim.SetTrigger("transition");
    }

    public IEnumerator FifthOakDialogue()
    {
        yield return
            Dialogue.instance.text("This is my grand-&lson. He's been&c\nyour rival since&c\nyou were a baby.");
        yield return Dialogue.instance.text("...Erm, what is&lhis name again?");
        tutanim.SetTrigger("transition");
        Dialogue.instance.enabled = false;
        while (!tutanim.GetCurrentAnimatorStateInfo(0).IsName("movegaryright")) yield return new WaitForEndOfFrame();
        cursor.SetActive(true);
        currentmenu = garynamemenu;
        selectedOption = 0;
        givingGaryAName = true;
    }

    public IEnumerator SixthOakDialogue()
    {
        yield return Dialogue.instance.text("That's right! I&lremember now! His&c\nname is " +
                                            GameData.instance.rivalName + "!");
        tutanim.SetTrigger("transition");
    }

    public IEnumerator SeventhOakDialogue()
    {
        yield return Dialogue.instance.text(PokemonUnity.Game.GameData.Trainer.name + "!");
        yield return Dialogue.instance.text("Your very own&l#MON legend is&c\nabout to unfold!");
        yield return Dialogue.instance.text("A world of dreams&land adventures&c\nwith #MON&c\nawaits! Let's go!");
        SoundManager.instance.FadeSong();
        tutanim.SetTrigger("transition");
    }

    void GotoOverworld()
    {
        overworld.SetActive(true);
        white.SetActive(false);
        Player.instance.menuActive = false;
        Player.instance.GetComponent<BoxCollider2D>().enabled = true;
        Dialogue.instance.deactivated = false;
        Player.instance.isDisabled = false;
        InputManager.Enable(Button.Start);
        GameData.instance.atTitleScreen = false;
        Player.instance.FadeToCurrentAreaSong();
        this.gameObject.SetActive(false);
    }
}