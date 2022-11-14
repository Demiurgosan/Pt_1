using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Player player))
        {
            player.HPup();
            transform.gameObject.SetActive(false);
        }
    }
}
