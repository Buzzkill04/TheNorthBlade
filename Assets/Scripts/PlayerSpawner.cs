using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private GameObject characterPrefab;
    private string characterPrefabName;
    // Start is called before the first frame update
    void Start()
    {
        //If the character creator was used (The player made a new game at the start of the play session)
        if (CharacterCreator.characterPrefabName != null)
        {
            //Get the prefab name from the character creator
            characterPrefabName = CharacterCreator.characterPrefabName;

        }
        else
        {
            //If the player used continue, load the player's prefab name
            characterPrefabName = LoadPlayerPrefabName();
        }
        //Get the prefab game object based from the name
        characterPrefab = (GameObject)Resources.Load(@$"Prefabs/{characterPrefabName}");
        //Create the prefab
        Instantiate(characterPrefab, transform.position, transform.rotation);
    }

    public string LoadPlayerPrefabName()
    {
        //Call the load player method and store the return value
        PlayerData savedPlayerData = SaveSystem.LoadPlayer();
        return savedPlayerData.characterPrefabName;
    }
}
