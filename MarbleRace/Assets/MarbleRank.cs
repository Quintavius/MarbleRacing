using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleRank : MonoBehaviour {
    [HideInInspector]
    public int lastWaypoint;
    private void OnTriggerEnter(Collider col){
        if (col.gameObject.tag == "Waypoint"){
            lastWaypoint = col.GetComponent<WaypointIndex>().index;
        }
    }
}
