using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private Transform[,] _table = new Transform[10, 5];
    private int[] _description = new int[5];

    private void Start()
    {
        Initialization();
        Construct(4, 4, 3, 3, 3);
    }
    void Initialization()
    {
        int _counter = 0;
        for(int i = 0; i < 10; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                _table[i, j] = GetComponentInChildren<Transform>().GetChild(_counter);
                _counter++;
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        //вз€ть положение по ’ игрока, сравнить его с значени€ми описани€ стены с которыми он совпадает по ’
        //использовать правый и левый край как переменные
        //сравнить не €вл€етс€ один из краев больше другого, вычисл€ть по наибольшему, вычесть ’ѕ
        //ограничить движение игрока в пределах попавихс€ столбцов
        //подн€ть игрока на уровень стобца
        //при выходе из стены выполнить падение освободить движение игрока
        if (collider.TryGetComponent(out Player player))
        {
            float rimLeft = player.transform.position.x - 0.5f;
            int colLeft, colrRight;
            BumpIn(rimLeft, out colLeft, out colrRight);

            //выдать с какими столбцами пересечение
            //сравнить их и вычесть наибольший

            player.HPdown();
        }
    }

    void Construct(int col1, int col2, int col3, int col4, int col5)
    {
        int[] cols = { col1, col2, col3, col4, col5 };
        for (int i = 0; i < 5; i++)
        {
            _description[i] = cols[i];
            ConstructColomn(cols[i], i);
        }
    }
    void ConstructColomn(int e, int col)
    {
        for (int i = e; i < 10; i++)
        {
            _table[i, col].gameObject.SetActive(false);
        }
    }
    void BumpIn(float rimLeft, out int colLeft, out int colRight)
    {
        colLeft = 0;
        colRight = 0;
        if (rimLeft == -2.5) { colLeft = 1; }
        if (rimLeft > -2.5f && rimLeft < -1.5f) { colLeft = 1; colRight = 2; }
        if (rimLeft == -1.5) { colLeft = 2; }
        if (rimLeft > -1.5f && rimLeft < -0.5f) { colLeft = 2; colRight = 3; }
        if (rimLeft == -0.5) { colLeft = 3; }
        if (rimLeft > -0.5f && rimLeft < 0.5f) { colLeft = 3; colRight = 4; }
        if (rimLeft == 0.5) { colLeft = 4; }
        if (rimLeft > 0.5f && rimLeft < 1.5f) { colLeft = 4; colRight = 5; }
        if (rimLeft == 1.5f) { colLeft = 5; }
    }
}
