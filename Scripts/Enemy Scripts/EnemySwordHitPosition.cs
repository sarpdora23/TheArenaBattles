using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordHitPosition : MonoBehaviour
{
    [SerializeField]
    private float radius;
    public LayerMask player_Layer;
    [SerializeField]
    private float damage;
    public bool canAttack;
    [SerializeField]
    private float force;
    [SerializeField]
    private GameObject mainCam; 
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        CheckCollider();
    }
    void CheckCollider()
    {
        Collider[] hits = Physics.OverlapSphere(gameObject.transform.position, radius, player_Layer);
        if (hits.Length > 0)
        {
            if (canAttack)
            {
                canAttack = false;
                Vector3 direction = hits[0].transform.position - gameObject.transform.position;
                Debug.Log(hits[0].name);
                if (!hits[0].gameObject.GetComponent<PlayerController>().isDefense)
                {
                    hits[0].GetComponent<HealthStamina>().TakeDamage(damage);
                }
                hits[0].gameObject.GetComponent<Rigidbody>().AddForce(CalculateVector(direction) * force,ForceMode.Impulse);
                Debug.Log("Force: " + force);
            }
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
