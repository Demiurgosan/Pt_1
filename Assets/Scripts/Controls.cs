using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public float BorderDistance;
    [HideInInspector] public bool MoveState;
    [SerializeField] private float _sidewaysSpeed;
    [SerializeField] private float _forwardSpeed;
    private Vector3 _prevMousePos;
    private float _tergetX;
    [HideInInspector] public bool IsFall = false;
    public float FallingSpeed = 1;

    void Update()
    {
        if (Input.GetMouseButton(0) && !MoveState) { MoveState = true; }
        if (Input.GetMouseButton(0))
        {
            _tergetX = transform.position.x + (Input.mousePosition - _prevMousePos).x * _sidewaysSpeed;
            var x = Mathf.Clamp(_tergetX, -BorderDistance, BorderDistance);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
        _prevMousePos = Input.mousePosition;

        if (IsFall)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - FallingSpeed * Time.deltaTime, transform.position.z);
            if (transform.position.y < 0)
            {
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                IsFall = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (MoveState)
        {
            transform.position += new Vector3(0, 0, _forwardSpeed);
        }
        
    }
}
