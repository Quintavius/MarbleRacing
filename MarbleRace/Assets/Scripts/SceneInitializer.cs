using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    public bool DebugPlayers;
    public bool[] DebugPlayersActive;
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;
    void Start()
    {
        //Debug
        if (DebugPlayers)
        {
            LevelSettings.player1active = DebugPlayersActive[0];
            LevelSettings.player2active = DebugPlayersActive[1];
            LevelSettings.player3active = DebugPlayersActive[2];
            LevelSettings.player4active = DebugPlayersActive[3];
        }
		//Set skins and names
        p4.GetComponent<MarbleSkin>().SetSkin(LevelSettings.player4Skin);
        p4.name = LevelSettings.player4Name;
        p3.GetComponent<MarbleSkin>().SetSkin(LevelSettings.player3Skin);
		p3.name = LevelSettings.player3Name;
        p2.GetComponent<MarbleSkin>().SetSkin(LevelSettings.player2Skin);
        p2.name = LevelSettings.player2Name;
        p1.GetComponent<MarbleSkin>().SetSkin(LevelSettings.player1Skin);
        p1.name = LevelSettings.player1Name;
        //Delete players as necessary, name and skin remainders
        if (!LevelSettings.player4active){ p4.SetActive(false); GameObject.Find("P4Cam").SetActive(false);}
        if (!LevelSettings.player3active){ p3.SetActive(false); GameObject.Find("P3Cam").SetActive(false);}
        if (!LevelSettings.player2active){ p2.SetActive(false); GameObject.Find("P2Cam").SetActive(false);}
        if (!LevelSettings.player1active){ p1.SetActive(false); GameObject.Find("P1Cam").SetActive(false);}
    }

    // Update is called once per frame
    void Update()
    {

    }
}
