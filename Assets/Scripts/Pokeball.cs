using System.Collections;
using System.Collections.Generic;
using PokemonUnity.Inventory;
using UnityEngine;

public class Pokeball : MonoBehaviour, InteractableObject
{
    public Items item;

    public IEnumerator Interact()
    {
        Inventory.instance.AddItem(item, 1);
        yield return Dialogue.instance.text(PokemonUnity.Game.GameData.Trainer.name + " found &l" + PokemonData.GetItemName(item) +
                                            "!");
        this.gameObject.SetActive(false); //maybe replace with Destroy
    }
}