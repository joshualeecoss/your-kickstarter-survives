using UnityEngine;

public class LoadDataOnStartup
{
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        SaveData data = SaveSystem.LoadGame();

        if (data != null)
        {
            PlayerPrefs.SetInt("totalGold", data.totalGold);
            Debug.Log(data.totalGold + " gold loaded successfully");
        }
        else
        {
            Debug.Log("No save data to load");
        }
    }
}
