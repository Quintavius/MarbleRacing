using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleBrain : MonoBehaviour {
    [Header("Movement Settings")]
    public float acceleration;
    public float maximumSpeed;
    public float slopeSpeed;

    [Header("AI Settings")]
    public Transform[] targets;
    float difficultyMod;
    float massMod;
    float dragMod;
    float angularMod;

    int targetIndex;

    //Shorthands
    [HideInInspector]
    public Rigidbody rb;
    Collider col;
    AISoundManager soundManager;

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
        soundManager = GetComponent<AISoundManager>();
        targetIndex = 0;

        //Generate personality
        difficultyMod = Random.Range(0.001f, 1.2f);
        massMod = Random.Range(0.3f, 3);
        dragMod = Random.Range(0.5f, 2);
        angularMod = Random.Range(0.5f, 2);

        //Apply rigidbody individualisms
        rb.mass *= massMod;
        rb.drag *= dragMod;
        rb.angularDrag *= angularMod;
    }
	
	void Update () {
        if (rb.velocity.magnitude < maximumSpeed * 1.5f)
        {
            //Get target direction
            Vector3 dir = targets[targetIndex].position - transform.position;
            dir = new Vector3(Mathf.Clamp(dir.x, -1, 1), 0, Mathf.Clamp(dir.z, -1, 1));
            //LET THE FORCE FLOW THROUGH YOU
            rb.AddForce(dir * acceleration * difficultyMod * Time.deltaTime);
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

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Waypoint")
        {
            var index = collision.gameObject.GetComponent<WaypointIndex>().index;
            if (index >= targetIndex)
            {
                targetIndex++;
            }
        }
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
