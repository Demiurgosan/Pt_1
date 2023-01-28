using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Game Game;
    [SerializeField] private GameObject CubePrefab;
    //[SerializeField] private 
    public float CubeSize = 1;
    [SerializeField] private int HP=1;
    private List<GameObject> _playerCubes = new List<GameObject>();
    [HideInInspector] public bool IsFall = false;

    private void Start()
    {
        InstantiateCubes(HP);
    }
    private void Update()
    {
        if(transform.position.y <= 0) IsFall = false;
    }

    public void BumpedInWall(int columnBumpedMax, int[] _rowsDenied)
    {
        int deniedRowsCount = 0;
        foreach (int e in _rowsDenied){
            if (_rowsDenied[e] != 0) deniedRowsCount++;}
        HpDown(columnBumpedMax - deniedRowsCount);

        transform.position = new Vector3(transform.position.x, columnBumpedMax, transform.position.z);
    }

    public void BumpedInLava()
    {
        transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
        IsFall = true;
        HpDown(1);
    }

    public void Finished()
    {
        GetComponent<Controls>().enabled = false;
        Game.Win();
    }

    //secondary functions
    public void HpUp(int hpConsume)
    {
        HP += hpConsume;
        InstantiateCubes(hpConsume);
    }
    private void HpDown(int hpSubtrahend)
    {
        if (HP - hpSubtrahend > 0)
        {
            HP -= hpSubtrahend;
            DestroyCubes(hpSubtrahend);
        }
        else
        {
            GetComponent<Controls>().enabled = false;
            for(int i = 0; i < _playerCubes.Count; i++)
            {
                Destroy(_playerCubes[i]);
            }
            Game.Lose();
        }

    }
    private void InstantiateCubes(int value)
    {
        int cubesCount = _playerCubes.Count;
        float shiftByY = (0.5f + cubesCount) * CubeSize; ;
        for (int i = 0; i < value; i++)
        {
            _playerCubes.Add(Instantiate(CubePrefab, transform));
            _playerCubes[i+cubesCount].transform.position = new Vector3(transform.position.x, i + shiftByY, transform.position.z);
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(new Vector3(transform.position.x, 0.5f, transform.position.z), new Vector3(1, 1, 1));
    }
}