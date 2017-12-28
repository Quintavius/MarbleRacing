using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITrackHolder : MonoBehaviour {
    WaypointIndex[] trackPoints;
	// Use this for initialization
	void Awake () {
        trackPoints = GetComponentsInChildren<WaypointIndex>();
        int trackPointSize = trackPoints.Length;
        for (int i = 0; i < trackPointSize; i++){
            trackPoints[i].index = i;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
