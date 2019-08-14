using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventBehaviour : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] TextAsset _text;
    [SerializeField] private string[] _cutText;

    [Header("UI Elements")]
    [SerializeField] private GameObject _eventPanel;
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _option1Text;
    [SerializeField] private TextMeshProUGUI _option2Text;
    [SerializeField] private Button _option1;
    [SerializeField] private Button _option2;
    [SerializeField] private Button _exitButton;

    [Header("Option1Gains")]
    [SerializeField] private int _followersToGain1;
    [SerializeField] private int _goldToGain1;
    [SerializeField] private int _foodToGain1;
    [SerializeField] private int _moraleToGain1;

    [Header("Option2Gains")]
    [SerializeField] private int _followersToGain2;
    [SerializeField] private int _goldToGain2;
    [SerializeField] private int _foodToGain2;
    [SerializeField] private int _moraleToGain2;

    [Header("Misc")]
    public bool EventActive = true;
    [SerializeField] private PlayerController _player;
    [SerializeField] private ResourceManager _resourceManager;

    private string _option1effects;
    private string _option2effects;

    void Start()
    {
        _cutText = _text.text.Split('\n');


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {


            AdjustForMorale();

            CreateEffectStrings();

            _eventPanel.SetActive(true);
            _option1.gameObject.SetActive(true);
            _option2.gameObject.SetActive(true);

            _titleText.text = _cutText[0];
            _descriptionText.text = _cutText[1];
 
            _option1Text.text = _option1effects;
            _option2Text.text = _option2effects;

            EventActive = true;

            _player.EventActive = true;

            //check gold cost
            if (_goldToGain1 + _resourceManager.GoldCount < 0)
                _option1.interactable = false;
            if (_goldToGain2 + _resourceManager.GoldCount < 0)
                _option1.interactable = false;

            
        }

    }
    public void Option1Click()
    {
        if (EventActive)
        {
            _option1.gameObject.SetActive(false);
            _option2.gameObject.SetActive(false);
            _exitButton.gameObject.SetActive(true);
            _descriptionText.text = _cutText[4];

            ChangeResources(1);
        }
    }
    public void Option2Click()
    {
        if (EventActive)
        {
            _option1.gameObject.SetActive(false);
            _option2.gameObject.SetActive(false);
            _exitButton.gameObject.SetActive(true);
            _descriptionText.text = _cutText[5];

            ChangeResources(2);
        }
    }
    public void ExitClick()
    {
        if (EventActive)
        {
            _eventPanel.SetActive(false);
            _exitButton.gameObject.SetActive(false);
            EventActive = false;
            _player.EventActive = false;
        }
    }
    private void ChangeResources(int option)
    {
        if (option == 1)
        {
                _resourceManager.FollowerCount += _followersToGain1;
                _resourceManager.GoldCount += _goldToGain1;
                _resourceManager.FoodCount += _foodToGain1;
                _resourceManager.Morale += _moraleToGain1;
        }
        if (option == 2)
        {
                _resourceManager.FollowerCount += _followersToGain2;
                _resourceManager.GoldCount += _goldToGain2;
                _resourceManager.FoodCount += _foodToGain2;
                _resourceManager.Morale += _moraleToGain2;
        }
    }
    private void AdjustForMorale()
    {
        int[] resources = new int[6] { _followersToGain1, _followersToGain2, _foodToGain1, _foodToGain2, _goldToGain1, _goldToGain2 };
        if (_resourceManager.Morale == 0)
        {
            for (int i = 0; i < resources.Length; i++)
            {
                if (resources[i] > 0)
                {
                    resources[i] = resources[i] / 2;
                }
                else if (resources[i] < 0)
                {
                    resources[i] = resources[i] * 2;
                }
            }
        }
        if (_resourceManager.Morale == 2)
        {
            for (int i = 0; i < resources.Length; i++)
            {
                if (resources[i] > 0)
                {
                    resources[i] = resources[i] * 2;
                }
                else if(resources[i] < 0)
                {
                    resources[i] = resources[i] / 2;
                }
            }
        }
        _followersToGain1 = resources[0];
        _followersToGain2 = resources[1];
        _foodToGain1 = resources[2];
        _foodToGain2 = resources[3];
        _goldToGain1 = resources[4];
        _goldToGain2 = resources[5];
    }
    private void CreateEffectStrings()
    {
        string follower1 = "", follower2 = "", food1 = "", food2 = "", gold1 = "", gold2 = "", morale1 = "", morale2 = "";


        if (_followersToGain1 != 0)
            follower1 = _followersToGain1 + " <sprite=0>";
        if (_followersToGain2 != 0)
            follower2 = _followersToGain2 + " <sprite=0>";

        if (_foodToGain1 != 0)
            food1 = _foodToGain1 + " <sprite=1>";
        if (_foodToGain2 != 0)
            food2 = _foodToGain2 + " <sprite=1>";

        if (_goldToGain1 != 0)
            gold1 = _goldToGain1 + " <sprite=2>";
        if (_goldToGain2 != 0)
            gold2 = _goldToGain2 + " <sprite=2>";

        if (_moraleToGain1 == -1)
            morale1 = "<sprite=4>";
        if (_moraleToGain1 == +1)
            morale1 = "<sprite=3>";

        if (_moraleToGain2 == -1)
            morale2 = "<sprite=4>";
        if (_moraleToGain2 == .1)
            morale2 = "<sprite=3>";

        _option1effects = _cutText[2] + "\t (" + follower1 + food1 + gold1 + morale1 + ")";
        _option2effects = _cutText[3] + "\t (" + follower2 +  food2 + gold2 + morale2 + ")";
    }
}