using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

OLD

public class Lava : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<ItemID>())
        {
            if (other.gameObject.GetComponent<ItemID>().melts_in_lava)
            {
                Destroy(other.gameObject);
            }
        }
        if (other.gameObject.GetComponent<PlayerController>())
        {
            if (!other.gameObject.GetComponent<PlayerController>().currently_active)
            {
                Destroy(other.gameObject);
            }
        }
    }

}
*/