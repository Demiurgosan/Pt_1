using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    [SerializeField] private Player _player;
    public float BorderDistance;
    [HideInInspector] public bool MoveState;
    [SerializeField] private float _sidewaysSpeed;
    [SerializeField] private float _forwardSpeed;
    private Vector3 _prevMousePos;
    private float _tergetX;
    [SerializeField] private float _fallingSpeedMultip = 1;
    private float _fallingTime;

    void Update()
    {
        //movement in sideways
        if (Input.GetMouseButton(0) && !MoveState) { MoveState = true; }
        if (Input.GetMouseButton(0))
        {
            _tergetX = transform.position.x + (Input.mousePosition - _prevMousePos).x * _sidewaysSpeed;
            var x = Mathf.Clamp(_tergetX, -BorderDistance, BorderDistance);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
        _prevMousePos = Input.mousePosition;

        //movement in forward
        if (MoveState)
        {
            transform.position += new Vector3(0, 0, _forwardSpeed * Time.deltaTime);
        }

        //falling down
        if (_player.IsFall)
        {
            _fallingTime += Time.deltaTime;
            var targetY = transform.position.y - _fallingSpeedMultip * _fallingTime;
            transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
            if (transform.position.y <= 0)
            {
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            }
        }
        else _fallingTime = 0;
    }
}
