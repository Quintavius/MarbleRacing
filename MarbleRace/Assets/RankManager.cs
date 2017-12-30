using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankManager : MonoBehaviour
{  
  public List<MarbleRank> winners;
  [HideInInspector]
  public List<MarbleRank> players;
  // Use this for initialization
  void Start()
  {
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
  }
}
