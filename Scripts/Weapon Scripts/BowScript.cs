using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowScript : MonoBehaviour
{
    public bool canUseArrow;
    [SerializeField]
    private GameObject arrowObject;
    [SerializeField]
    private float min_Force;
    [SerializeField]
    private float max_Force;
    private float force;
    private float force_counter;
    private bool canShoot;
    [SerializeField]
    private Transform arrow_Transform;
    [SerializeField]
    private Transform mainCamera;
    public float arrow_counter;
    [SerializeField]
    private Animator bow_Animator;
    void Start()
    {
        force_counter = 0;
        force = max_Force;
        canShoot = false;
    }

    // Update is called once per frame
    void Update()
    {
        ArrowStretching();
    }
    void ArrowStretching()
    {
        if (Input.GetMouseButtonDown(1) && canUseArrow)
        {
            StartCoroutine(AddForce());
            canShoot = true;
        }
        if (Input.GetMouseButtonDown(0) && canShoot && arrow_counter >= 1)
        {
            StopCoroutine(AddForce());
            canShoot = false;
            GameObject arrow = GameObject.Instantiate(arrowObject, arrow_Transform.position, gameObject.transform.rotation);
            arrow.GetComponent<ArrowScript>().isEnemy = false;
            arrow.GetComponent<ArrowScript>().archer = gameObject;
            arrow.GetComponent<Rigidbody>().velocity = mainCamera.forward * force;
            arrow_counter--;
            force = 0;
            force_counter = 0;
        }
    }
    IEnumerator AddForce()
    {
        yield return new WaitForSeconds(0.3f);
        force = force + 3;
        force_counter++;
        if (force_counter < 10)
        {
            force  = Mathf.Clamp(force, min_Force, max_Force);
            StartCoroutine(AddForce());
        }
    }
    void Shoot()
    {

    }
}
