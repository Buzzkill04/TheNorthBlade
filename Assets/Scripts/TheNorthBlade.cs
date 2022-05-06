using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheNorthBlade : MonoBehaviour
{
    //Whether the item has been collected
    public bool collected = false;

    // Update is called once per frame
    void Update()
    {
        //If the item was collected
        if (collected)
        {
            //Get the centre of the screen
            Vector2 centreOfScreen = new Vector2(Screen.width / 2, Screen.height / 2);
            //Move the north blade to the centre of the screen
            transform.position = Vector2.MoveTowards(transform.position, centreOfScreen, 0.01f);
        }
        else
        {
            //Have the sprite bob up and down
            Item.BobUpAndDownSprite(transform);
        }
    }
}
