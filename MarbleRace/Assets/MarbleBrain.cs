using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleBrain : MonoBehaviour {
    [Header("Movement Settings")]
    public float acceleration;
    public float maximumSpeed;
    public float slopeSpeed;

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
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionExit(Collision collision)
    {
        colCount--;
    }
}
