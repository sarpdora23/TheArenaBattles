using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private float health;
    [SerializeField]
    private bool isArcher;
    [SerializeField]
    private GameObject drop_arrow;
    private Animator anim;
    private EnemyController enemy_Controller;
    public bool isDefense;
    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        enemy_Controller = gameObject.GetComponent<EnemyController>();
    }
    public void TakeDamge(float damage)
    {
        if (!isArcher)
        {
            enemy_Controller.can_take_damage = true;
            enemy_Controller.current_State = EnemyController.EnemyStates.TAKE_DAMAGE;
            if (health > 0)
            {
                health -= damage;
            }
            Dead();
        }
        else
        {
            anim.Play("TakeDamage");
            if (health > 0)
            {
                health -= damage;
            }
            Dead();
        }
        
    }
    public void ArcherSetPasive()
    {
        gameObject.SetActive(false);
    }
    void Dead()
    {
        if (health <= 0)
        {
            if (isArcher)
            {
                //GameObject.Instantiate(drop_arrow, transform.position, Quaternion.identity);
                anim.Play("Death");
               LevelManager.levelManager_instance.KilledEnemy();
            }
            else
            {
                enemy_Controller.isAlive = false;
                LevelManager.levelManager_instance.KilledEnemy();
            }
            
        }
    }
    
}
