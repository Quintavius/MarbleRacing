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


    // Use this for initialization
    void Start()
    {
        cam = GetComponent<Camera>();
        offset = transform.position - followPlayer.transform.position;
        mainCam = Camera.main;
        camOutline = GetComponent<CameraOutline>();
    }
    void Update()
    {
        var xBorder = Screen.width / 10;
        var yBorder = Screen.height / 10;
        screenPoint = mainCam.WorldToScreenPoint(followPlayer.position); //We're checking if I'm visible to the main cam
        if ((screenPoint.x > 0 + xBorder && screenPoint.x < Screen.width - xBorder) //Am I within X
        && (screenPoint.y > 0 + yBorder && screenPoint.y < Screen.height - yBorder)) //Am I within Y
        {
            if (screenPoint.z > 0) //Am i in front of the camera? Just checking
            {
                cam.enabled = false; //Main cam can see me, no need for follow cam
                camOutline.showOutline = false;
            }
            else
            {
                cam.enabled = true; //Fuck i'm somehow behind the main cam
                camOutline.showOutline = true;
            }
        }
        else
        {
            cam.enabled = true; //shit i'm out of bounds
            camOutline.showOutline = true;
        }

    }
    void FixedUpdate()
    {
        transform.position = followPlayer.transform.position + offset;
    }
   
}
