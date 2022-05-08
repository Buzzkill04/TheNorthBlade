using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Game object the camera will follow
    private GameObject characterToFollow;
    //The camera that will move with the camera
    public Camera sceneCam;
    //The Z position the camera needs to be locked on
    const float sceneCamZ = -10f;
    //The x and y positions the camera can travel between
    public float XBounds1;
    public float XBounds2;
    public float YBounds1;
    public float YBounds2;


    // Update is called once per frame
    void Update()
    {
        characterToFollow = GameObject.FindGameObjectWithTag("Player");
        //Change the position of the camera to the characterToFollow's X position once per frame, with the Y and Z locked
        //Clamp the x position between the bounds so that the camera doesnt go beyond the worlds bounds
        //We also need to clamp on the y axis for levels that have more verticality 
        sceneCam.transform.position = new Vector3(Mathf.Clamp(characterToFollow.transform.position.x, XBounds1, XBounds2), 
                                                    Mathf.Clamp(characterToFollow.transform.position.y, YBounds1, YBounds2),  
                                                        sceneCamZ);
    }
}

