using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RankManager : MonoBehaviour
{  
  public GameObject rankingCanvas;
  public GameObject firstSelectedButton;
  public Text rankingText;
  bool raceOver;
  bool raceOverCheck;
  bool rankCanvasEnabled;
  public List<MarbleRank> winners;
  [HideInInspector]
  public List<MarbleRank> players; //this is ALL marbles not just humans
  int marblesFinished;
  SceneInitializer initializer;
  EventSystem eventSystem;
  void Start()
  {
    eventSystem = FindObjectOfType<EventSystem>();
    initializer = GameObject.FindObjectOfType<SceneInitializer>();
    marblesFinished = 0;
    raceOverCheck = true;
    //Throw all players in a list
    MarbleRank[] tempList = FindObjectsOfType<MarbleRank>();
    foreach (MarbleRank rank in tempList)
    {
      if (rank.isActiveAndEnabled){
      players.Add(rank);
      }
    }
  }

  void EnableRankCanvas(){
      rankingCanvas.SetActive(true);
      eventSystem.SetSelectedGameObject(firstSelectedButton);
      rankCanvasEnabled = true;
  }

  void Update()
  {
    players.Sort(delegate (MarbleRank x, MarbleRank y)
    {
      //Ahead by a waypoint
      if (x.lastWaypoint > y.lastWaypoint)
      {
        return -1;
      }
      //Behind by a waypoint
      else if (x.lastWaypoint < y.lastWaypoint)
      {
        return 1;
      }
      //On the same waypoint
      else if (x.lastWaypoint == y.lastWaypoint)
      {
        //Less distance to next waypoint
        if (x.nextWaypointDistance < y.nextWaypointDistance)
        {
          return -1;
        }
        //More or equal distance to next waypoint
        else
        {
          return 1;
        }
      }
      return 0;
    });

    for (int i = 0; i < players.Count; i++){
      if (players[i].isPlayer && players[i].isActiveAndEnabled){
        //if ANY player marble has NOT finished, set the check to FALSE
        if (!players[i].isFinished){
          raceOverCheck = false;
        }
      }
      if (!players[i].isFinished){
        players[i].currentRank = i+1;
      }
    }

    //if the snare has been triggered, reset snare
    if (raceOverCheck == false){ raceOverCheck = true;}
    //Snare was never triggered, all players are done
    else if (raceOverCheck == true){raceOver = true;}

    if (raceOver){
      if (!rankCanvasEnabled){
        EnableRankCanvas();
      }
      foreach (MarbleRank marble in winners){
        var marbleName = marble.gameObject.name;
        if (marble.isPlayer){
          if (marble.gameObject == initializer.p1){marbleName = "<color=#336FFFFF>"+marbleName+"</color>";}
          else if (marble.gameObject == initializer.p2){marbleName = "<color=#CE4444FF>"+marbleName+"</color>";}
          else if (marble.gameObject == initializer.p3){marbleName = "<color=#FFCA40FF>"+marbleName+"</color>";}
          else if (marble.gameObject == initializer.p4){marbleName = "<color=#1EBF1FFF>"+marbleName+"</color>";}
        }
        if (!marble.ranked){
          if (marblesFinished == 0){rankingText.text += "1st " + marbleName + "\n";}
          else if (marblesFinished == 1){rankingText.text += "2nd " + marbleName + "\n";}
          else if (marblesFinished == 2){rankingText.text += "3rd " + marbleName + "\n";}
          else if (marblesFinished >= 3){rankingText.text += (marblesFinished+1) + "th " + marbleName + "\n";}
          marblesFinished++;
          marble.ranked = true;
        }
      }
    }
  }
}
