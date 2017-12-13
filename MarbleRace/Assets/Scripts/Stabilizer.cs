using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stabilizer : MonoBehaviour {
    Transform cam;
	// Use this for initialization
	void Start () {
        cam = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
        
        transform.eulerAngles = new Vector3(0,cam.eulerAngles.y,0);
	}
}
