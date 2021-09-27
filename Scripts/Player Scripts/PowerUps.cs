using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField]
    private SwordHitScript swordHit_Script;
    private PlayerController playerController_Script;

    private void Awake()
    {
        playerController_Script = gameObject.GetComponent<PlayerController>();
    }
    void TakePower()
    {
        swordHit_Script.damage = swordHit_Script.damage * 3;
        swordHit_Script.canBrokeShield = true;
        StartCoroutine(TakePowerTime());
    }
    void Faster()
    {
        playerController_Script.speed = playerController_Script.speed * 2;
        StartCoroutine(FasterTime());
    }
    IEnumerator TakePowerTime()
    {
        yield return new WaitForSeconds(5);
        swordHit_Script.damage = swordHit_Script.damage / 2;
        swordHit_Script.canBrokeShield = false;
    }
    IEnumerator FasterTime()
    {
        yield return new WaitForSeconds(10);
        playerController_Script.speed = playerController_Script.speed / 2;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Power")
        {
            TakePower();
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "Speed")
        {
            Faster();
            other.gameObject.SetActive(false);
        }
    }
}
