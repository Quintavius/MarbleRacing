using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleController : MonoBehaviour {
    public float movementSpeed;
    public float maximumSpeed;

    Rigidbody rb;
    Camera cam;
    //Vector3 steerDir;

    float hmove;
    float vmove;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        hmove = Input.GetAxis("Horizontal");
        vmove = Input.GetAxis("Vertical");

        if (hmove != 0 || vmove != 0) {
            //Get force direction
            Vector3 dir = new Vector3(hmove, 0, vmove);
            Vector3 steerDir = cam.transform.TransformDirection(dir);
            //LET THE FORCE FLOW THROUGH YOU
            rb.AddForce(steerDir * movementSpeed * Time.deltaTime);
        }
	}
}
