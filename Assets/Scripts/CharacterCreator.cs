using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterCreator : MonoBehaviour
{
    //Create static fields for the character type and prefab name
    public static string characterType;
    public static string characterPrefabName;
    //Animator for fading effect
    public Animator animator;

    //Called when the wizard radio button is toggled
    public void WizardButton(bool toggled)
    {
        //If the button is toggled to being selected
        if (toggled)
        {
            //Make the character type the wizard and choose a random prefab
            characterType = "wizard";
            int num = (int)Random.Range(1, 3);
            switch (num)
            {
                case 1:
                    characterPrefabName = "WIZ1";
                    break;
                case 2:
                    characterPrefabName = "WIZ2";
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
            characterType = "sScout";
            int num = (int)Random.Range(1, 3);
            switch (num)
            {
                case 1:
                    characterPrefabName = "SS1";
                    break;
                case 2:
                    characterPrefabName = "SS2";
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
            characterType = "swordsman";
            int num = (int)Random.Range(1, 3);
            switch (num)
            {
                case 1:
                    characterPrefabName = "SM1";
                    break;
                case 2:
                    characterPrefabName = "SM2";
                    break;
                default:
                    break;
            }
        }
    }

    //Called when continue is pressed
    public void Continue()
    {
        StartCoroutine(LoadStart());
    }
    IEnumerator LoadStart()
    {
        //Fade the screen to black,
        animator.SetTrigger("Start");
        //Wait for 1 second
        yield return new WaitForSeconds(1);
        //Load the starting screen
        SceneManager.LoadScene("DarkForest");

    }

}
