using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerBehaviour : MonoBehaviour
{
    [SerializeField] private int _id;

    [SerializeField] private ResourceManager _resourceManager;


    private MeshRenderer _renderer;
    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {

            
        if (_resourceManager.FollowerCount >= _id)
            _renderer.enabled = true;
        else
            _renderer.enabled = false;
    }
}
