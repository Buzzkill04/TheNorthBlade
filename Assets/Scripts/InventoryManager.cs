using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //The amount of pinapples the player has 
    public int numPinapple = 0;
    //The amount of peaches the player has
    public int numPeach = 0;
    //The amount of Strawberrys the player has 
    public int numStrawberry = 0;

    //Called when the sprite the script is attached to collides with something that is marked as a trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            //Check the tag of the game obect and incrmement the correct counter then destroy the game object
            case "Pineapple":
                numPinapple++;
                Destroy(collision.gameObject);
                break;
            case "Peach":
                numPeach++;
                Destroy(collision.gameObject);
                break;
            case "Strawberry":
                numStrawberry++;
                Destroy(collision.gameObject);
                break;
            default:
                break;
        }
    }
}
