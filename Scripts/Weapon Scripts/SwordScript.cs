using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    [SerializeField]
    private float damage;
    private Animator anim;
    public LayerMask enemy_Layer;
    [SerializeField]
    private GameObject hit_Position;
    private ShieldScript shield_Script;
    public bool canAttack;

    private void Start()
    {
        canAttack = true;
    }
    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        shield_Script = gameObject.GetComponent<ShieldScript>();
    }

    void Update()
    {
        Attack();
    }
    void Attack()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            anim.Play("Sword_Attack");
            shield_Script.canDefense = false;
        }
    }
    public void HitOpen()
    {
        hit_Position.SetActive(true);
    }
    public void HitClose()
    {
        hit_Position.SetActive(false);
    }
    public void CanProtectOpen()
    {
        shield_Script.canDefense = true;
    }
    public void CanAttackOpen()
    {
        canAttack = true;
    }
}
