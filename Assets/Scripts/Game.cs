using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Player Player;
    [SerializeField] private GameObject _winWindow;
    [SerializeField] private GameObject _loseWindow;
    [SerializeField] private int _startAtLevel;
    [SerializeField] private int _currentLevel;
    private List<Transform> _listOfLevels = new List<Transform>();
    private int _playerHpAtLevelStart;
    private void Start()
    {
        int n = transform.childCount;
        Transform temp;
        for (int i = 1; i < n; i++)
        {
            temp = transform.Find("Level " + i);
            if(temp != null)
            {
                _listOfLevels.Add(temp);
            }
        }
        _listOfLevels[_startAtLevel-1].gameObject.SetActive(true);
        _currentLevel = _startAtLevel;
        _playerHpAtLevelStart = Player.HP;
    }
    public void Win()
    {
        _winWindow.SetActive(true);
    }
    public void Lose()
    {
        _loseWindow.SetActive(true);
    }
    public void NextLevel()
    {
        _winWindow.SetActive(false);
        Player.ResetPlayer();
        _listOfLevels[_currentLevel - 1].GetComponent<Level>().ResetLevel();
        _listOfLevels[_currentLevel - 1].gameObject.SetActive(false);
        if (_currentLevel < _listOfLevels.Count)
        {
            _currentLevel++;
        }
        else
        {
            _currentLevel = 1;
        }

        _listOfLevels[_currentLevel - 1].gameObject.SetActive(true);
        _playerHpAtLevelStart = Player.HP;
    }
    public void Restart()
    {
        _loseWindow.SetActive(false);
        Player.ResetPlayer(_playerHpAtLevelStart);
        _listOfLevels[_currentLevel-1].GetComponent<Level>().ResetLevel();
    }
}
