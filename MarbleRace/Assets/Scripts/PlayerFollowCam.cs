using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This one sits on the static player camers, it does not touch cinemachine and only handles the corner cams!!
public class PlayerFollowCam : MonoBehaviour
{
    public Transform followPlayer;
    Camera cam;
    Camera mainCam;
    CameraOutline camOutline;
    Vector3 screenPoint;
    Vector3 offset;
    public bool oldSchoolSplitscreen = false;

    [Range(0,10)]
    public float ZoomSmoothing = 2;
    [Range(0,1)]
    public float ZoomScale = 0.2f;
    float followOffsetYDefault;
    private float vel;
    [Range(0,1)]
    public float lookAheadDist;
    [Range(0,3)]
    public float lookAheadSmoothing;
    Vector3 lookAheadDir;
    private Vector3 lookAheadRef;
    void Start()
    {
        cam = GetComponent<Camera>();
        offset = transform.position - followPlayer.transform.position;
        mainCam = Camera.main;
        camOutline = GetComponent<CameraOutline>();
        followOffsetYDefault = offset.y;
    }

    void LookAhead(){
        Vector3 rawLookAhead = followPlayer.position + (followPlayer.GetComponent<Rigidbody>().velocity * lookAheadDist);
        lookAheadDir = Vector3.SmoothDamp(lookAheadDir, rawLookAhead, ref lookAheadRef, lookAheadSmoothing);
    }

    void ToggleCameraDynamic(){
        var xBorder = Screen.width / 10;
        var yBorder = Screen.height / 10;
        screenPoint = mainCam.WorldToScreenPoint(followPlayer.position);                //We're checking if I'm visible to the main cam
        if ((screenPoint.x > 0 + xBorder && screenPoint.x < Screen.width - xBorder)     //Am I within X
        && (screenPoint.y > 0 + yBorder && screenPoint.y < Screen.height - yBorder))    //Am I within Y
        {
            if (screenPoint.z > 0)                                                      //Am i in front of the camera? Just checking
            {
                cam.enabled = false;                                                    //Main cam can see me, no need for follow cam
                camOutline.showOutline = false;
            }
            else
            {
                cam.enabled = true;                                                     //Fuck i'm somehow behind the main cam
                camOutline.showOutline = true;
            }
        }
        else
        {
            cam.enabled = true;                                                         //shit i'm out of bounds
            camOutline.showOutline = true;
        }
    }

    void AdjustCameraDistance(){
        var playerSpeed = followPlayer.GetComponent<Rigidbody>().velocity.magnitude * ZoomScale;

        if (Mathf.Abs(playerSpeed) >= 1 ){
            var newOffset = Mathf.SmoothDamp(offset.y, followOffsetYDefault * playerSpeed, ref vel,ZoomSmoothing);
            offset = new Vector3(offset.x, newOffset, offset.z);
        }else{
            var newOffset = Mathf.SmoothDamp(offset.y, followOffsetYDefault, ref vel,ZoomSmoothing);
            offset = new Vector3(offset.x, newOffset, offset.z);
        }
        transform.position = followPlayer.transform.position + offset;        
    }

    void LookAtPlayer(){
        transform.LookAt(lookAheadDir);
    }

    void Update()
    {
        if (!oldSchoolSplitscreen){
            ToggleCameraDynamic();
        }
    }
    void FixedUpdate()
    {
        AdjustCameraDistance();
        LookAhead();
        LookAtPlayer();
    }
   
}
