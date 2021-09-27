using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedArrow : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.GetChild(0).GetComponentInChildren<BowScript>().arrow_counter += Random.Range(1,6);
            gameObject.SetActive(false);
        }
    }
}
