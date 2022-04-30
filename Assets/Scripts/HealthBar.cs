using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //The slider controls the width of the fill object
    public Slider healthFillSlider;
    public PlayerLife playerLifeScript;
    public Text currHealthText;
    public Text maxHealthText;

    //Called every frame
    private void Update()
    {
        //Get the player life script 
        playerLifeScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLife>();
        //Set the slider value to the players health
        SetHealthFillSlider(playerLifeScript.playerLevel, playerLifeScript.playerHealth);
    }

    public void SetHealthFillSlider(int playerLevel, float health)
    {
        //The max value should be the amount needed to use the ability which is 7 - the player level
        healthFillSlider.maxValue = 100*playerLevel;
        //Set the health slider value to the health
        healthFillSlider.value = health;
        //Set the text of the health, so the player has a better idea as to their health
        maxHealthText.text = (100 * playerLevel).ToString();
        currHealthText.text = health.ToString();
    }
}
