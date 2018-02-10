using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankDisplay : MonoBehaviour {
	public Transform playerToFollow;
	MarbleRank playerRank;
	public Transform cameraToLookAt;
	Text rankText;
	void Start(){
		rankText = GetComponentInChildren<Text>();
		playerRank = playerToFollow.GetComponent<MarbleRank>();
	}
	void Update(){
		rankText.text = playerRank.currentRank.ToString();
	}
	void LateUpdate () {
		transform.position = playerToFollow.position;
		transform.LookAt(cameraToLookAt.position);
	}
}
