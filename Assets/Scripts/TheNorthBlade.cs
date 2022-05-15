using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheNorthBlade : MonoBehaviour
{
    //Whether the item has been collected
    public bool collected = false;
    //The scene camera
    public Camera sceneCam;

    private void Start()
    {
        sceneCam = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
       
        //If the item was collected
        if (collected)
        {
            //Get the centre of the screen
            Vector3 centreOfScreen = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            //Move the north blade to the centre of the screen
            transform.position = Vector3.MoveTowards(transform.position, sceneCam.ScreenToWorldPoint(centreOfScreen), 0.005f);
        }
        else
        {
            //Have the sprite bob up and down
            Item.BobUpAndDownSprite(transform);
        }
    }
}
