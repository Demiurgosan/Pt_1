using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private bool _gizmoIsActive = true;
    public GameObject BrickPrefab;
    [SerializeField] private float _brickHeight;
    [SerializeField] private float _brickWidth;
    [SerializeField] private int[] _columnsPeak = new int[5];
    [SerializeField] private int[] _rowsDenied = new int[5];
    private bool[,] _wallStructure;
    private float[] _wallPoints;
    private GameObject[,] _wallElements;

    private void OnDrawGizmos()
    {
        if (!_gizmoIsActive) return;
        Gizmos.color = Color.red;
        for (int i = 0; i < _columnsPeak.Length; i++)//columns
        {
            for (int j = 0; j < _columnsPeak[i]; j++)//rows
            {
                    Gizmos.DrawCube(new Vector3(
                        i * _brickWidth - (_columnsPeak.Length / 2f - _brickWidth / 2), 
                        j + _brickHeight / 2, 
                        transform.position.z) , new Vector3(0.95f, 0.95f, 1));
            }
        }
    }

    private void Start()
    {
        int maxPeak = _columnsPeak.Max();
        GetComponent<BoxCollider>().size = new Vector3(_columnsPeak.Length * _brickWidth, maxPeak * _brickHeight * 2, 1.1f);

        _wallStructure = new bool[_columnsPeak.Length, maxPeak];//recording information about existing bricks
        for (int i = 0; i < _columnsPeak.Length; i++)//columns
        {
            for(int j = 0; j < _columnsPeak[i]; j++)//rows
            {
                _wallStructure[i, j] = true;
            }
        }

        for(int i = 0; i < _rowsDenied.Length; i++)
        {
            if (_rowsDenied[i] != 0)
            {
                for(int j=0; j < _wallStructure.GetUpperBound(0)+1; j++) { _wallStructure[j, _rowsDenied[i]-1] = false; }
            }
        }                                                      //complite recording information

        _wallPoints = new float[_columnsPeak.Length];//points for trigger compare with player.X
        for (int i = 0; i < _columnsPeak.Length; i++)
        {
            _wallPoints[i] = WallXCalculation(i);
        }

        _wallElements = new GameObject[_columnsPeak.Length, maxPeak];//creating brick wall on scene
        for (int i = 0; i < _columnsPeak.Length; i++)//columns
        {
            for (int j = 0; j < _columnsPeak[i]; j++)//rows
            {
                if (_wallStructure[i,j] == true) 
                { 
                    _wallElements[i, j] = Instantiate(BrickPrefab, transform);
                    _wallElements[i, j].transform.position =
                        new Vector3(WallXCalculation(i),
                        j + _brickHeight / 2f, 
                        transform.position.z);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collider)//must be problems if player be more left then first point in WallPoints
    {
        if (collider.TryGetComponent(out Player player))
        {
            int bumpedColumn1 = 0;
            int bumpedColumn2 = 0;
            for(int e = 0; e < _wallPoints.Length; e++)
            {
                if (player.transform.position.x == _wallPoints[e])
                { bumpedColumn1 = e; break; }
                else if (player.transform.position.x < _wallPoints[e + 1])
                { bumpedColumn1 = e; bumpedColumn2 = e + 1; break; }
            }
            int columnBumpedMax = Mathf.Max(_columnsPeak[bumpedColumn1], _columnsPeak[bumpedColumn2]);
            player.BumpedInWall(columnBumpedMax, _rowsDenied);
        }   
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent(out Controls controls))
        {
            controls.IsFall = true;
        }
    }

    //secondary functions
    private float WallXCalculation(int index)
    {
        float resolution = index * _brickWidth - (_columnsPeak.Length / 2f - _brickWidth / 2f);
        return resolution;
    }
}
