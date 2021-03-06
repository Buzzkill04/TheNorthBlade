using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    //The player life script attached to the player
    private PlayerLife playerLifeScript;
    //The player's inventory manager
    private InventoryManager playerInventoryManager;
    //All the text fields that need to be changed in the inventory 
    public Text pineAmountText;
    public Text peachAmountText;
    public Text strawAmountText;
    public Text currWeightText;
    public Text maxHealthText;
    public Text strengthText;
    public Text levelText;
    public Text enemiesKilledText;

    // Update is called once per frame
    void Update()
    {
        //Get the player script
        playerLifeScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLife>();
        //Get the player inventory manager
        playerInventoryManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();
        //Update the texts in the UI
        SetTexts(playerLifeScript, playerInventoryManager);
    }

    public void SetTexts(PlayerLife playerLifeScript, InventoryManager playerInventoryManager)
    {
        //Set the value of the pineapple, peach and strawberry text fields to the stored amounts
        pineAmountText.text = playerInventoryManager.numPinapple.ToString();
        peachAmountText.text = playerInventoryManager.numPeach.ToString();
        strawAmountText.text = playerInventoryManager.numStrawberry.ToString();
        //Set the value current weight to the sum of all the inventory items
        currWeightText.text = (playerInventoryManager.numPinapple + playerInventoryManager.numPeach + playerInventoryManager.numStrawberry).ToString();
        //Set the stats text to the current player stats
        maxHealthText.text = playerLifeScript.maxPlayerHealth.ToString();
        strengthText.text = playerLifeScript.playerStrength.ToString();
        levelText.text = playerLifeScript.playerLevel.ToString();
        enemiesKilledText.text = playerLifeScript.enemiesKilled.ToString();
    }
    //Called when the player uses a pineapple from the menu
    public void UsePineapple()
    {
        if (playerInventoryManager.numPinapple >= 1)
        {
            //Heal the player by the correct amount
            playerLifeScript.PlayerHeal(playerInventoryManager.pineHeal);
            playerInventoryManager.numPinapple--;
        }
    }
    //Called when the player uses a peach from the menu
    public void UsePeach()
    {
        if (playerInventoryManager.numPeach >= 1)
        {
            //Heal the player by the correct amount
            playerLifeScript.PlayerHeal(playerInventoryManager.peachHeal);
            playerInventoryManager.numPeach--;
        }
    }
    //Called when the player uses a strawberry from the menu
    public void UseStrawberry()
    {
        if (playerInventoryManager.numStrawberry >= 1)
        {
            //Heal the player by the correct amount
            playerLifeScript.PlayerHeal(playerInventoryManager.strawHeal);
            playerInventoryManager.numStrawberry--;
        }
    }
}
