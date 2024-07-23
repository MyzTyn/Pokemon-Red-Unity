using System.Collections;
using System.Collections.Generic;
using PokemonUnity.Inventory;
using UnityEngine;

public class TextDatabase : MonoBehaviour
{
    public static TextDatabase instance;

    void Awake()
    {
        instance = this;
    }

    public void GetItem(Items item)
    {
        StartCoroutine(GetItemText(item));
    }

    public IEnumerator GetItemText(Items item)
    {
        Inventory.instance.AddItem(item, 1);
        yield return Dialogue.instance.text(PokemonUnity.Game.GameData.Trainer.name + " found &l" + PokemonData.GetItemName(item) +
                                            "!");
    }
}