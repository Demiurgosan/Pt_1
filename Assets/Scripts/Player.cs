using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Game Game;
    [SerializeField] private GameObject CubePrefab;
    [SerializeField] private float CubeSize = 1;
    public int HP { get; private set; }
    [SerializeField] private int _hpAtStart = 1;
    [SerializeField] private Vector3 _startPosition = new Vector3(0,0,1);
    private List<GameObject> _playerCubes = new List<GameObject>();
    [HideInInspector] public bool IsFall = false;

    private void Start()
    {
        HpUp(_hpAtStart);
    }
    private void Update()
    {
        if(transform.position.y <= 0) IsFall = false;
    }

    public void ResetPlayer()
    {
        transform.position = _startPosition;
        GetComponent<Controls>().enabled = true;
    }

    public void ResetPlayer(int hp)
    {
        ResetPlayer();
        _playerCubes.Clear();
        HpUp(hp);
    }

    //PLAYER COLLISIONS
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out HpStack hpStack))
        {
            HpUp(hpStack.HpValue);
        }

        if (collider.TryGetComponent(out Lava lava))
        {
            transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
            IsFall = true;
            HpDown(1);
        }

        if (collider.TryGetComponent(out Wall wall))
        {
            int bumpedColumnIndex1 = 0;
            int bumpedColumnIndex2 = 0;
            for (int e = 0; e < wall.WallPointsX.Length - 1; e++)
            {
                if (transform.position.x == wall.WallPointsX[e])
                { bumpedColumnIndex1 = e; break; }
                else if (transform.position.x < wall.WallPointsX[e + 1])
                { bumpedColumnIndex1 = e; bumpedColumnIndex2 = e + 1; break; }
            }
            if (transform.position.x >= wall.WallPointsX[wall.WallPointsX.Length - 1])
            {
                bumpedColumnIndex1 = wall.WallPointsX.Length - 1;
            }
            int columnBumpedMax = Mathf.Max(wall.ColumnsPeak[bumpedColumnIndex1], wall.ColumnsPeak[bumpedColumnIndex2]);

            int deniedRowsCount = 0;
            foreach (int e in wall.RowsDenied)
            {
                if (wall.RowsDenied[e] != 0) deniedRowsCount++;
            }

            HpDown(columnBumpedMax - deniedRowsCount);
            transform.position = new Vector3(transform.position.x, columnBumpedMax, transform.position.z);
        }

        if(collider.TryGetComponent(out Finish finish))
        {
            GetComponent<Controls>().enabled = false;
            Game.Win();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent(out Wall wall))
        {
            IsFall = true;
        }
    }

    //SECONDARY FUNCTIONS
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