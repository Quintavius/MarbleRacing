using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleRank : MonoBehaviour
{
    public int currentRank;
    [HideInInspector]
    public bool ranked;
    [HideInInspector]
    public int lastWaypoint = 0; //Use this for 1st pass sorting
    [HideInInspector]
    public float nextWaypointDistance; //Use this for 2nd pass sorting
    AITrackHolder trackHolder;
    WaypointIndex nextWaypoint;
    RankManager ranks;
    [HideInInspector]
    public bool isPlayer;
    [HideInInspector]
    public bool isFinished;
    private void Start()
    {
        trackHolder = FindObjectOfType<AITrackHolder>();
        ranks = FindObjectOfType<RankManager>();
        isPlayer = (GetComponent<MarbleController>() != null) ? true : false; //Am i attached to a player?
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Waypoint")
        {
            lastWaypoint = col.GetComponent<WaypointIndex>().index;
        }
    }
    private void Update()
    {
        if (!isFinished)
        {
            if (lastWaypoint + 1 < trackHolder.trackPoints.Length) //Haven't reached the end yet
            {
                nextWaypoint = trackHolder.trackPoints[lastWaypoint + 1];
                nextWaypointDistance = Vector3.Distance(transform.position, nextWaypoint.transform.position);
            }
            else
            {
                //GOOOOAAAAAAAAALLLLL
                ranks.winners.Add(this);
                isFinished = true;
            }
        }

    }
}
