using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArcherEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private NavMeshAgent navmMeshAgent;
    [SerializeField]
    private float movement_Speed;
    [SerializeField]
    private float attack_Distance;
    [SerializeField]
    private Transform arrowInstiantePosition;
    [SerializeField]
    private GameObject arrow_object;
    [SerializeField]
    private float arrow_force;
    [SerializeField]
    private float shoot_timer;
    private bool canShoot;
    public bool canSee;
    [SerializeField]
    private float max_X, min_X, max_Z, min_Z;
    private bool checkNewPosition;
    private Vector3 newPosition;
    public bool didSee;
    private bool didSeeTimerCheck;
    private ArcherEnemyStates current_state;
    private Animator anim;
    public enum ArcherEnemyStates
    {
        MOVE,
        CHASE,
        SHOOT,
        TAKE_DAMAGE,
        DEAD
    }
    private void Awake()
    {
        navmMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        anim = gameObject.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        current_state = ArcherEnemyStates.MOVE;
        didSee = false;
        canShoot = true;
        canSee = false;
        checkNewPosition = true;
    }

    // Update is called once per frame
    void Update()
    {
        ControllState();
        switch (current_state)
        {
            case ArcherEnemyStates.MOVE:
                MoveAnim();
                break;
            case ArcherEnemyStates.CHASE:
                ChaseAnim();
                break;
            case ArcherEnemyStates.SHOOT:
                ShootAnim();
                break;
            case ArcherEnemyStates.TAKE_DAMAGE:
                break;
            case ArcherEnemyStates.DEAD:
                break;
            default:
                break;
        }
    }
    void ShootAnim()
    {
        anim.SetTrigger("Shoot");
    }
    void MoveAnim()
    {

    }
    void ChaseAnim()
    {

    }
    void ControllState()
    {
        MoveTheEnemy();
        LostPlayer();
    }
    void LostPlayer()
    {
        if (didSee && !canSee)
        {
            if (didSeeTimerCheck)
            {
                didSeeTimerCheck = false;
                StartCoroutine(LostTime());
            }
           
        }
        else
        {
            StopCoroutine(LostTime());
            didSeeTimerCheck = true;
        }
    }
    IEnumerator LostTime()
    {
        yield return new WaitForSeconds(10);
        didSee = false;
        didSeeTimerCheck = true;
    }
    void MoveTheEnemy()
    {
        if (Vector3.Distance(player.transform.position,transform.position) > attack_Distance)
        {
            if (!didSee)
            {
                navmMeshAgent.speed = movement_Speed;
                current_state = ArcherEnemyStates.MOVE;
                navmMeshAgent.SetDestination(GenerateRandomPosition());
                checkNewPosition = false;
            }
            else
            {
                navmMeshAgent.speed = movement_Speed;
                current_state = ArcherEnemyStates.CHASE;
                Debug.Log("I CAN SEEEE");
                navmMeshAgent.SetDestination(player.transform.position);
            }
        }
        else
        {
            if (canSee)
            {
                didSee = true;
                navmMeshAgent.speed = 0;
                Vector3 targetPostition = new Vector3(player.transform.position.x,
                                       this.transform.position.y,
                                       player.transform.position.z);
                this.transform.LookAt(targetPostition);
                if (canShoot)
                {
                    canShoot = false;
                    ShootAnim();
                    StartCoroutine(ShootDelay());
                }
            }
            else
            {
                navmMeshAgent.speed = movement_Speed;
                navmMeshAgent.SetDestination(player.transform.position);
            }
            
        }
    }
    IEnumerator NewPosition()
    {
        yield return new WaitForSeconds(6);
        checkNewPosition = true;
    }
    Vector3 GenerateRandomPosition()
    {
        if (checkNewPosition)
        {
            Debug.Log("Test? GenerateRandomPosition");
            checkNewPosition = false;
            newPosition = new Vector3(Random.Range(min_X, max_X), gameObject.transform.position.y, Random.Range(min_Z, max_Z));
           
            StartCoroutine(NewPosition());
        }
        if (transform.position == newPosition)
        {
            newPosition = new Vector3(Random.Range(min_X, max_X), gameObject.transform.position.y, Random.Range(min_Z, max_Z));
            StartCoroutine(NewPosition());
        }
        return newPosition;
    }
    public void Shoot()
    {
        GameObject arrow = Instantiate(arrow_object, arrowInstiantePosition.position, transform.rotation);
        arrow.GetComponent<ArrowScript>().archer = gameObject;
        arrow.GetComponent<ArrowScript>().isEnemy = true;
        arrow.GetComponent<Rigidbody>().velocity = transform.forward * arrow_force;
    }
    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(shoot_timer);
        canShoot = true;
    }
}
