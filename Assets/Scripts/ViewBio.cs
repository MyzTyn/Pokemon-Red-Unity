using System.Collections;
using PokemonUnity;
using UnityEngine;
using UnityEngine.UI;


public class ViewBio : MonoBehaviour
{
    public GameObject menu;
    public int pokemonID;
    public bool displayingBio;
    public Image pokemonSprite;
    public CustomText descriptionText, nameText, categoryText, heightText, weightText, dexNoText;
    string pokemonName = "";
    PokemonUnity.Monster.Data.PokemonData entryData;


    public IEnumerator DisplayABio(int whatBio)
    {
        SoundManager.instance.SetMusicLow();
        Pokemons pokemon = (Pokemons)whatBio;
        pokemonName = TempLocalizationXML.instance.GetStr(((Pokemons)whatBio).ToString(TextScripts.Name));

        bool hasSeen = PokemonUnity.Game.GameData.Trainer.hasSeen(pokemon);
        bool hasCaught = PokemonUnity.Game.GameData.Trainer.hasOwned(pokemon);
        
        Debug.Log("Display " + pokemonName + "'s bio. This Pokemon " + (hasSeen && hasCaught
            ?
            "has been seen and caught."
            : hasSeen
                ? "has been seen."
                : "has not been seen or caught."));

        pokemonID = whatBio;
        entryData = Kernal.PokemonData[(Pokemons)pokemonID - 1];
        
        InitText();
        menu.SetActive(true);
        displayingBio = true;

        SoundManager.instance.PlayCry(pokemonID - 1);

        while (SoundManager.instance.isPlayingCry)
        {
            yield return new WaitForSeconds(0.01f);
        }

        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            if (InputManager.Pressed(Button.A)) break;
        }
        
        // ToDo: Split the descriptionText into multiple pages
        //If there's more than one page for the description, go to the next page
        if (entryData.ID.ToString(TextScripts.Description).Length > 1)
        {
            //descriptionText.text = entryData.ID.ToString(TextScripts.Description)[1];
            descriptionText.text = entryData.ID.ToString(TextScripts.Description);

            while (true)
            {
                yield return new WaitForSeconds(0.01f);
                if (InputManager.Pressed(Button.A)) break;
            }
        }

        displayingBio = false;
        menu.SetActive(false);
        SoundManager.instance.SetMusicNormal();
    }

    public void InitText()
    {
        nameText.text = pokemonName;
        // categoryText.text = entryData.category;
        // heightText.text = entryData.heightFeet + " " + (entryData.heightInches < 10 ? "0" : "") +
        //                   entryData.heightInches;
        // weightText.text = string.Format("{0,5:0.0}", entryData.weight);
        // dexNoText.text = (pokemonID > 99 ? "" : pokemonID > 9 ? "0" : "00") + pokemonID.ToString();
        // descriptionText.text = entryData.descriptionText[0];
        
        // ToDo: Where to get the category such as SEED, etc...
        categoryText.text = "???";
        heightText.text = entryData.Height + " ";
        weightText.text = $"{entryData.Weight,5:0.0}";
        dexNoText.text = (pokemonID > 99 ? "" : pokemonID > 9 ? "0" : "00") + pokemonID;
        descriptionText.text = entryData.ID.ToString(TextScripts.Description);
        
        pokemonSprite.sprite = DataLoader.instance.frontMonSprites[pokemonID - 1];
    }
}