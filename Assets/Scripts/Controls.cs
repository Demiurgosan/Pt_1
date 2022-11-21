using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    [SerializeField] private float _speed;
    public float WallDistance;
    private Vector3 _prevMousePos;
    private float _tergetX;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _tergetX = transform.position.x + (Input.mousePosition - _prevMousePos).x * _speed;
            var x = Mathf.Clamp(transform.position.x, -WallDistance, WallDistance);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
        _prevMousePos = Input.mousePosition;
    }
}
