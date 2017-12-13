using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointIndex : MonoBehaviour {
    public int index;

    private void Awake()
    {
        index = int.Parse(gameObject.name);
    }
}
