using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleBrain : MonoBehaviour {
    [Header("Movement Settings")]
    public float acceleration;
    public float maximumSpeed;
    public float slopeSpeed;

    [Header("AI Settings")]
    public GameObject targetHolder;
    public WaypointIndex[] targets;
    float difficultyMod;
    float massMod;
    float maxSpeedMod;
    float slopeSpeedMod;
    float dragMod;
    float angularMod;

    public int targetIndex;

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

    void Awake()
    {
        targets = targetHolder.GetComponentsInChildren<WaypointIndex>();
        rb = GetComponent<Rigidbody>();
    }

    void Start () {
        //initialize
        col = GetComponent<Collider>();
        soundManager = GetComponent<AISoundManager>();
        targetIndex = 0;

        //Generate personality
        difficultyMod = Random.Range(0.8f, 1f); //accel mod
        massMod = Random.Range(0.9f, 1.1f); //mass
        dragMod = Random.Range(0.9f, 1.1f); //drag
        angularMod = Random.Range(0.9f, 1.1f); //ang drag
        maxSpeedMod = Random.Range(0.8f, 1f); //max speed
        slopeSpeedMod = Random.Range(0.8f, 1f); //slope

        //Apply rigidbody individualisms
        rb.mass *= massMod;
        rb.drag *= dragMod;
        rb.angularDrag *= angularMod;
        maximumSpeed *= maxSpeedMod;
        slopeSpeed *= slopeSpeedMod;
    }

    void Update()
    {
        //Ride like the wind, but only if you haven't finished yet you marble brained moron
        if (targetIndex < targets.Length)
        {
            if (rb.velocity.magnitude < maximumSpeed * 1.5f)
            {
                //Get target direction
                Vector3 dir = targets[targetIndex].transform.position - transform.position;
                dir = new Vector3(Mathf.Clamp(dir.x, -1, 1), 0, Mathf.Clamp(dir.z, -1, 1));
                //LET THE FORCE FLOW THROUGH YOU
                rb.AddForce(dir * acceleration * difficultyMod * Time.deltaTime);
            }
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
                targetIndex = index +1;
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
