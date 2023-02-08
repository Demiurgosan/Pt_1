using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UINextLevel : MonoBehaviour
{
    [SerializeField] private Game _game;
    public void OnButtonClick()
    {
        _game.NextLevel();
    }
}
