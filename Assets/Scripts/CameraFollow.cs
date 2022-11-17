using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3 _rotation;

    void Update()
    {
        transform.position = _offset;
        transform.rotation = Quaternion.Euler(_rotation);
    }
}
