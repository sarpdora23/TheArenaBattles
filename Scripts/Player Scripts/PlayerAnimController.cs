using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    public Animator sword_n_shield_anim;
    public PlayerAnimStates player_anim_states;
    public SwordnShieldStates sword_n_shield_states;
    [SerializeField]
    private GameObject swordHitPosition;
    //[SerializeField]
    //private Animator bow_anim;
    private void Update()
    {
        if (player_anim_states == PlayerAnimStates.SWORD_N_SHIELD)
        {
            switch (sword_n_shield_states)
            {
                case SwordnShieldStates.IDLE:
                    Idle();
                    break;
                case SwordnShieldStates.RUN:
                    Run();
                    break;
                case SwordnShieldStates.JUMP:
                    Jump();
                    break;
                case SwordnShieldStates.CROUCH:
                    Crouch();
                    break;
                case SwordnShieldStates.TAKEDAMAGE:
                    TakeDamage();
                    break;
                case SwordnShieldStates.ATTACK:
                    Attack();
                    break;
                case SwordnShieldStates.DEFENSE:
                    Defense();
                    break;
                case SwordnShieldStates.DEATH:
                    Death();
                    break;
                case SwordnShieldStates.CROUCH_ATTACK:
                    CrouchAttack();
                    break;
                case SwordnShieldStates.CROUCH_WALK:
                    CrouchWalk();
                    break;
                case SwordnShieldStates.CROUCH_TO_STAND:
                    CrouchToStand();
                    break;
                case SwordnShieldStates.BOW:
                    break;
            }
        }
       
    }
    public void OpenHitBox()
    {
        swordHitPosition.SetActive(true);
    }
    public void CloseHitBox()
    {
        swordHitPosition.SetActive(false);
    }
    public void BackToIdle()
    {
        sword_n_shield_anim.ResetTrigger("Attack");
        sword_n_shield_anim.ResetTrigger("Jump");
        gameObject.GetComponent<PlayerController>().speed = 10;
        if (sword_n_shield_states == SwordnShieldStates.CROUCH_ATTACK)
        {
            sword_n_shield_states = SwordnShieldStates.CROUCH;
        }
        else
        {
            sword_n_shield_states = SwordnShieldStates.IDLE;
        }
        
    }
    public void StandUp()
    {
        sword_n_shield_states = SwordnShieldStates.IDLE;
    }
    void CrouchToStand()
    {
        sword_n_shield_anim.SetBool("Crouch", false);
    }
    void Idle()
    {
        sword_n_shield_anim.SetBool("Run", false);
        sword_n_shield_anim.SetBool("Block", false);
        gameObject.GetComponent<PlayerController>().speed = 10;
    }
    void Defense()
    {
        sword_n_shield_anim.SetBool("Block", true);
    }
    void Run()
    {
        sword_n_shield_anim.SetBool("Run", true);
    }
    public void RunForward()
    {
        sword_n_shield_anim.SetInteger("Run_Dir", 1);
    }
    public void RunBack()
    {
        sword_n_shield_anim.SetInteger("Run_Dir", 2);
    }
    public void RunRight()
    {
        sword_n_shield_anim.SetInteger("Run_Dir", 3);
    }
    public void RunLeft() 
    {
        sword_n_shield_anim.SetInteger("Run_Dir", 4);
    }
    public void StopRun()
    {
        sword_n_shield_anim.SetInteger("Run_Dir", 0);
    }
    void Jump()
    {
        sword_n_shield_anim.SetTrigger("Jump");
    }
    void Crouch()
    {
        sword_n_shield_anim.SetBool("Crouch", true);
    }
    void TakeDamage()
    {
        sword_n_shield_anim.Play(Sword_n_Shield_Anim.Take_Damage);
    }
    void Attack()
    {
        sword_n_shield_anim.SetTrigger("Attack");
        
    }
    void Death()
    {
        sword_n_shield_anim.Play(Sword_n_Shield_Anim.Death);
    }
    void CrouchAttack()
    {
        sword_n_shield_anim.SetTrigger("Attack");
        gameObject.GetComponent<PlayerController>().speed = 0;
    }
    void CrouchWalk()
    {
        sword_n_shield_anim.SetBool("Run", true);
    }
    public enum PlayerAnimStates
    {
        SWORD_N_SHIELD,
        ARROW
    }
    public enum SwordnShieldStates
    {
        IDLE,
        RUN,
        JUMP,
        CROUCH,
        TAKEDAMAGE,
        ATTACK,
        DEFENSE,
        DEATH,
        CROUCH_ATTACK,
        CROUCH_WALK,
        CROUCH_TO_STAND,
        BOW
    }
}
