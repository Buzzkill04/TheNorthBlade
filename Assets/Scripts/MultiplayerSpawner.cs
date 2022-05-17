using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerSpawner : MonoBehaviour
{
    private GameObject characterPrefab;
    private string characterPrefabName;
    // Start is called before the first frame update
    void Start()
    {
        //Get the prefab name from the character creator
        characterPrefabName = CharacterCreator.MultiplayerCharacterPrefabName;
        //Get the prefab game object based from the name
        characterPrefab = (GameObject)Resources.Load(@$"Prefabs/{characterPrefabName}");
        //Create the player character prefab
        GameObject spawnedPlayer = Instantiate(characterPrefab, transform.position, transform.rotation);
        //Make the second player layer to the enemy layer
        spawnedPlayer.layer = 9;
        //Give the second player the Enemy Tag
        spawnedPlayer.tag = "Enemy";
        //Change the movement and attack keys and set the isSecondPlayer bool to true
        spawnedPlayer.GetComponent<PlayerCombat>().attackButton = "AttackMultiplayer";
        spawnedPlayer.GetComponent<PlayerCombat>().isSecondPlayer = true;
        spawnedPlayer.GetComponent<PlayerMovement>().movementAxis = "HorizontalMultiplayer";
        spawnedPlayer.GetComponent<PlayerMovement>().jumpButton = "JumpMultiplayer";
    }
}
