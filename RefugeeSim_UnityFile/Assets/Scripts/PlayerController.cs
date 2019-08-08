using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _charCTRL;
    [SerializeField] private float _speed;

    public bool EventActive;
    void Start()
    {
        _charCTRL = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!EventActive && Input.GetKey(KeyCode.Mouse0))
        {
            _charCTRL.Move(Vector3.forward * _speed * Time.fixedDeltaTime);
        }
    }
}
