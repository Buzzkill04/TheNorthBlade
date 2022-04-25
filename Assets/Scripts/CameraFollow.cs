using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Game object the camera will follow
    private GameObject characterToFollow;
    //The camera that will move with the camera
    public Camera sceneCam;
    //The Y and Z position the camera needs to be locked on
    public float sceneCamY;
    public float XBounds1;
    public float XBounds2;
    const float sceneCamZ = -10f;

    // Update is called once per frame
    void Update()
    {
        characterToFollow = GameObject.FindGameObjectWithTag("Player");
        //Change the position of the camera to the characterToFollow's X position once per frame, with the Y and Z locked
        //Clamp the x position between the bounds so that the camera doesnt go beyond the worlds bounds
        sceneCam.transform.position = new Vector3(Mathf.Clamp(characterToFollow.transform.position.x, XBounds1, XBounds2), sceneCamY, sceneCamZ);
    }
}

