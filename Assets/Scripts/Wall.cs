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
    public int[] RowsDenied = new int[5];
    public int[] ColumnsPeak = new int[5];
    public float[] WallPointsX { get; private set; }
    private bool[,] _wallStructure;
    private GameObject[,] _wallElements;

    private void Start()
    {

        int maxPeak = ColumnsPeak.Max();
        GetComponent<BoxCollider>().size = new Vector3(ColumnsPeak.Length * _brickWidth, maxPeak * _brickHeight * 2, 1.1f);
        _wallStructure = new bool[ColumnsPeak.Length, maxPeak];//recording information about existing bricks

        for (int i = 0; i < ColumnsPeak.Length; i++)//columns
        {
            for(int j = 0; j < ColumnsPeak[i]; j++)//rows
            {
                _wallStructure[i, j] = true;
            }
        }

        for(int i = 0; i < RowsDenied.Length; i++)
        {
            if (RowsDenied[i] != 0)
            {
                for(int j=0; j < _wallStructure.GetUpperBound(0)+1; j++) { _wallStructure[j, RowsDenied[i]-1] = false; }
            }
        }                                                      //complite recording information

        WallPointsX = new float[ColumnsPeak.Length];//points for trigger compare with player.X
        for (int i = 0; i < ColumnsPeak.Length; i++)
        {
            WallPointsX[i] = WallXCalculation(i);
        }

        _wallElements = new GameObject[ColumnsPeak.Length, maxPeak];//creating brick wall on scene
        for (int i = 0; i < ColumnsPeak.Length; i++)//columns
        {
            for (int j = 0; j < ColumnsPeak[i]; j++)//rows
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

    //SECONDARY FUNCTIONS
    private float WallXCalculation(int index)
    {
        float resolution = index * _brickWidth - ((ColumnsPeak.Length - _brickWidth) / 2f);
        return resolution;
    }
    private void OnDrawGizmos()
    {
        if (!_gizmoIsActive) return;
        Gizmos.color = Color.red;
        for (int i = 0; i < ColumnsPeak.Length; i++)//columns
        {
            for (int j = 0; j < ColumnsPeak[i]; j++)//rows
            {
                Gizmos.DrawCube(new Vector3(
                    i * _brickWidth - (ColumnsPeak.Length / 2f - _brickWidth / 2),
                    j + _brickHeight / 2,
                    transform.position.z), new Vector3(0.95f, 0.95f, 1));
            }
        }
    }
}
