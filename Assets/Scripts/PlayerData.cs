using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Tutorial followed https://www.youtube.com/watch?v=XOjd_qU2Ido
[System.Serializable]
public class PlayerData
{
    //The characters type
    public string characterType; 
    //The characters prefab
    public string characterPrefabName; 
    //The players level
    public int playerLevel;
    //The amount of XP the player has 
    public float playerXP;
    //The players health at the end of a level
    public float playerHealth;
    //The players strength
    public float playerStrength;
    //The current ability status
    public int characterAbilityStatus;
    //The amount of enemies the player has killed
    public int enemyKillCount;
    //The level the player was on
    public int sceneBuildIndex;
    //The amount of food in the players inventory at a time, in order {pineapple, peach, strawberry}
    public int[] inventoryItemAmounts; 



    public PlayerData(PlayerLife currentPlayer)
    {
        //Set the current player data
        characterType = currentPlayer.characterType;
        characterPrefabName = currentPlayer.characterPrefabName;
        playerLevel = currentPlayer.playerLevel;
        playerXP = currentPlayer.playerXP;
        playerHealth = currentPlayer.playerHealth;
        playerStrength = currentPlayer.playerStrength;
        characterAbilityStatus = currentPlayer.abilityStatus;
        enemyKillCount = currentPlayer.enemiesKilled;
        sceneBuildIndex = currentPlayer.sceneBuildIndex;
        //The amount of each collectable item (numPineapple, numPeach, numStrawberry)
        inventoryItemAmounts = new int[3];
        inventoryItemAmounts[0] = currentPlayer.numPinapple;
        inventoryItemAmounts[1] = currentPlayer.numPeach;
        inventoryItemAmounts[2] = currentPlayer.numStrawberry;

    }

}
