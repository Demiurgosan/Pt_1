using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpStack : MonoBehaviour
{
    [Range(1, 10)]
    public  int HpValue = 1;
    public GameObject HpPrefab;
    private List<GameObject> _cubesList = new List<GameObject>();// do i need <T> GameObject or Transform?

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (int i = 0; i < HpValue; i++)
        {
            Gizmos.DrawCube(new Vector3(transform.position.x, i + 0.5f, transform.position.z), new Vector3(1, 1, 1));
        }
    }

    private void Start()
    {
        for(int i = 0; i < HpValue; i++)
        {
            GameObject element = Instantiate(HpPrefab, transform);
            _cubesList.Add(element);
            _cubesList[i].transform.position = new Vector3(transform.position.x, i + 0.5f, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Player player))
        {
            player.HpUp(HpValue);
            transform.gameObject.SetActive(false);
        }
    }
}
