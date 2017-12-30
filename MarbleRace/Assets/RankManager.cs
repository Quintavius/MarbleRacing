using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankManager : MonoBehaviour {
	MarbleRank[] players;
	// Use this for initialization
	void Start () {
		players = FindObjectsOfType<MarbleRank>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
