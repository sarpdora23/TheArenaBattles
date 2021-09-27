using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public bool hasShield;
    [SerializeField]
    private float movement_speed;
    private Animator anim;
    private NavMeshAgent navMeshAgent;
    public EnemyStates current_State;
    [SerializeField]
    private float attack_distance;
    private Transform player_Transform;
    private bool canAttack;
    [SerializeField]
    private float attack_delay;
    private float attack_counter;
    private bool control_use;
    private bool can_block;
    public bool can_take_damage;
    private bool firstTimeAttack;
    [SerializeField]
    private float block_timer;
    [SerializeField]
    private GameObject swordHitObject;
    public bool isAlive;
    public void AddAttackCounter()
    {
        attack_counter++;
    }
    public void CanAttackOpen()
    {
        swordHitObject.GetComponent<EnemySwordHitPosition>().canAttack = true;
    }
    public void CanAttackClose()
    {
        swordHitObject.GetComponent<EnemySwordHitPosition>().canAttack = false;
    }
    public void OpenControlUse()
    {
        control_use = true;
    }
    public void EnemySetActiveFalse()
    {
        gameObject.SetActive(false);
    }
    private void Awake()
    {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        anim = gameObject.GetComponent<Animator>();
    }
    private void Start()
    {
        player_Transform = GameObject.FindGameObjectWithTag("Player").transform;
        current_State = EnemyStates.CHASED;
        canAttack = true;
        control_use = true;
        attack_counter = 0;
        firstTimeAttack = true;
        isAlive = true;
        can_take_damage = true;
        can_block = true;
    }
    private void Update()
    {
        if (player_Transform.gameObject.GetComponent<PlayerController>().isAlive)
        {
            ControlState();
            switch (current_State)
            {
                case EnemyStates.CHASED:
                    Chase();
                    break;
                case EnemyStates.ATTACK:
                    Attack();
                    break;
                case EnemyStates.CONTROL:
                    Control();
                    break;
                case EnemyStates.TAKE_DAMAGE:
                    TakeDamage();
                    break;
                case EnemyStates.DEFENSE:
                    Defense();
                    break;
                case EnemyStates.CHASE_WITH_SHIELD:
                    Chase_With_Shield();
                    break;
                case EnemyStates.DEAD:
                    Dead();
                    break;
            }
        }
        else
        {
            movement_speed = 0;
        }
       
    }
    void ControlState()
    {
        if (isAlive)
        {
            if (!hasShield)
            {
                if (Vector3.Distance(player_Transform.position, gameObject.transform.position) > attack_distance)
                {
                    current_State = EnemyStates.CHASED;
                }
                else
                {
                    if (control_use)
                    {
                        current_State = EnemyStates.CONTROL;
                        control_use = false;
                        Debug.Log("Current state: " + current_State);
                    }

                }
            }
            else
            {
                if (Vector3.Distance(player_Transform.position, gameObject.transform.position) > attack_distance)
                {
                    if (!gameObject.GetComponent<EnemyHealth>().isDefense && canAttack)
                    {
                        current_State = EnemyStates.CHASED;
                    }
                    
                }
                else
                {
                    if (control_use)
                    {
                        current_State = EnemyStates.CONTROL;
                        control_use = false;
                        Debug.Log("Current state: " + current_State);
                    }
                }
            }
        }
        else
        {
            navMeshAgent.speed = 0;
            current_State = EnemyStates.DEAD;
            anim.Play("Dead");
        }
        
    }
    void Control()
    {
        navMeshAgent.speed = 0;
        if (canAttack)
        {
            canAttack = false;
            anim.SetBool("isControl", true);
            firstTimeAttack = false;   
            StartCoroutine(AttackDelay()); 
        }
       
    }
    public void ChangeControlState()
    {
        control_use = true;
        anim.ResetTrigger("Attack");
        Debug.Log("Test?");
    }
    void Chase_With_Shield()
    {
        navMeshAgent.speed = movement_speed / 2;
        //Shield + Walk anim play
        navMeshAgent.SetDestination(player_Transform.position);
    }
    void TakeDamage()
    {
        if (can_take_damage)
        {
            can_take_damage = false;
            anim.Play("TakeDamage");
        }
    }
    void Defense()
    {
        if (can_block)
        {
            navMeshAgent.speed = 0;
            Debug.Log("Test??Defense");
            can_block = false;
            anim.SetBool("DefenseCheck", true);
            gameObject.GetComponent<EnemyHealth>().isDefense = true;
            attack_counter = 0;
            StartCoroutine(DefenseTimer());
        }
    }
    void Attack()
    {
        navMeshAgent.speed = 0;
        anim.SetTrigger("Attack");
    }
    void Chase()
    {
        navMeshAgent.speed = movement_speed;
        anim.SetBool("isControl", false);
        navMeshAgent.SetDestination(player_Transform.position);
    }
    void Dead()
    {

    }
    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attack_delay);
        if (hasShield)
        {
            if (attack_counter < 3)
            {
                Debug.Log("Test Control Attack");
                current_State = EnemyStates.ATTACK;
            }
            else
            {
                Debug.Log("Attack Counter: " + attack_counter);
                current_State = EnemyStates.DEFENSE;
            }
        }
        else
        {
            Debug.Log("Test Control Attack");
            current_State = EnemyStates.ATTACK;
        }
        
        Debug.Log("Current state: " + current_State);
        canAttack = true;
    }
    IEnumerator DefenseTimer()
    {
        navMeshAgent.speed = 0;
        yield return new WaitForSeconds(block_timer);
        anim.SetBool("DefenseCheck", false);
        control_use = true;
        anim.SetTrigger("DefenseFinished");
        gameObject.GetComponent<EnemyHealth>().isDefense = false;
        current_State = EnemyStates.CONTROL;
        can_block = true;
        navMeshAgent.speed = movement_speed;
    }
    public void OpenHitCollision()
    {
        swordHitObject.SetActive(true);
    }
    public void CloseHitCollision()
    {
        swordHitObject.SetActive(false);
    }
    public enum EnemyStates
    {
        CHASED,
        ATTACK,
        CONTROL,
        TAKE_DAMAGE,
        DEFENSE,
        CHASE_WITH_SHIELD,
        DEAD
    }
}
