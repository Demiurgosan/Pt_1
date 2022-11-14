using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public float Speed;
    public float WallDistance;
    private Vector3 _prevMousePos;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - _prevMousePos;
            transform.position = new Vector3
                (transform.position.x + delta.x * Speed, transform.position.y, transform.position.z);
        }

        if (transform.position.x > WallDistance) transform.position = new Vector3
                (WallDistance, transform.position.y, transform.position.z);
        if (transform.position.x < -WallDistance) transform.position = new Vector3
                (-WallDistance, transform.position.y, transform.position.z);

        _prevMousePos = Input.mousePosition;
    }
}
