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
        //����� ��������� �� � ������, �������� ��� � ���������� �������� ����� � �������� �� ��������� �� �
        //������������ ������ � ����� ���� ��� ����������
        //�������� �� �������� ���� �� ����� ������ �������, ��������� �� �����������, ������� ��
        //���������� �������� ������ � �������� ��������� ��������
        //������� ������ �� ������� ������
        //��� ������ �� ����� ��������� ������� ���������� �������� ������
        if (collider.TryGetComponent(out Player player))
        {
            float rimLeft = player.transform.position.x - 0.5f;
            PlayerWallCollision(rimLeft);
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
    float PlayerWallCollision(float rimLeft)
    {
        if (rimLeft == -2.5) { return 1f; }
        if (rimLeft > -2.5f && rimLeft < -1.5f) { return 1.5f; }
        if (rimLeft == -1.5) { return 2f; }
        if (rimLeft > -1.5f && rimLeft < -0.5f) { return 2.5f; }
        if (rimLeft == -0.5) { return 3f; }
        if (rimLeft > -0.5f && rimLeft < 0.5f) { return 3.5f; }
        if (rimLeft == 0.5) { return 4f; }
        if (rimLeft > 0.5f && rimLeft < 1.5f) { return 4.5f; }
        if (rimLeft == 1.5f) { return 5f; }
        return 0f;
    }
}
