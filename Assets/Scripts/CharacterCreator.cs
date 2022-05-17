using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterCreator : MonoBehaviour
{
    //Create static fields for the character type and prefab name
    public static string characterType;
    public static string characterPrefabName;
    public static string MultiplayerCharacterType;
    public static string MultiplayerCharacterPrefabName;
    //Animator for fading effect
    public Animator animator;

    //Called when the wizard radio button is toggled
    public void WizardButton(bool toggled)
    {
        //If the button is toggled to being selected
        if (toggled)
        {
            //If there is no multiplayer character type
            if (characterType == null)
            {
                //Character type is wizard
                characterType = "wizard";
            }
            //Otherwise
            else
            {
                //Multiplayer character type is wizard
                MultiplayerCharacterType = "wizard";
            }
            //Get a random number between 1,3, the two possible numbers are 1 and 2, 3 is excluded
            int num = (int)Random.Range(1, 3);
            switch (num)
            {
                case 1:
                    //Same process as above, if there is no multiplayer character prefab name 
                    if (characterPrefabName == null)
                    {
                        //make the character prefab name wiz1
                        characterPrefabName = "WIZ1";
                    }
                    else
                    {
                        //Otherwise make the multiplayer character prefab name wiz1
                        MultiplayerCharacterPrefabName = "WIZ1";
                    }
                    break;
                case 2:
                    if (characterPrefabName == null)
                    {
                        //make the character prefab name wiz1
                        characterPrefabName = "WIZ2";
                    }
                    else
                    {
                        //Otherwise make the multiplayer character prefab name wiz1
                        MultiplayerCharacterPrefabName = "WIZ2";
                    }
                    break;
                default:
                    break;
            }
        }
    }
    //Called when the wizard radio button is toggled
    public void SSButton(bool toggled)
    {
        //If the button is toggled to being selected
        if (toggled)
        {
            //Make the character type the skeleton scout and choose a random prefab
            //If there is no multiplayer character type
            if (characterType == null)
            {
                //Character type is skeleton scout
                characterType = "sScout";
            }
            //Otherwise
            else
            {
                //Multiplayer character type is skeleton scout
                MultiplayerCharacterType = "sScout";
            }
            //Get a random number between 1,3, the two possible numbers are 1 and 2, 3 is excluded
            int num = (int)Random.Range(1, 3);
            switch (num)
            {
                case 1:
                    //Same process as above, if there is no multiplayer character prefab name 
                    if (characterPrefabName == null)
                    {
                        //make the character prefab name SS1
                        characterPrefabName = "SS1";
                    }
                    else
                    {
                        //Otherwise make the multiplayer character prefab name SS1
                        MultiplayerCharacterPrefabName = "SS1";
                    }
                    break;
                case 2:
                    if (characterPrefabName == null)
                    {
                        //make the character prefab name SS2
                        characterPrefabName = "SS2";
                    }
                    else
                    {
                        //Otherwise make the multiplayer character prefab name SS2
                        MultiplayerCharacterPrefabName = "SS2";
                    }
                    break;
                default:
                    break;
            }
        }
    }
    //Called when the wizard radio button is toggled
    public void SMButton(bool toggled)
    {
        //If the button is toggled to being selected
        if (toggled)
        {
            //Make the character type the swordsman and choose a random prefab
            //If there is no multiplayer character type
            if (characterType == null)
            {
                //Character type is swordsman
                characterType = "swordsman";
            }
            //Otherwise
            else
            {
                //Multiplayer character type is swordsman
                MultiplayerCharacterType = "swordsman";
            }
            //Get a random number between 1,3, the two possible numbers are 1 and 2, 3 is excluded
            int num = (int)Random.Range(1, 3);
            switch (num)
            {
                case 1:
                    //Same process as above, if there is no multiplayer character prefab name 
                    if (characterPrefabName == null)
                    {
                        //make the character prefab name SM1
                        characterPrefabName = "SM1";
                    }
                    else
                    {
                        //Otherwise make the multiplayer character prefab name SM1
                        MultiplayerCharacterPrefabName = "SM1";
                    }
                    break;
                case 2:
                    if (characterPrefabName == null)
                    {
                        //make the character prefab name SM2
                        characterPrefabName = "SM2";
                    }
                    else
                    {
                        //Otherwise make the multiplayer character prefab name SM2
                        MultiplayerCharacterPrefabName = "SM2";
                    }
                    break;
                default:
                    break;
            }
        }
    }

    //Called when continue is pressed
    public void Continue()
    {
        //If a character has been selected
        if (characterType != null || MultiplayerCharacterType != null)
        {
            //If the gamemode is not multiplayer load the dark forest
            if (!MainMenu.isMultiplayer)
            {
                StartCoroutine(LoadNextScene(2));
            }
            //otherwise load the multiplayer scene
            else if (MainMenu.isMultiplayer && MultiplayerCharacterType == null && MultiplayerCharacterPrefabName == null)
            {
                //Go to the character creator again so player 2 can pick their character
                StartCoroutine(LoadNextScene(1));
                
            }
            else if (MainMenu.isMultiplayer && characterPrefabName != null && MultiplayerCharacterPrefabName != null)
            {
                //Start the duel
                StartCoroutine(LoadNextScene(9));
            }
        }
        
    }
    IEnumerator LoadNextScene(int sceneIndex)
    {
        //Fade the screen to black,
        animator.SetTrigger("Start");
        //Wait for 1 second
        yield return new WaitForSeconds(1);
        //Load the starting screen
        SceneManager.LoadScene(sceneIndex);
    }

}
