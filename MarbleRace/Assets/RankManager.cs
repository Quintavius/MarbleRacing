using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankManager : MonoBehaviour
{  
  public GameObject rankingCanvas;
  public Text rankingText;
  bool raceOver;
  bool raceOverCheck;
  public List<MarbleRank> winners;
  [HideInInspector]
  public List<MarbleRank> players;
  // Use this for initialization
  void Start()
  {
    raceOverCheck = true;
    //Throw all players in a list
    MarbleRank[] tempList = FindObjectsOfType<MarbleRank>();
    foreach (MarbleRank rank in tempList)
    {
        players.Add(rank);
    }
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

    //Check if all players are done
    foreach (MarbleRank marble in players){
      if (marble.isPlayer){
        //if ANY player marble has NOT finished, set the check to FALSE
        if (!marble.isFinished){
          raceOverCheck = false;
        }
      }
    }
    //if the snare has been triggered, reset snare
    if (raceOverCheck == false){ raceOverCheck = true;}
    //Snare was never triggered, all players are done
    else if (raceOverCheck == true){raceOver = true;}

    if (raceOver){
      rankingCanvas.SetActive(true);
      foreach (MarbleRank marble in winners){
        if (!marble.ranked){
          rankingText.text += marble.gameObject + "\n";
          marble.ranked = true;
        }
      }
    }
  }
}
