using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    [SerializeField]
    private float damage;
    public bool isEnemy;
    [SerializeField]
    private float force;
    public GameObject archer;
    private Rigidbody my_Body;
    private void Awake()
    {
        my_Body = gameObject.GetComponent<Rigidbody>();
    }
    void Start()
    {
        Invoke("Deactive", 4);
    }

    void Deactive()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && isEnemy)
        {
            //Vector3 direction = collision.gameObject.transform.position - archer.transform.position;
            //collision.gameObject.GetComponent<Rigidbody>().AddForce(force * direction.normalized, ForceMode.Impulse);
            if (!other.gameObject.GetComponent<PlayerController>().isDefense)
            {
                other.gameObject.GetComponent<HealthStamina>().TakeDamage(damage);
                gameObject.SetActive(false);
            }

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Shield")
        {
            my_Body.isKinematic = true;
            gameObject.transform.parent = collision.gameObject.transform;

            Debug.Log("Arrow Shield");
        }
        if (collision.gameObject.tag == "Enemy" && !isEnemy)
        {
            Vector3 direction = collision.gameObject.transform.position - archer.transform.position;
            //collision.gameObject.GetComponent<EnemyAddForce>().CloseKinematic();
            //collision.gameObject.GetComponent<Rigidbody>().AddForce(force * direction.normalized, ForceMode.Impulse);
            //collision.gameObject.GetComponent<EnemyAddForce>().OpenKinematic();
            if (!collision.gameObject.GetComponent<EnemyHealth>().isDefense)
            {
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamge(damage);
            }
            
        }

        if (collision.gameObject.tag == "Player" && isEnemy)
        {
            //Vector3 direction = collision.gameObject.transform.position - archer.transform.position;
            //collision.gameObject.GetComponent<Rigidbody>().AddForce(force * direction.normalized, ForceMode.Impulse);
            if (!collision.gameObject.GetComponent<PlayerController>().isDefense)
            {
                collision.gameObject.GetComponent<HealthStamina>().TakeDamage(damage);
                gameObject.SetActive(false);
            }
            
        }
       
    }
}
