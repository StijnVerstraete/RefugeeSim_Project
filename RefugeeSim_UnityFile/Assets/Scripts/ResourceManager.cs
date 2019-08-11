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
    [SerializeField] private TextMeshProUGUI _followersText, _goldText, _foodText, _quitText;
    [SerializeField] private Image _morale;
    [SerializeField] private Sprite _moraleUp, _moraleDown, _moraleNeutral;

    [SerializeField] private GameObject _gameOverPanel;

    private int _followersLost = 0, _moraleLost = 0, _foodLost = 0;

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
        if (FollowerCount <= 0)
            _gameOverPanel.SetActive(true);
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene("MainScene");

        CalculateEndDayEffects();
    }
    private void UpdateUI()
    {
        _followersText.text = FollowerCount.ToString();
        _goldText.text = GoldCount.ToString();
        _foodText.text = FoodCount.ToString();

        if (Morale == 0)
            _morale.sprite = _moraleDown;
        else if (Morale == 2)
            _morale.sprite = _moraleUp;
        else if (Morale == 1)
            _morale.sprite = _moraleNeutral;
    }
    private void CalculateEndDayEffects()
    {    
        //use food
        _foodLost = FollowerCount;

        Debug.Log(FoodCount);
        Debug.Log(_foodLost);
        Debug.Log(FollowerCount);
        //starvation effect
        if (FoodCount <= 0)
        {
            FoodCount = 0;
            _followersLost = FollowerCount / 3;
            _moraleLost = 0;
        }
        if (FollowerCount <= 1 && FoodCount <= 0)
        {
            FollowerCount = 0;
        }
        EndDayEffects("EndDay", _followersLost, _foodLost, _moraleLost);
    }
    public void EndDay()
    {
        //loose values
        FollowerCount -= _followersLost;
        FoodCount -= _foodLost;
        Morale -= _moraleLost;

        //clamp values
        if (Morale > 1)
            Morale = 1;
        if (Morale < -1)
            Morale = -1;
    }
    private void EndDayEffects(string text, int followersLost, int foodLost, int moraleLost)
    {
        string followersstring = "";
        string foodstring = "";
        string moralestring = "";
        if (followersLost != 0)
             followersstring = "-" + followersLost + " <sprite=0>";
        if (foodLost != 0)
            foodstring = "-" + foodLost+ " <sprite=1>";
        if (moraleLost != 0)
            moralestring = "<sprite=4>";

        _quitText.text = text + " (" + followersstring + foodstring + moralestring + ")";
    }
}
