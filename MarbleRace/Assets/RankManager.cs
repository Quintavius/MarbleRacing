using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankManager : MonoBehaviour
{
	[HideInInspector]
    public List<MarbleRank> players;
    // Use this for initialization
    void Start()
    {
		//Throw all players in a list
        MarbleRank[] tempList = FindObjectsOfType<MarbleRank>();
		foreach (MarbleRank rank in tempList){
			players.Add(rank);
		}
    }
    void Update()
    {
		players.Sort(delegate(MarbleRank x, MarbleRank y){return x.lastWaypoint.CompareTo(y.lastWaypoint);});
    }
}
