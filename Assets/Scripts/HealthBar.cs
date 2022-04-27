using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //The slider controls the width of the fill object
    public Slider healthFillSlider;
    public PlayerLife playerLifeScript;

    //Called every frame
    private void Update()
    {
        //Get the player life script 
        playerLifeScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLife>();
        //Set the slider value to the players health
        SetHealthFillSlider(playerLifeScript.playerHealth);
    }

    public void SetHealthFillSlider(float health)
    {
        //Set the health slider value to the health
        healthFillSlider.value = health;
    }
}
