using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    public bool canDefense;
    private Animator anim;
    public bool isDefence;
    private SwordScript sword_Script;
    public bool canUseShield;
    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        sword_Script = gameObject.GetComponent<SwordScript>();
    }
    void Start()
    {
        isDefence = false;
        canDefense = true;
    }

    void Update()
    {
        Defense();
    }
    void Defense()
    {
        if (Input.GetMouseButtonDown(1) && canDefense && canUseShield)
        {
            anim.Play("Shield_Protect");
            sword_Script.canAttack = false;
            
        }
        if (Input.GetMouseButtonUp(1))
        {
            anim.Play("Shield_Idle");
        }
    }
    public void OpenDefense()
    {
        isDefence = true;
    }
    public void CloseDefence()
    {
        isDefence = false;
    }
}
