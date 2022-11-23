using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private bool GizmoIsActive = true;
    public GameObject BrickPrefab;
    [SerializeField] private float BrickHeight;
    [SerializeField] private float BrickWidth;
    [SerializeField] private int[] ColumnsPeak = new int[5];
    [SerializeField] private int[] RowsDenied = new int[5];
    private bool[,] WallStructure;
    //private int[] WallValues;
    private GameObject[,] WallElements;

    private void OnDrawGizmos()
    {
        if(GizmoIsActive == true) 
        { 
        Gizmos.color = Color.red;
        for (int i = 0; i < ColumnsPeak.Length; i++)//columns
        {
            for (int j = 0; j < ColumnsPeak[i]; j++)//rows
            {
                    Gizmos.DrawCube(new Vector3(
                        i * BrickWidth - (ColumnsPeak.Length / 2f - BrickWidth / 2), 
                        j + BrickHeight / 2, 
                        transform.position.z) , new Vector3(0.95f, 0.95f, 1));
            }
        }
        }
    }

    private void Start()
    {
        int maxPeak = 0;
        foreach (int i in ColumnsPeak) { maxPeak = Math.Max(maxPeak, i); }//compute posible maximum lenght of array's 2nd demention
        GetComponent<BoxCollider>().size = new Vector3(ColumnsPeak.Length * BrickWidth, maxPeak * BrickHeight * 2, 1.1f);

        WallStructure = new bool[ColumnsPeak.Length, maxPeak];//recording information about existing bricks
        for (int i = 0; i < ColumnsPeak.Length; i++)//columns
        {
            for(int j = 0; j < ColumnsPeak[i]; j++)//rows
            {
                WallStructure[i, j] = true;
            }
        }

        for(int i = 0; i < RowsDenied.Length; i++)
        {
            if (RowsDenied[i] != 0)
            {
                for(int j=0; j < WallStructure.GetUpperBound(0)+1; j++) { WallStructure[j, RowsDenied[i]-1] = false; }
            }
        }                                                      //complite recording information

        /*WallValues = new int[ColumnsPeak.Length];//compute sum of existing bricks in columns
        for(int i = 0; i < WallStructure.GetUpperBound(0)+1; i++)
        {
            for(int j = 0; j < WallStructure.GetUpperBound(1)+1; j++)
            {
                if(WallStructure[i, j] == true) { WallValues[i]++; }
            }
        }*/

        WallElements = new GameObject[ColumnsPeak.Length, maxPeak];//creating brick wall on scene
        for (int i = 0; i < ColumnsPeak.Length; i++)//columns
        {
            for (int j = 0; j < ColumnsPeak[i]; j++)//rows
            {
                if (WallStructure[i,j] == true) 
                { 
                    WallElements[i, j] = Instantiate(BrickPrefab, transform);
                    WallElements[i, j].transform.position =
                        new Vector3(i * BrickWidth - (ColumnsPeak.Length / 2f - BrickWidth / 2),
                        j + BrickHeight / 2, 
                        transform.position.z);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Player player))
        {
            
        }
    }
}
