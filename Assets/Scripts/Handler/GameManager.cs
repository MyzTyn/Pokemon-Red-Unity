using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // ToDo: Rename those to scenes?
    public Slots slots;
    public PokemonMenu pokemonMenu;
    public Options options;
    public Title title;
    public IntroHandler introHandler;
    public OakIntroCutsceneHandler oakIntroCutsceneHandler;
    public Pokedex pokedex;
    public PC pc;

    private void Start()
    {
        // Run the game at 60 fps
        Application.targetFrameRate = 60;
        
        // ToDo: Remove those instances
        PokemonMenu.instance = pokemonMenu;
        Options.instance = options;
        Title.instance = title;
        OakIntroCutsceneHandler.instance = oakIntroCutsceneHandler;
        Pokedex.instance = pokedex;
        
        GameState.instance.inGame = !GameState.instance.startIntroScene;
        
        if (GameState.instance.startIntroScene)
        {
            BootGame();
        }
    }
    
    /// <summary>
    /// Initialize the game that changes based on the version
    /// </summary>
    private void InitVersion()
    {
        // ToDo: Rename to Version
        slots.Init();
        title.InitVersion();
        introHandler.InitVersion();
        oakIntroCutsceneHandler.InitVersion();
    }
    
    private void BootGame()
    {
        introHandler.gameObject.SetActive(true);
        introHandler.Init();
    }

    private void ResetGame()
    {
        introHandler.ResetGame();
    }
}