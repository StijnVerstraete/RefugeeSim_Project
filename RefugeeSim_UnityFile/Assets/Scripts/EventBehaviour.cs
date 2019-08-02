using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventBehaviour : MonoBehaviour
{
    [SerializeField] TextAsset _text;
    [SerializeField] private string[] _cutText;

    //ui elements
    [SerializeField] private GameObject _eventPanel;
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _option1Text;
    [SerializeField] private TextMeshProUGUI _option2Text;

    [SerializeField] private Button _option1;
    [SerializeField] private Button _option2;
    [SerializeField] private Button _exitButton;

    public bool EventActive = true;

    void Start()
    {
        _cutText = _text.text.Split('\n');

        //move to ontriggerenter once player is in
        _titleText.text = _cutText[0];
        _descriptionText.text = _cutText[1];
        _option1Text.text = _cutText[2];
        _option2Text.text = _cutText[3];

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_cutText);
       

    }
    private void OnTriggerEnter(Collider other)
    {

            _eventPanel.SetActive(true);
            _option1.enabled = true;
            _option2.enabled = true;

            EventActive = true;

    }
    public void Option1Click()
    {
        if (EventActive)
        {
            _option1.gameObject.SetActive(false);
            _option2.gameObject.SetActive(false);
            _exitButton.gameObject.SetActive(true);
            _descriptionText.text = _cutText[4];
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
        }
    }
    public void ExitClick()
    {
        if (EventActive)
        {
            _eventPanel.SetActive(false);
            _exitButton.gameObject.SetActive(false);
            EventActive = false;
        }
    }
}

