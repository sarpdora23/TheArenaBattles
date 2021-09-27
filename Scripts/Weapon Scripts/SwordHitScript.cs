using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwordHitScript : MonoBehaviour
{
    public LayerMask enemy_Layer;
    [SerializeField]
    private float radius;
    [SerializeField]
    private float force;
    public float damage;
    public bool canBrokeShield;
    private Collider ennemy_collider;
    private void Start()
    {
        canBrokeShield = false;
    }
    void Update()
    {
        CheckColliders();
    }
    void CheckColliders()
    {
      Collider[] hits  = Physics.OverlapSphere(gameObject.transform.position, radius, enemy_Layer);
        if (hits.Length > 0)
        {
            foreach (Collider enemy in hits)
            {
                ennemy_collider = enemy;
                Debug.Log(ennemy_collider.gameObject.name);
                Vector3 direction = enemy.transform.position - gameObject.transform.position;
                enemy.GetComponent<EnemyAddForce>().CloseKinematic();
                enemy.GetComponent<Rigidbody>().AddForce(CalculateVector(direction) * force, ForceMode.Impulse);
                enemy.GetComponent<EnemyAddForce>().OpenKinematic();
                if (enemy.GetComponent<EnemyHealth>().isDefense)
                {
                    if (canBrokeShield)
                    {
                        enemy.GetComponent<EnemyHealth>().TakeDamge(damage);
                    }
                }
                else
                {
                    enemy.GetComponent<EnemyHealth>().TakeDamge(damage);
                }
            }
            gameObject.SetActive(false);
        }
    }
    Vector3 CalculateVector(Vector3 vector)
    {
        Vector3 deneme = vector;
        if (deneme.x > 0)
        {
            deneme.x = 1;
        }
        else if (deneme.x == 0)
        {
            deneme.x = 0;
        }
        else
        {
            deneme.x = -1;
        }
        if (deneme.z > 0)
        {
            deneme.z = 1;
        }
        else if (deneme.z == 0)
        {
            deneme.z = 0;
        }
        else
        {
            deneme.z = -1;
        }
        return deneme;
    }
}
