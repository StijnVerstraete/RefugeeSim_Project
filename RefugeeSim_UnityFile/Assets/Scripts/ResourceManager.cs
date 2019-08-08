using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResourceManager : MonoBehaviour
{
    public int FollowerCount;
    public int GoldCount;
    public int FoodCount;
    public int Morale; //0 - negative, 1 - neutral, 2 - positive

    //ui elements
    [SerializeField] private TextMeshProUGUI _followersText, _goldText, _foodText;
    [SerializeField] private Image _morale;
    [SerializeField] private Sprite _moraleUp, _moraleDown, _moraleNeutral;

    [SerializeField] private GameObject _gameOverPanel;

    private void Awake()
    {
        //read from playerprefs
        /*FollowerCount = PlayerPrefs.GetInt("FollowerCount");
        GoldCount = PlayerPrefs.GetInt("GoldCount");
        FoodCount = PlayerPrefs.GetInt("FoodCount");
        Morale = PlayerPrefs.GetInt("Morale");*/
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
        UpdateUI();

        //restart
        if (FollowerCount == 0)
            _gameOverPanel.SetActive(true);
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene("MainScene");
    }
    private void UpdateUI()
    {
        _followersText.text = FollowerCount.ToString();
        _goldText.text = GoldCount.ToString();
        _foodText.text = FoodCount.ToString();

        if (Morale < 0)
            _morale.sprite = _moraleDown;
        else if (Morale > 0)
            _morale.sprite = _moraleUp;
        else if (Morale == 0)
            _morale.sprite = _moraleNeutral;

    }
    public void EndDay()
    {
        //use food
        FoodCount = FoodCount - FollowerCount;

        //starvation effect
        if (FoodCount <= 0 )
        {
            FoodCount = 0;
            FollowerCount -= FollowerCount / 3;
            Morale -= 1;
        }
        if (FollowerCount <= 1 && FoodCount <= 0)
        {
            FollowerCount = 0;
        }


        //clamp values
        if (Morale > 1)
            Morale = 1;
        if (Morale < -1)
            Morale = -1;     
    }
}
