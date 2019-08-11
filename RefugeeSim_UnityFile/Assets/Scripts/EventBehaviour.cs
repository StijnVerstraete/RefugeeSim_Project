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

    void Start()
    {
        _cutText = _text.text.Split('\n');
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _eventPanel.SetActive(true);
            _option1.gameObject.SetActive(true);
            _option2.gameObject.SetActive(true);

            _titleText.text = _cutText[0];
            _descriptionText.text = _cutText[1];
            _option1Text.text = _cutText[2];
            _option2Text.text = _cutText[3];

            EventActive = true;

            _player.EventActive = true;
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

            //change resources
            _resourceManager.FollowerCount += _followersToGain1;
            _resourceManager.GoldCount += _goldToGain1;
            _resourceManager.FoodCount += _foodToGain1;
            _resourceManager.Morale += _moraleToGain1;
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

            //change resources
            _resourceManager.FollowerCount += _followersToGain2;
            _resourceManager.GoldCount += _goldToGain2;
            _resourceManager.FoodCount += _foodToGain2;
            _resourceManager.Morale += _moraleToGain2;
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
}

