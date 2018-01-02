using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitializer : MonoBehaviour {
	public GameObject p1;
	public GameObject p2;
	public GameObject p3;
	public GameObject p4;
	void Awake () {
		LevelSettings.numberOfPlayers = 2;
		Debug.Log(LevelSettings.numberOfPlayers);
		//Delete players as necessary, name and skin remainders
		if (LevelSettings.numberOfPlayers  == 4){
			p4.GetComponentInChildren<MarbleSkin>().SetSkin(LevelSettings.player4Skin);
			p4.GetComponentInChildren<MarbleSkin>().gameObject.name = LevelSettings.player4Name;
			}
		if (LevelSettings.numberOfPlayers  < 4){ 
			p4.SetActive(false); 
			p3.GetComponentInChildren<MarbleSkin>().gameObject.name = LevelSettings.player3Name;
			p3.GetComponentInChildren<MarbleSkin>().SetSkin(LevelSettings.player3Skin);
			}
		if (LevelSettings.numberOfPlayers  < 3){
			p3.SetActive(false); 
			p2.GetComponentInChildren<MarbleSkin>().SetSkin(LevelSettings.player2Skin);
			p2.GetComponentInChildren<MarbleSkin>().gameObject.name = LevelSettings.player2Name;
			}
		if (LevelSettings.numberOfPlayers  < 2){ 
			p2.SetActive(false);
			p1.GetComponentInChildren<MarbleSkin>().SetSkin(LevelSettings.player1Skin);
			p1.GetComponentInChildren<MarbleSkin>().gameObject.name = LevelSettings.player1Name;
			}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
