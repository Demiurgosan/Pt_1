using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int HP=4;
    private int _lenghtofarray = 15;
    private Transform[] PlayerCubes;

    private void Start()
    {
        PlayerCubes = new Transform[_lenghtofarray];
        for (int i = 0; i < _lenghtofarray; i++)
        {
            PlayerCubes[i] = GetComponentInChildren<Transform>().GetChild(i);
        }
    }

    void Update()
    {
        for(int i = 0; i < _lenghtofarray; i++)
        {
            if (HP - 1 >= i) { PlayerCubes[i].gameObject.SetActive(true); }
            if (HP - 1 < i) { PlayerCubes[i].gameObject.SetActive(false); }
        }
    }

    internal void HPup()
    {
        HP++;
    }

    internal void HPdown()
    {
        HP--;
    }
    internal void HPcrush(int damage)
    {
       
    }
}
