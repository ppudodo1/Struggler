using UnityEngine;

public static class GameManager
{
    //defaultna vrijednost
    private static string levelDiedOn;
    private static int numberOfHearts;
    private static int numberOfShield;
    private static int unlockedLevels;
    private static bool mainMenuEasterEgg;

    static GameManager()
    {
        levelDiedOn = "Cutscene";
        numberOfHearts = 3;
        numberOfShield = 1;
        unlockedLevels = 1;
        mainMenuEasterEgg = false;
    }
    public static void SetMainMenuEasterEgg(bool boolean){
        mainMenuEasterEgg = boolean;
    }
    public static bool GetMainMenuEasterEgg(){
        return mainMenuEasterEgg;
    }
    public static void SetLevelDiedOn(string level){
        levelDiedOn = level;
    }

    public static string GetLevelDiedOn()
    {
        return levelDiedOn;
    }

    public static void SetNumberOfHearts(int number){
        numberOfHearts = number;
    }

    public static int GetNumberOfHearts(){
        return numberOfHearts;
    }

    public static void SetNumberOfShield(int number){
    numberOfShield = number;
    }

    public static int GetNumberOfShield(){
        return numberOfShield;
    }

    public static void SetUnlockedLevels(int number){
    unlockedLevels = number;
    }

    public static int GetUnlockedLevels(){
        return unlockedLevels;
    }
}
