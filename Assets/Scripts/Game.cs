using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject _winWindow;
    [SerializeField] private GameObject _loseWindow;

    public void Win()
    {
        _winWindow.SetActive(true);
    }

    public void Lose()
    {
        _loseWindow.SetActive(true);
    }
}
