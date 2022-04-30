using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    //The Ui for the inventory
    public GameObject inventoryUI;

    // Update is called once per frame
    void Update()
    {
        //If E is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            //If the inventory is currently active
            if (inventoryUI.activeSelf)
            {
                //Deactivate the game object (close the menu)
                inventoryUI.SetActive(false);
            }
            else 
            {
                //Activate the game object (open the menu)
                inventoryUI.SetActive(true);
            }
        }
        //If any key is pressed, the left mouse button is not pressed down and the menu is active
        else if ((Input.anyKeyDown && !Input.GetMouseButtonDown(0)) && inventoryUI.activeSelf)
        {
            //Deactivate the game object (close the menu)
            inventoryUI.SetActive(false);
        }
    }
}
