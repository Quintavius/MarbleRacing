using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

//This lad here controls what the main camera is looking at and adjusts camera distance/zoom based on speed
public class UpdateTarget : MonoBehaviour
{
    [Range(0,10)]
    public float ZoomSmoothing;
    [Range(0,1)]
    public float ZoomScale;
    Transform tracker;
    RankManager ranks;
    Transform playerToFollow; //Get this to decide who to follow
    CinemachineTransposer transposer;
    float followOffsetYDefault;
    private float vel;
    void Start()
    {
        transposer = FindObjectOfType<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>();
        ranks = FindObjectOfType<RankManager>();
        tracker = GameObject.Find("CameraTracker").transform;
        followOffsetYDefault = transposer.m_FollowOffset.y;
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

    void AdjustCameraDistance(){
        var playerSpeed = playerToFollow.GetComponent<Rigidbody>().velocity.magnitude * ZoomScale;

        if (Mathf.Abs(playerSpeed) >= 1 ){
            var offset = Mathf.SmoothDamp(transposer.m_FollowOffset.y, followOffsetYDefault * playerSpeed, ref vel,ZoomSmoothing);
            transposer.m_FollowOffset = new Vector3(transposer.m_FollowOffset.x, offset, transposer.m_FollowOffset.z);
        }else{
            var offset = Mathf.SmoothDamp(transposer.m_FollowOffset.y, followOffsetYDefault, ref vel,ZoomSmoothing);
            transposer.m_FollowOffset = new Vector3(transposer.m_FollowOffset.x, offset, transposer.m_FollowOffset.z);
        }        
    }

    private void FixedUpdate(){
        WritePlayerToFollow();
        AdjustCameraDistance();
    }
}
