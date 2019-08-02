using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int FollowerCount;
    public int GoldCount;
    public int FoodCount;
    public int Morale; //0 - negative, 1 - neutral, 2 - positive

    private void Awake()
    {
        //read from playerprefs
        FollowerCount = PlayerPrefs.GetInt("FollowerCount");
        GoldCount = PlayerPrefs.GetInt("GoldCount");
        FoodCount = PlayerPrefs.GetInt("FoodCount");
        Morale = PlayerPrefs.GetInt("Morale");
    }
    void Update()
    {
        //write to playerprefs
        PlayerPrefs.SetInt("FollowerCount", FollowerCount);
        PlayerPrefs.SetInt("GoldCount", GoldCount);
        PlayerPrefs.SetInt("FoodCount", FoodCount);
        PlayerPrefs.SetInt("Morale", Morale);

        //clamp morale
        if (Morale < 0)
            Morale = 0;
        if (Morale > 2)
            Morale = 2;
    }
}
