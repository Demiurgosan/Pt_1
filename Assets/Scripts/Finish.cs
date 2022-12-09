using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.TryGetComponent(out Player player))
        {
            player.Finished();
        }
    }
}
