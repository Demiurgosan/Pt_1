using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject CubePrefab;
    public int HP=1;
    private List<GameObject> _playerCubes = new List<GameObject>();
    [HideInInspector] public bool IsFall = false;
    public float FallingSpeed = 1;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(new Vector3(transform.position.x, 0.5f, transform.position.z), new Vector3(1, 1, 1));
    }

    private void Start()
    {
        InstantiateCubes(HP);
    }

    private void Update()
    {
        if (IsFall)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - FallingSpeed * Time.deltaTime, transform.position.z);
            if(transform.position.y < 0)
            {
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                IsFall = false;
            }
        }
    }

    public void Bumped(int columnBumpedMax, int[] _rowsDenied)
    {
        int deniedRowsCount = 0;
        foreach (int e in _rowsDenied){
            if (_rowsDenied[e] != 0) deniedRowsCount++;}
        HpDown(columnBumpedMax - deniedRowsCount);

        transform.position = new Vector3(transform.position.x, (float)columnBumpedMax, transform.position.z);
    }

    //secondary functions
    public void HpUp(int hpConsume)
    {
        HP += hpConsume;
        InstantiateCubes(hpConsume);
    }

    private void HpDown(int hpSubtrahend)
    {
        HP -= hpSubtrahend;
        DestroyCubes(hpSubtrahend);
    }
    private void InstantiateCubes(int value)
    {
        int cubesCount = _playerCubes.Count;
        for (int i = 0; i < value; i++)
        {
            _playerCubes.Add(Instantiate(CubePrefab, transform));
            _playerCubes[i+cubesCount].transform.position = new Vector3(transform.position.x, i + 0.5f + cubesCount, transform.position.z);
        }
    }

    private void DestroyCubes(int value)
    {
        for (int i = 0; i < value; i++)
        {
            Destroy(_playerCubes[_playerCubes.Count - 1]);
            _playerCubes.RemoveAt(_playerCubes.Count-1);
        }
    }
}
