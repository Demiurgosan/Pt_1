using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private List<Transform> _listOfElements = new List<Transform>();
    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            _listOfElements.Add(transform.GetChild(i));
        }
    }

    public void ResetLevel()
    {
        for(int i = 0; i < _listOfElements.Count; i++)
        {
            _listOfElements[i].gameObject.SetActive(true);
        }
    }
}
