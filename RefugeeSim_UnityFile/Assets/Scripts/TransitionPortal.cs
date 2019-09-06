using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionPortal : MonoBehaviour
{
    [SerializeField] private string _nextScene;
    [SerializeField] private Image _transitionPanel;
    [SerializeField] private float _transitionTime;

    float _alpha = 0;
    private bool _transitionStarted;
    private float _timer;

    // Update is called once per frame
    void Update()
    {
        Transition(_transitionTime);

    }
    private void OnTriggerEnter(Collider other)
    {
        _transitionStarted = true;
    }
    private void Transition(float flashtime)
    {
        if (_transitionStarted)
        {
            _alpha += _transitionTime * Time.deltaTime;
            _transitionPanel.color = new Color(_transitionPanel.color.r, _transitionPanel.color.g, _transitionPanel.color.b, _alpha);
            if (_alpha >= 1)
                SceneManager.LoadScene(_nextScene);
            Debug.Log(_alpha);
        }
    }
}
