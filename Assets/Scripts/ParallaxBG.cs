using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    //Length and start pos of images
    private float length, startPos;
    //Scene camera
    public GameObject sceneCam;
    //Parallax effect multiplier
    public float parallaxEffect;

    //Start is called before the first frame update
    void Start()
    {
        //Set the start pos and the length
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        
    }
    //Update is called  every frame
    private void Update()
    {
        //Get a temporary scene camera position for repeating the backgrounds, we minus the parallaxEffect from 1 because its relative to the camera
        float sceneCamTempPos = sceneCam.transform.position.x * (1 - parallaxEffect);

        //Create distance to travel
        float dist = sceneCam.transform.position.x * parallaxEffect;
        //Change the position of the game object the script is applied to
        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);
        //If the temporary scene cam pos is less than the start pos + length
        if (sceneCamTempPos > (startPos + length)) 
        {
            //Add the length to the start pos
            startPos += length;
        }
        else if (sceneCamTempPos < (startPos - length)) //If the temporary scene cam pos is less than the start pos + length            
        {
            //Minus the length from the start point
            startPos -= length;
        }
    }
}
