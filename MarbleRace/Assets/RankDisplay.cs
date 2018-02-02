using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankDisplay : MonoBehaviour {
	public Transform playerToFollow;
	MarbleRank playerRank;
	Text rankText;
	void Start(){
		rankText = GetComponentInChildren<Text>();
		playerRank = playerToFollow.GetComponent<MarbleRank>();
	}
	void Update(){
		rankText.text = playerRank.currentRank.ToString();
	}
	void FixedUpdate () {
		transform.position = playerToFollow.position;
		transform.LookAt(Camera.main.transform.position);
	}
}
