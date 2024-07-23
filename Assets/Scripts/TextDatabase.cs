﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDatabase : MonoBehaviour
{
    public static TextDatabase instance;

    void Awake()
    {
        instance = this;
    }

    public void GetItem(ItemsEnum item)
    {
        StartCoroutine(GetItemText(item));
    }

    public IEnumerator GetItemText(ItemsEnum item)
    {
        Inventory.instance.AddItem(item, 1);
        yield return Dialogue.instance.text(GameData.instance.playerName + " found &l" + PokemonData.GetItemName(item) +
                                            "!");
    }
}