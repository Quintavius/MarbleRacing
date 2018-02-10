using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackGlitchDebug : MonoBehaviour {
	float t;
	MarbleController marbleController;
	Vector3 lastTouched;
	private void Start(){
		marbleController = GetComponent<MarbleController>();
	}
	private void Update(){
		if (marbleController.grounded){
			t = 0;
		}else{
			t += Time.deltaTime;
			Debug.Log("not grounded");
		}

		if (t >= 6){
			GameObject orb = Instantiate(Resources.Load("DebugSphere", typeof(GameObject))) as GameObject;	
			orb.transform.position = lastTouched;	
			this.enabled = false;
		}
	}

	private void OnCollisionExit(Collision other){
		lastTouched = transform.position;
	}
}
