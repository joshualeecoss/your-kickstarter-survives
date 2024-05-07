using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int totalGold;

    public SaveData()
    {
        totalGold = PlayerPrefs.GetInt("totalGold");
    }
}
