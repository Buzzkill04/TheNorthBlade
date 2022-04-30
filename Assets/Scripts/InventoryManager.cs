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

    //The amount of health gained when a pineapple is eaten
    public int pineHeal = 35;
    //The amount of health gained when a peach is eaten
    public int peachHeal = 20;
    //The amount of health gained when a strawberry is eaten
    public int strawHeal = 10;

    //Called when the sprite the script is attached to collides with something that is marked as a trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((numPinapple + numPeach + numStrawberry) < 12)
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
}
