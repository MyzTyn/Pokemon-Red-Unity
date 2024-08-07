﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class MathE
{
    public static void Clamp(ref int var, int min, int max)
    {
        var = (int)Mathf.Clamp((float)var, (float)min, (float)max);
    }

    public static void Wrap(ref int var, int min, int max)
    {
        var = var < min ? max : var > max ? min : var;
    }
}


public class Options : MonoBehaviour
{
    public GameCursor cursor;
    public RectTransform textSpeedArrow, battleAnimationArrow, battleStyleArrow;
    public int selectedOption;
    public static Options instance;


    // Use this for initialization
    private void Awake()
    {
        instance = this;
    }


    public void Init()
    {
        selectedOption = 0;
        UpdateCursorPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Pressed(Button.A))
        {
            if (Title.instance.gameObject.activeSelf)
            {
                if (selectedOption == 3)
                {
                    SoundManager.instance.PlayABSound();
                    InputManager.instance.DisableForSeconds(Button.A, 0.2f);
                    Title.instance.currentMenu =
                        Title.instance.HasData ? Title.instance.datamenu : Title.instance.nodatamenu;
                    this.gameObject.SetActive(false);
                }
            }
            else if (MainMenu.instance.currentmenu == MainMenu.instance.optionsmenu)
            {
                if (selectedOption == 3)
                {
                    SoundManager.instance.PlayABSound();
                    InputManager.Enable(Button.Start);
                    MainMenu.instance.currentmenu = MainMenu.instance.thismenu;
                    this.gameObject.SetActive(false);
                }
            }
        }

        if (InputManager.Pressed(Button.B))
        {
            if (Title.instance.gameObject.activeSelf)
            {
                SoundManager.instance.PlayABSound();
                this.gameObject.SetActive(false);
            }
            else if (MainMenu.instance.currentmenu == MainMenu.instance.optionsmenu)
            {
                SoundManager.instance.PlayABSound();
                InputManager.Enable(Button.Start);
                MainMenu.instance.currentmenu = MainMenu.instance.thismenu;
                this.gameObject.SetActive(false);
            }
        }

        if (InputManager.Pressed(Button.Left))
        {
            if (selectedOption == 0)
            {
                GameState.instance.textChoice--;
                MathE.Clamp(ref GameState.instance.textChoice, 0, 2);
                UpdateCursorPosition();
            }

            if (selectedOption == 1)
            {
                GameState.instance.animationChoice--;
                MathE.Clamp(ref GameState.instance.animationChoice, 0, 1);
                UpdateCursorPosition();
            }

            if (selectedOption == 2)
            {
                GameState.instance.battleChoice--;
                MathE.Clamp(ref GameState.instance.battleChoice, 0, 1);
                UpdateCursorPosition();
            }
        }

        if (InputManager.Pressed(Button.Right))
        {
            if (selectedOption == 0)
            {
                GameState.instance.textChoice++;
                MathE.Clamp(ref GameState.instance.textChoice, 0, 2);
                UpdateCursorPosition();
            }

            if (selectedOption == 1)
            {
                GameState.instance.animationChoice++;
                MathE.Clamp(ref GameState.instance.animationChoice, 0, 1);
                UpdateCursorPosition();
            }

            if (selectedOption == 2)
            {
                GameState.instance.battleChoice++;
                MathE.Clamp(ref GameState.instance.battleChoice, 0, 1);
                UpdateCursorPosition();
            }
        }

        if (InputManager.Pressed(Button.Down))
        {
            selectedOption++;
            MathE.Clamp(ref selectedOption, 0, 3);
            UpdateCursorPosition();
        }

        if (InputManager.Pressed(Button.Up))
        {
            selectedOption--;
            MathE.Clamp(ref selectedOption, 0, 3);
            UpdateCursorPosition();
        }
    }


    void UpdateCursorPosition()
    {
        switch (selectedOption)
        {
            case 0:
                if (GameState.instance.textChoice == 2) cursor.SetPosition(112, 112);
                else if (GameState.instance.textChoice == 1) cursor.SetPosition(56, 112);
                else cursor.SetPosition(8, 112);
                textSpeedArrow.anchoredPosition = cursor.rectTransform.anchoredPosition + new Vector2(4, 4);
                break;
            case 1:
                cursor.SetPosition(8 + (9 * GameState.instance.animationChoice) * 8, 72);
                battleAnimationArrow.anchoredPosition = cursor.rectTransform.anchoredPosition + new Vector2(4, 4);
                break;
            case 2:
                cursor.SetPosition(8 + (9 * GameState.instance.battleChoice) * 8, 32);
                battleStyleArrow.anchoredPosition = cursor.rectTransform.anchoredPosition + new Vector2(4, 4);
                break;
            case 3:
                cursor.SetPosition(8, 8);
                break;
        }
    }
}