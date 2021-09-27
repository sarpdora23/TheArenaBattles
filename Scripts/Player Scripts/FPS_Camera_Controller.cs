using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Camera_Controller : MonoBehaviour
{
    //[SerializeField]
    //private Transform look_Root;
    [SerializeField]
    private float sensivity;
    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        RotateCamera();
    }
    void RotateCamera()
    {
        gameObject.transform.Rotate(new Vector3(0,Input.GetAxisRaw("Mouse X") * sensivity, 0));
       // look_Root.Rotate(new Vector3(Input.GetAxisRaw("Mouse Y") * sensivity * -1,0,0));
    }
}
