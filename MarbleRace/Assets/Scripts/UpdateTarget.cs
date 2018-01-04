using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class UpdateTarget : MonoBehaviour
{
    Transform tracker;
    RankManager ranks;
    Transform playerToFollow; //Get this to decide who to follow
    void Start()
    {
        ranks = FindObjectOfType<RankManager>();
        tracker = GameObject.Find("CameraTracker").transform;

    }
    void Update()
    {
        WritePlayerToFollow();
    }
    void WritePlayerToFollow()
    {
        for (int i = 0; i <= ranks.players.Count; i++)
        {
            if (ranks.players[i].isPlayer && ranks.players[i].isActiveAndEnabled)
            {
                playerToFollow = ranks.players[i].transform;
                tracker.SetParent(ranks.players[i].transform);
                tracker.localPosition = Vector3.zero;
                break;
            }
        }
    }
}
