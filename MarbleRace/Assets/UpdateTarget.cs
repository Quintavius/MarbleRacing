using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class UpdateTarget : MonoBehaviour
{
    public Cinemachine.CinemachineTargetGroup tg;
    RankManager ranks;
    Transform playerToFollow; //Get this to decide who to follow
    void Start()
    {
        ranks = FindObjectOfType<RankManager>();
    }
    void Update()
    {
        WritePlayerToFollow();
    }
    void WritePlayerToFollow()
    {
        for (int i = 0; i <= ranks.players.Count; i++)
        {
            if (ranks.players[i].isPlayer)
            {
				//This is fucked, change it to parent whatever the group looks at to the furthest player
                playerToFollow = ranks.players[i].transform;
                tg.m_Targets[0] = playerToFollow;
                break;
            }
        }
    }
}
