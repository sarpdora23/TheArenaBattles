using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody my_Body;
    public float speed;
    public LayerMask ground_Layer;
    [SerializeField]
    private bool canJump;
    [SerializeField]
    private float jump_Force;
    private float forward_dash_counter = 0, back_dash_counter = 0, right_dash_counter = 0, left_dash_counter = 0;
    private bool canDash;
    private HealthStamina health_stamina_script;
    private float default_speed;
    [SerializeField]
    private GameObject post_Processing;
    [SerializeField]
    private GameObject shield_Trail;
    [SerializeField]
    private GameObject sword_Trail;
    private PlayerAnimController playerAnimController;
    [SerializeField]
    private GameObject sword_object;
    [SerializeField]
    private GameObject shield_object;
    public bool isDefense = false;
    public bool isAlive = true;
    private bool canMove = false;
    private void Start()
    {
        isDefense = false;
        canDash = true;
        canJump = true;
        default_speed = speed;
    }
    private void Awake()
    {
        my_Body = gameObject.GetComponent<Rigidbody>();
        health_stamina_script = gameObject.GetComponent<HealthStamina>();
        playerAnimController = gameObject.GetComponent<PlayerAnimController>();
    }
    void ItemDashEffects()
    {
        shield_Trail.SetActive(true);
        sword_Trail.SetActive(true);
    }
    IEnumerator CloseItemDash()
    {
        yield return new WaitForSeconds(0.23f);
        shield_Trail.SetActive(false);
        sword_Trail.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            IdleControl();
            CheckCurrentItem();
            MovePlayer();
            JumpPlayer();
            Dash();
            Crounch();
            CheckStamina();
            PlayerAttack();
            PlayerDefense();
            MovePlayerDirection();
        }
        
    }
    void MovePlayerDirection()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKey(KeyCode.W))
        {
            playerAnimController.RunForward();
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKey(KeyCode.S))
        {
            playerAnimController.RunBack();
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKey(KeyCode.D))
        {
            playerAnimController.RunRight();
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKey(KeyCode.A))
        {
            playerAnimController.RunLeft();
        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            playerAnimController.StopRun();
        }
    }
    void PlayerDefense()
    {
        if (Input.GetMouseButton(1))
        {
            isDefense = true;
            speed = 0;
            playerAnimController.sword_n_shield_states = PlayerAnimController.SwordnShieldStates.DEFENSE;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isDefense = false;
            playerAnimController.sword_n_shield_states = PlayerAnimController.SwordnShieldStates.IDLE;
        }
    }
    void IdleControl()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (playerAnimController.sword_n_shield_states == PlayerAnimController.SwordnShieldStates.CROUCH)
            {
                playerAnimController.sword_n_shield_states = PlayerAnimController.SwordnShieldStates.CROUCH_WALK;
            }
            else
            {
                playerAnimController.sword_n_shield_states = PlayerAnimController.SwordnShieldStates.RUN;
            }
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            if (playerAnimController.sword_n_shield_states == PlayerAnimController.SwordnShieldStates.CROUCH_WALK)
            {
                playerAnimController.sword_n_shield_states = PlayerAnimController.SwordnShieldStates.CROUCH;
            }
            else
            {
                playerAnimController.sword_n_shield_states = PlayerAnimController.SwordnShieldStates.IDLE;
            }
        }
    }
    void PlayerAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerAnimController.sword_n_shield_states = PlayerAnimController.SwordnShieldStates.ATTACK;
            speed = 0;
            playerAnimController.sword_n_shield_anim.SetInteger("Attack_Num", Random.Range(1, 3));
        }
    }
    void CheckCurrentItem()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerAnimController.player_anim_states = PlayerAnimController.PlayerAnimStates.SWORD_N_SHIELD;
            shield_object.SetActive(true);
            sword_object.SetActive(true);
            Debug.Log("Current Item: " + playerAnimController.player_anim_states);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerAnimController.player_anim_states = PlayerAnimController.PlayerAnimStates.ARROW;
            shield_object.SetActive(false);
            sword_object.SetActive(false);
            Debug.Log("Current Item: " + playerAnimController.player_anim_states);
        }
    }
    void Crounch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {

            //look_Root.localPosition = new Vector3(look_Root.localPosition.x,crounch_height,look_Root.localPosition.z);
            playerAnimController.sword_n_shield_states = PlayerAnimController.SwordnShieldStates.CROUCH;
            speed = speed / 2;
            canDash = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            playerAnimController.sword_n_shield_states = PlayerAnimController.SwordnShieldStates.CROUCH_TO_STAND;
            //look_Root.localPosition = new Vector3(look_Root.localPosition.x, normal_height, look_Root.localPosition.z);
            speed = speed * 2;
            canDash = true;
        }
    }
    void MovePlayer()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        transform.Translate(h * speed * Time.smoothDeltaTime, 0, v * speed * Time.smoothDeltaTime);
    }
    void JumpPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canJump)
            {
                canJump = false;
                playerAnimController.sword_n_shield_states = PlayerAnimController.SwordnShieldStates.JUMP;
                my_Body.velocity = new Vector3(my_Body.velocity.x, jump_Force, my_Body.velocity.z);
            }
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = true;
        }
    }
    void Dash()
    {
        if (canDash)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                forward_dash_counter++;
                StartCoroutine(ForwardDashTimer());
                if (forward_dash_counter == 2)
                {
                    StopCoroutine(ForwardDashTimer());
                    forward_dash_counter = 0;
                    Debug.Log("Forward DASHHH");
                    ItemDashEffects();
                    post_Processing.SetActive(true);
                    StartCoroutine(CloseItemDash());
                    StartCoroutine(DeactivePostProcess());
                    transform.Translate(0, 0, speed);
                    health_stamina_script.Tired(20);
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                back_dash_counter++;
                StartCoroutine(BackDashTimer());
                if (back_dash_counter == 2)
                {
                    StopCoroutine(BackDashTimer());
                    back_dash_counter = 0;
                    Debug.Log("Back DASHHH");
                    ItemDashEffects();
                    post_Processing.SetActive(true);
                    StartCoroutine(CloseItemDash());
                    StartCoroutine(DeactivePostProcess());
                    transform.Translate(0, 0, -speed);
                    health_stamina_script.Tired(20);
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                right_dash_counter++;
                StartCoroutine(RightDashTimer());
                if (right_dash_counter == 2)
                {
                    StopCoroutine(RightDashTimer());
                    right_dash_counter = 0;
                    Debug.Log("Right DASHHH");
                    ItemDashEffects();
                    post_Processing.SetActive(true);
                    StartCoroutine(CloseItemDash());
                    StartCoroutine(DeactivePostProcess());
                    transform.Translate(speed, 0, 0);
                    health_stamina_script.Tired(20);
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                left_dash_counter++;
                StartCoroutine(LeftDashTimer());
                if (left_dash_counter == 2)
                {
                    StopCoroutine(LeftDashTimer());
                    left_dash_counter = 0;
                    Debug.Log("Left DASHHH");
                    ItemDashEffects();
                    post_Processing.SetActive(true);
                    StartCoroutine(CloseItemDash());
                    StartCoroutine(DeactivePostProcess());
                    transform.Translate(-speed, 0, 0);
                    health_stamina_script.Tired(20);
                }
            }
        }
        
    }
    void CheckStamina()
    {
        if (health_stamina_script.stamina >= 20)
        {
            canDash = true;
        }
        else
        {
            canDash = false;
        }
    }
    public void CanMove()
    {
        canMove = true;
    }
    IEnumerator DeactivePostProcess()
    {
        yield return new WaitForSeconds(0.1f);
        post_Processing.SetActive(false);
    }
    IEnumerator ForwardDashTimer()
    {
        yield return new WaitForSeconds(0.2f);
        forward_dash_counter = 0;
    }
    IEnumerator BackDashTimer()
    {
        yield return new WaitForSeconds(0.2f);
        back_dash_counter = 0;
    }
    IEnumerator RightDashTimer()
    {
        yield return new WaitForSeconds(0.2f);
        right_dash_counter = 0;
    }
    IEnumerator LeftDashTimer()
    {
        yield return new WaitForSeconds(0.2f);
        left_dash_counter = 0;
    }
}
