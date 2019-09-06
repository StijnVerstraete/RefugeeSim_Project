using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionFadeOut : MonoBehaviour
{
    [SerializeField] private Image _transitionPanel;
    [SerializeField] private float _transitionTime;

    private float _alpha;
    private bool _transitionCompleted = false;

    private void Start()
    {
        _alpha = _transitionPanel.color.a;
    }
    void Update()
    {
        if (!_transitionCompleted)
            FadeOut();
    }
    private void FadeOut()
    {
        _alpha -= _transitionTime * Time.deltaTime;
        _transitionPanel.color = new Color(_transitionPanel.color.r, _transitionPanel.color.g, _transitionPanel.color.b, _alpha);
        if (_alpha <= 0)
            _transitionCompleted = true;
    }
}
