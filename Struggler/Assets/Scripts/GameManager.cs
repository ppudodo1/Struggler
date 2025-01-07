using UnityEngine;

public static class GameManager
{
    //defaultna vrijednost
    private static string levelDiedOn;
    private static int numberOfHearts;
    private static int numberOfShield;

    static GameManager()
    {
        levelDiedOn = "Cutscene";
        numberOfHearts = 3;
        numberOfShield = 1;
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
}
