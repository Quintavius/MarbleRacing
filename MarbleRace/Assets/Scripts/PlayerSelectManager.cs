using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerSelectManager : MonoBehaviour {
	public Transform p1;
	MarbleSelector p1sel;
	MarbleSkin p1skin;
	public Transform p2;
	MarbleSelector p2sel;
	MarbleSkin p2skin;
	public Transform p3;
	MarbleSelector p3sel;
	MarbleSkin p3skin;
	public Transform p4;
	MarbleSelector p4sel;
	MarbleSkin p4skin;
	public Text readyText;
	//These are to check if the axis is in use
	bool p1x;
	bool p2x;
	bool p3x;
	bool p4x;
	void Start () {
		//Reset active players
		LevelSettings.numberOfPlayers = 0;
		LevelSettings.player1active = false;
		LevelSettings.player2active = false;
		LevelSettings.player3active = false;
		LevelSettings.player4active = false;

		//Initialize
		p1sel = p1.GetComponent<MarbleSelector>();
		p2sel = p2.GetComponent<MarbleSelector>();
		p3sel = p3.GetComponent<MarbleSelector>();
		p4sel = p4.GetComponent<MarbleSelector>();

		p1skin = p1.GetComponent<MarbleSkin>();
		p2skin = p2.GetComponent<MarbleSkin>();
		p3skin = p3.GetComponent<MarbleSkin>();
		p4skin = p4.GetComponent<MarbleSkin>();

		//Set default skins
		p1skin.SetSkin(LevelSettings.player1Skin);
		p2skin.SetSkin(LevelSettings.player2Skin);
		p3skin.SetSkin(LevelSettings.player3Skin);
		p4skin.SetSkin(LevelSettings.player4Skin);
	}
	
	void Update () {
		//Back to main menu
		if (LevelSettings.numberOfPlayers == 0 && Input.GetButtonDown("Cancel")) {SceneManager.LoadScene("MainMenu");}

		CheckInput();

		//Handle ready text
		if (LevelSettings.numberOfPlayers != 0) {CheckIfReady();} else {readyText.enabled = false;}

		//Start game if ready
		if (Input.GetButtonDown("Pause") && readyText.enabled){
			//Save names and let's go
			LevelSettings.player1Name = p1.gameObject.name;
			LevelSettings.player2Name = p2.gameObject.name;
			LevelSettings.player3Name = p3.gameObject.name;
			LevelSettings.player4Name = p4.gameObject.name;
			SceneManager.LoadScene("LevelSelect");
		}
	}

	void CheckInput(){
		//Player 1
		if (Input.GetButtonDown("Submit_Player1") && p1sel.selected) {p1sel.SetReady();}
		else if (Input.GetButtonDown("Cancel_Player1") && p1sel.ready) {p1sel.SetNotReady();}
		else if (Input.GetButtonDown("Submit_Player1") && !p1sel.selected) {p1sel.Select(); LevelSettings.numberOfPlayers++; LevelSettings.player1active = true;}
		else if (Input.GetButtonDown("Cancel_Player1") && p1sel.selected) {p1sel.Deselect(); LevelSettings.numberOfPlayers--; LevelSettings.player1active = false;}
		//Player 2
		if (Input.GetButtonDown("Submit_Player2") && p2sel.selected) {p2sel.SetReady();}
		else if (Input.GetButtonDown("Cancel_Player2") && p2sel.ready) {p2sel.SetNotReady();}
		else if (Input.GetButtonDown("Submit_Player2") && !p2sel.selected) {p2sel.Select(); LevelSettings.numberOfPlayers++; LevelSettings.player2active = true;}
		else if (Input.GetButtonDown("Cancel_Player2") && p2sel.selected) {p2sel.Deselect(); LevelSettings.numberOfPlayers--; LevelSettings.player2active = false;}
		//Player 3
		if (Input.GetButtonDown("Submit_Player3") && p3sel.selected) {p3sel.SetReady();}
		else if (Input.GetButtonDown("Cancel_Player3") && p3sel.ready) {p3sel.SetNotReady();}
		else if (Input.GetButtonDown("Submit_Player3") && !p3sel.selected) {p3sel.Select(); LevelSettings.numberOfPlayers++; LevelSettings.player3active = true;}
		else if (Input.GetButtonDown("Cancel_Player3") && p3sel.selected) {p3sel.Deselect(); LevelSettings.numberOfPlayers--; LevelSettings.player3active = false;}
		//Player 4
		if (Input.GetButtonDown("Submit_Player4") && p4sel.selected) {p4sel.SetReady();}
		else if (Input.GetButtonDown("Cancel_Player4") && p4sel.ready) {p4sel.SetNotReady();}
		else if (Input.GetButtonDown("Submit_Player4") && !p4sel.selected) {p4sel.Select(); LevelSettings.numberOfPlayers++; LevelSettings.player4active = true;}
		else if (Input.GetButtonDown("Cancel_Player4") && p4sel.selected) {p4sel.Deselect(); LevelSettings.numberOfPlayers--; LevelSettings.player4active = false;}
	
		//Change skins
		//Player 1
		if (p1sel.selected && !p1sel.ready){
			if (Input.GetAxis("Horizontal_Player1") > 0.5f && !p1x) {p1skin.SelectSkin(Marble.Selection.Up, 1); p1x = true;}
			else if (Input.GetAxis("Horizontal_Player1") < -0.5f && !p1x) {p1skin.SelectSkin(Marble.Selection.Down, 1); p1x = true;}
			else if (Input.GetAxis("Horizontal_Player1") == 0) {p1x = false;}}
		//Player 2
		if (p2sel.selected && !p2sel.ready){
			if (Input.GetAxis("Horizontal_Player2") > 0.5f && !p2x) {p2skin.SelectSkin(Marble.Selection.Up, 2); p2x = true;}
			else if (Input.GetAxis("Horizontal_Player2") < -0.5f && !p2x) {p2skin.SelectSkin(Marble.Selection.Down, 2); p2x = true;}
			else if (Input.GetAxis("Horizontal_Player2") == 0) {p2x = false;}}
		//Player 3
		if (p3sel.selected && !p3sel.ready){
			if (Input.GetAxis("Horizontal_Player3") > 0.5f && !p3x) {p3skin.SelectSkin(Marble.Selection.Up, 3); p3x = true;}
			else if (Input.GetAxis("Horizontal_Player3") < -0.5f && !p3x) {p3skin.SelectSkin(Marble.Selection.Down, 3); p3x = true;}
			else if (Input.GetAxis("Horizontal_Player3") == 0) {p3x = false;}}
		//Player 4
		if (p4sel.selected && !p4sel.ready){
			if (Input.GetAxis("Horizontal_Player4") > 0.5f && !p4x) {p4skin.SelectSkin(Marble.Selection.Up, 4); p4x = true;}
			else if (Input.GetAxis("Horizontal_Player4") < -0.5f && !p4x) {p4skin.SelectSkin(Marble.Selection.Down, 4); p4x = true;}
			else if (Input.GetAxis("Horizontal_Player4") == 0) {p4x = false;}}
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
