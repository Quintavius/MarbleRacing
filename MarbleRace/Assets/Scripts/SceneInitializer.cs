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
    Camera p1Cam;
    Camera p2Cam;
    Camera p3Cam;
    Camera p4Cam;
    List<Camera> activePlayerCams;
    void Start()
    {
        activePlayerCams = new List<Camera>();
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

        //Initialize camera references
        p1Cam = GameObject.Find("P1Cam").GetComponent<Camera>();
        p2Cam = GameObject.Find("P2Cam").GetComponent<Camera>();
        p3Cam = GameObject.Find("P3Cam").GetComponent<Camera>();
        p4Cam = GameObject.Find("P4Cam").GetComponent<Camera>();

        //Delete players as necessary, name and skin remainders
        if (!LevelSettings.player1active) {p1.SetActive(false); p1Cam.gameObject.SetActive(false); GameObject.Find("P1RankCanvas").SetActive(false);} 
        else {activePlayerCams.Add(p1Cam);}

        if (!LevelSettings.player2active){ p2.SetActive(false); p2Cam.gameObject.SetActive(false); GameObject.Find("P2RankCanvas").SetActive(false);} 
        else {activePlayerCams.Add(p2Cam);}

        if (!LevelSettings.player3active){ p3.SetActive(false); p3Cam.gameObject.SetActive(false); GameObject.Find("P3RankCanvas").SetActive(false);} 
        else {activePlayerCams.Add(p3Cam);}

        if (!LevelSettings.player4active){ p4.SetActive(false); p4Cam.gameObject.SetActive(false); GameObject.Find("P4RankCanvas").SetActive(false);} 
        else {activePlayerCams.Add(p4Cam);}

        SetUpSplitscreen();
    }

    void DisableMainCam(){
        Camera.main.gameObject.SetActive(false);
    }

    //Function to quickly set up camera, throw in ID of player by active !!!BASE ZERO!!!
    void SetPlayerCamera(int playerID, float x, float y, float w, float h){
        Camera cam = activePlayerCams[playerID];
        cam.rect = new Rect(x,y,w,h);
    }

    void SetUpSplitscreen()
    {
        if (LevelSettings.numberOfPlayers == 1){
            SetPlayerCamera(0,-1,-0.5f,0.1f,0.1f);
        }
        if (LevelSettings.numberOfPlayers == 2){
            //2 players, disable main and 2 non players
            //DisableMainCam();
            SetPlayerCamera(0,0,0.5f,1f,0.5f);
            SetPlayerCamera(1,0f,0f,1f,0.5f);

        }else if (LevelSettings.numberOfPlayers == 3){
            //3 players, disable only non player, move main into corner
            SetPlayerCamera(0,0,0.5f,0.5f,0.5f);
            SetPlayerCamera(1,0.5f,0.5f,0.5f,0.5f);
            SetPlayerCamera(2,0,0f,0.5f,0.5f);
            Camera.main.rect = new Rect(0.5f,0f,0.5f,0.5f);

        }else if (LevelSettings.numberOfPlayers == 4){
            //4 players, disable main
            //DisableMainCam();
            SetPlayerCamera(0,0,0.5f,0.5f,0.5f);
            SetPlayerCamera(1,0.5f,0.5f,0.5f,0.5f);
            SetPlayerCamera(2,0,0f,0.5f,0.5f);
            SetPlayerCamera(3,0.5f,0f,0.5f,0.5f);
        }
    }
}
