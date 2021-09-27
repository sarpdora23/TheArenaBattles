using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    private SwordScript sword_Script;
    private ShieldScript shield_Script;
    private BowScript bow_Script;
    [SerializeField]
    private GameObject bow_Object;
    [SerializeField]
    private GameObject shield_Object;
    [SerializeField]
    private GameObject sword_Object;
    private int itemNo;

    private void Awake()
    {
        sword_Script = gameObject.GetComponent<SwordScript>();
        shield_Script = gameObject.GetComponent<ShieldScript>();
        bow_Script = gameObject.GetComponent<BowScript>();
    }
    void Start()
    {
        SwordAndShield();
    }

    // Update is called once per frame
    void Update()
    {
        ItemSwitch();
       // TakeItem();
    }
    void ItemSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwordAndShield();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Bow();
        }
    }
    void SwordAndShield()
    {
        sword_Script.canAttack = true;
        shield_Script.canDefense = true;
        shield_Script.canUseShield = true;
        bow_Script.canUseArrow = false;
        bow_Object.SetActive(false);
        shield_Object.SetActive(true);
        sword_Object.SetActive(true);
    }
    void Bow()
    {
        sword_Script.canAttack = false;
        shield_Script.canDefense = false;
        shield_Script.canUseShield = false;
        bow_Script.canUseArrow = true;
        bow_Object.SetActive(true);
        shield_Object.SetActive(false);
        sword_Object.SetActive(false);
    }
    
}
