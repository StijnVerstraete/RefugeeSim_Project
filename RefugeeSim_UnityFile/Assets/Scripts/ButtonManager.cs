using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private GameObject[] _eventBehaviours;
    private EventBehaviour _activeEvent;
    private ResourceManager _resourceManager;

    void Start()
    {
        _eventBehaviours = GameObject.FindGameObjectsWithTag("event");
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject behaviour in _eventBehaviours)
        {
            if (behaviour.GetComponent<EventBehaviour>().EventActive)
            {
                _activeEvent = behaviour.GetComponent<EventBehaviour>();
            }
        }
    }
    public void Option1Click()
    {
        _activeEvent.Option1Click();
    }
    public void Option2Click()
    {
        _activeEvent.Option2Click();
    }
    public void ExitClick()
    {
        _resourceManager.EndDay();
        _activeEvent.ExitClick();
    }
}
