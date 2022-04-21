using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        BobUpAndDownSprite(transform);
    }

    public static void BobUpAndDownSprite(Transform trsfm)
    {
        //Use a sin curve to get the amount to increase and decrease the position by
        //A regular sin curve has too many values between -1 and 1, causing the position to skyrocket.
        //Dividing by 1000 essentially clamps the value between two very small numbers
        //Clamp cannot be used as it does not allow for smooth floating, jitters can occur
        float curve = Mathf.Sin(Time.time) / 10000;
        //Calculate the new Y position
        float newYPos = trsfm.position.y + curve;
        //Make the position of the sprite the old x position and the new Y position, 2D games Z is always 0
        trsfm.position = new Vector3(trsfm.position.x, newYPos, 0f);

    }
}
