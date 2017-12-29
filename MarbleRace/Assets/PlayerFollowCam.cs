using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowCam : MonoBehaviour {
	public Transform followPlayer;
	Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - followPlayer.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position = followPlayer.transform.position + offset;
	}
}
