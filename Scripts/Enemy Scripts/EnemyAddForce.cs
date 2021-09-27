using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAddForce : MonoBehaviour
{
    public void CloseKinematic()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
    public void OpenKinematic()
    {
        StartCoroutine(KinematicTimer());
    }
    IEnumerator KinematicTimer()
    {
        yield return new WaitForSeconds(0.6f);
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
}
