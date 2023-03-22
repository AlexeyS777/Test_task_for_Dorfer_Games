using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barn : MonoBehaviour
{
    public static event GameManager.Transform_event TradeItems;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            TradeItems?.Invoke(this.transform.parent.transform);
        }
    }
}
