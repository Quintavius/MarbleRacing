using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleBrain : MonoBehaviour {
    [Header("Movement Settings")]
    public float acceleration;
    public float maximumSpeed;
    public float slopeSpeed;

    public Transform[] targets;
    int targetIndex;
    //Shorthands
    [HideInInspector]
    public Rigidbody rb;
    Collider col;

    //Variables
    [HideInInspector]
    public float mag;
    [HideInInspector]
    public bool grounded;
    [HideInInspector]
    public bool playDropSound;
    int colCount;

    void Start () {
        //initialize
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        //soundManager = GetComponent<MarbleSoundManager>();
    }
	
	void Update () {
        if (rb.velocity.magnitude < maximumSpeed * 1.5f)
        {
            //Get target direction
            Vector3 dir = targets[0].position - transform.position;
            dir = new Vector3(dir.x, 0, dir.z);
            //LET THE FORCE FLOW THROUGH YOU
            rb.AddForce(dir * acceleration * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (colCount == 0) { grounded = false; } else { grounded = true; }
    }

    private void OnCollisionEnter(Collision collision)
    {
        colCount++;
    }

        void OnCollisionStay(Collision colInfo)
    {
        //Increase speed when rolling
        mag = rb.velocity.magnitude;
        if (rb.velocity.y < 0 && mag < maximumSpeed)
        {
            rb.AddForce(new Vector3(rb.velocity.x * slopeSpeed, rb.velocity.y * slopeSpeed, rb.velocity.z * slopeSpeed));
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        colCount--;
    }
}
