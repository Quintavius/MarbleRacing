using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    public int DebugPlayerNumber;
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;
    void Awake()
    {
        LevelSettings.numberOfPlayers = DebugPlayerNumber;
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
        if (LevelSettings.numberOfPlayers <= 3){ p4.SetActive(false); GameObject.Find("P4Cam").SetActive(false);}
        if (LevelSettings.numberOfPlayers <= 2){ p3.SetActive(false); GameObject.Find("P3Cam").SetActive(false);}
        if (LevelSettings.numberOfPlayers <= 1){ p2.SetActive(false); GameObject.Find("P2Cam").SetActive(false);}
    }

    // Update is called once per frame
    void Update()
    {

    }
}
