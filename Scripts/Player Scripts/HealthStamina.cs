using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthStamina : MonoBehaviour
{
    [SerializeField]
    private Image blood_effect;
    [SerializeField]
    private Image tired_effect;
    public float health;
    public float stamina;
    [SerializeField]
    private float default_health;
    private float default_stamina;
    private bool takeRest_Delay_Check;
    private PlayerAnimController playerAnimController;
    private void Awake()
    {
        playerAnimController = gameObject.GetComponent<PlayerAnimController>();
    }
    private void Start()
    {
        default_health = health;
        default_stamina = stamina;
        takeRest_Delay_Check = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(20);
        }
        TakeRest();
    }
    public void TakeDamage(float damage)
    {
        playerAnimController.sword_n_shield_states = PlayerAnimController.SwordnShieldStates.TAKEDAMAGE;
        health = health - damage;
        if (health <= 0)
        {
            Dead();
        }
        else
        {
            Debug.Log(default_health - health);
           // blood_effect.color = new Color(blood_effect.color.r, blood_effect.color.g, blood_effect.color.b,(default_health - health) / 100);
        }
    }
    public void Tired(float energy)
    {
        stamina = stamina - energy;
        stamina = Mathf.Clamp(stamina, 0, 100);
        if (stamina == 0)
        {
            ZeroStamina();
        }
        tired_effect.color = new Color(tired_effect.color.r, tired_effect.color.g, tired_effect.color.b, (default_stamina - stamina) / 100);
    }
    void TakeRest()
    {
        if (stamina < 100)
        {
            Debug.Log("Test? Take rest");
            if (takeRest_Delay_Check)
            {
                takeRest_Delay_Check = false;
                stamina = stamina + 3;
                stamina = Mathf.Clamp(stamina, 0, 100);
                tired_effect.color = new Color(tired_effect.color.r, tired_effect.color.g, tired_effect.color.b, (default_stamina - stamina) / 100);
                StartCoroutine(RestDelay());
            }
        }
    }
    IEnumerator RestDelay()
    {
        yield return new WaitForSeconds(0.2f);
        takeRest_Delay_Check = true;
    }
    void ZeroStamina()
    {

    }
    void Dead()
    {
        playerAnimController.sword_n_shield_states = PlayerAnimController.SwordnShieldStates.DEATH;
        gameObject.GetComponent<PlayerController>().isAlive = false;
        StartCoroutine(RestartDelay());
    }

    IEnumerator RestartDelay()
    {
        yield return new WaitForSeconds(2.3f);
        LevelManager.levelManager_instance.Lose();
    }
}
