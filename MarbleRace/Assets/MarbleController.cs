using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleController : MonoBehaviour {
    public float acceleration;
    public float maximumSpeed;
    public float slopeSpeed;

    Rigidbody rb;
    Collider col;
    Camera cam;
    public GameObject stabilizer;

    float hmove;
    float vmove;

    public float mag;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        col = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
        hmove = Input.GetAxis("Horizontal");
        vmove = Input.GetAxis("Vertical");

        if (hmove != 0 || vmove != 0) {
            if (rb.velocity.magnitude < maximumSpeed * 1.5f)
            {
                //Get force direction
                Vector3 dir = new Vector3(hmove, 0, vmove);
                Vector3 steerDir = stabilizer.transform.TransformDirection(dir);
                //LET THE FORCE FLOW THROUGH YOU
                rb.AddForce(steerDir * acceleration * Time.deltaTime);
            }
        }
	}
    void OnCollisionStay(Collision colInfo)
    {
        mag = rb.velocity.magnitude;
        if (rb.velocity.y < 0 && mag < maximumSpeed)
        {
            rb.AddForce(new Vector3(rb.velocity.x * slopeSpeed, rb.velocity.y * slopeSpeed, rb.velocity.z * slopeSpeed));
        }

    }
}
