using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stabilizer : MonoBehaviour {
    public Transform cameraToFollow;
	void FixedUpdate () {
        transform.eulerAngles = new Vector3(0,cameraToFollow.eulerAngles.y,0);
	}
}
