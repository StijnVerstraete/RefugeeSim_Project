using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerTargetFollow : MonoBehaviour
{
    [SerializeField] private Transform _followerGoal;
    [SerializeField] private float _speed;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, _followerGoal.position) > 5)
        {
            transform.position = Vector3.Lerp(transform.position, _followerGoal.position, Time.deltaTime * _speed);
        }
    }
}
