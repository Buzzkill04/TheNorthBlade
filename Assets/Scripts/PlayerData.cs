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
    public string characterPrefab; 
    //The players level
    public int playerLevel; 
    //The players strength
    public float playerStrength;
    //The current ability status
    public int characterAbilityStatus;
    //The amount of enemies the player has killed
    public int enemyKillCount;
    //The level the player was on
    public int sceneBuildIndex;
    //The players position at the time of save
    public float[] playerPosition; 
    //The amount of food in the players inventory at a time, in order {pineapple, peach, strawberry}
    public int[] inventoryItemAmounts; 

    //Need to implement scene saving


    public PlayerData(PlayerLife currentPlayer)
    {
        //Set the current player data
        characterType = currentPlayer.characterType;
        characterPrefab = currentPlayer.characterPrefab;
        playerLevel = currentPlayer.playerLevel;
        playerStrength = currentPlayer.playerStrength;
        characterAbilityStatus = currentPlayer.abilityStatus;
        enemyKillCount = currentPlayer.enemiesKilled;
        sceneBuildIndex = currentPlayer.sceneBuildIndex;
        //Player position (x, y, z)
        playerPosition = new float[3];
        playerPosition[0] = currentPlayer.transform.position.x;
        playerPosition[1] = currentPlayer.transform.position.y;
        playerPosition[2] = currentPlayer.transform.position.z;
        //The amount of each collectable item (numPineapple, numPeach, numStrawberry)
        inventoryItemAmounts = new int[3];
        inventoryItemAmounts[0] = currentPlayer.numPinapple;
        inventoryItemAmounts[1] = currentPlayer.numPeach;
        inventoryItemAmounts[2] = currentPlayer.numStrawberry;

    }

}
