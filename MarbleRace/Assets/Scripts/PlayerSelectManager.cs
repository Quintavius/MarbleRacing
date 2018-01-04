using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerSelectManager : MonoBehaviour {
	public Transform p1;
	MarbleSelector p1sel;
	public Transform p2;
	MarbleSelector p2sel;
	public Transform p3;
	MarbleSelector p3sel;
	public Transform p4;
	MarbleSelector p4sel;
	public Text readyText;
	void Start () {
		LevelSettings.numberOfPlayers = 0;
		p1sel = p1.GetComponent<MarbleSelector>();
		p2sel = p2.GetComponent<MarbleSelector>();
		p3sel = p3.GetComponent<MarbleSelector>();
		p4sel = p4.GetComponent<MarbleSelector>();
	}
	
	// Update is called once per frame
	void Update () {
		CheckInput();
		if (LevelSettings.numberOfPlayers != 0) {CheckIfReady();} else {readyText.enabled = false;}

		//Start game if ready, this is testing only
		if (Input.GetButtonDown("Pause") && readyText.enabled){
			SceneManager.LoadScene("MultiplayerTestingRange");
		}
	}

	void CheckInput(){
		//Player 1
		if (Input.GetButtonDown("Submit_Player1") && p1sel.selected) {p1sel.SetReady();}
		else if (Input.GetButtonDown("Cancel_Player1") && p1sel.ready) {p1sel.SetNotReady();}
		else if (Input.GetButtonDown("Submit_Player1") && !p1sel.selected) {p1sel.Select(); LevelSettings.numberOfPlayers++; LevelSettings.player1active = true;}
		else if (Input.GetButtonDown("Cancel_Player1") && p1sel.selected) {p1sel.Deselect();	LevelSettings.numberOfPlayers--; LevelSettings.player1active = false;}
		//Player 2
		if (Input.GetButtonDown("Submit_Player2") && p2sel.selected) {p2sel.SetReady();}
		else if (Input.GetButtonDown("Cancel_Player2") && p2sel.ready) {p2sel.SetNotReady();}
		else if (Input.GetButtonDown("Submit_Player2") && !p2sel.selected) {p2sel.Select(); LevelSettings.numberOfPlayers++; LevelSettings.player2active = true;}
		else if (Input.GetButtonDown("Cancel_Player2") && p2sel.selected) {p2sel.Deselect();	LevelSettings.numberOfPlayers--; LevelSettings.player2active = false;}
		//Player 3
		if (Input.GetButtonDown("Submit_Player3") && p3sel.selected) {p3sel.SetReady();}
		else if (Input.GetButtonDown("Cancel_Player3") && p3sel.ready) {p3sel.SetNotReady();}
		else if (Input.GetButtonDown("Submit_Player3") && !p3sel.selected) {p3sel.Select(); LevelSettings.numberOfPlayers++; LevelSettings.player3active = true;}
		else if (Input.GetButtonDown("Cancel_Player3") && p3sel.selected) {p3sel.Deselect();	LevelSettings.numberOfPlayers--; LevelSettings.player3active = false;}
		//Player 4
		if (Input.GetButtonDown("Submit_Player4") && p4sel.selected) {p4sel.SetReady();}
		else if (Input.GetButtonDown("Cancel_Player4") && p4sel.ready) {p4sel.SetNotReady();}
		else if (Input.GetButtonDown("Submit_Player4") && !p4sel.selected) {p4sel.Select(); LevelSettings.numberOfPlayers++; LevelSettings.player4active = true;}
		else if (Input.GetButtonDown("Cancel_Player4") && p4sel.selected) {p4sel.Deselect();	LevelSettings.numberOfPlayers--; LevelSettings.player4active = false;}
	
	}
	void CheckIfReady(){
		if ((p1sel.ready || !p1sel.selected)
		&& (p2sel.ready || !p2sel.selected)
		&& (p3sel.ready || !p3sel.selected)
		&& (p4sel.ready || !p4sel.selected)){
			readyText.enabled = true;
		}else{
			readyText.enabled = false;
		}
	}
}
