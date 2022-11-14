using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset;
    public Vector3 Rotation;

    void Update()
    {
        transform.position = Offset;
        //transform.rotation = Rotation;
    }
}
