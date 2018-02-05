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
    private Vector3 trackerVel;
    Vector3 trackerOffset;
    void Start()
    {
        transposer = FindObjectOfType<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>();
        ranks = FindObjectOfType<RankManager>();
        tracker = GameObject.Find("CameraTracker").transform;
        followOffsetYDefault = transposer.m_FollowOffset.y;
    }

    //iterate through every human marble by rank, as soon as one is found, set camera and kill the process
    void WritePlayerToFollow()
    {
        for (int i = 0; i <= ranks.players.Count; i++)
        {
            if (ranks.players[i].isPlayer && ranks.players[i].isActiveAndEnabled)
            {
                if (ranks.players[i].transform != playerToFollow){
                    playerToFollow = ranks.players[i].transform;
                    trackerOffset = tracker.position - playerToFollow.position;
                }

                var dist = Vector3.Distance(tracker.position, playerToFollow.position);
                if (Mathf.Abs(dist) > 0.01f){
                    //Need to get closer to target
                    tracker.position = playerToFollow.position + trackerOffset;
                    tracker.position = Vector3.SmoothDamp(tracker.position, playerToFollow.position, ref trackerVel,2);
                    trackerOffset = tracker.position - playerToFollow.position;
                }else{
                    //This is fine, just keep up
                    tracker.position = playerToFollow.position;
                }
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
