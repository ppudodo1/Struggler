using UnityEngine;

public static class GameManager
{
    //defaultna vrijednost
    private static string levelDiedOn;
    private static int numberOfHearts;
    private static int numberOfShield;
    private static int unlockedLevels;
    private static bool mainMenuEasterEgg;
    public static bool isPaused;

    static GameManager()
    {

        isPaused = false;
    }
    
}
