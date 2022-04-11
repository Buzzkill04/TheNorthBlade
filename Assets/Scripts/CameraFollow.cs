using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Game object the camera will follow
    public GameObject characterToFollow;
    //The camera that will move with the camera
    public Camera sceneCam;
    //The Y and Z position the camera needs to be locked on
    const float sceneCamY = 0.408f;
    const float sceneCamZ = -10f;

    // Update is called once per frame
    void Update()
    {
        //Change the position of the camera to the characterToFollow's X position once per frame, with the Y and Z locked
        sceneCam.transform.position = new Vector3(characterToFollow.transform.position.x, sceneCamY, sceneCamZ);
    }
}

