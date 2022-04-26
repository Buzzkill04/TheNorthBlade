using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthFillSlider;
    public PlayerLife playerLifeScript;

    //Called every frame
    private void Update()
    {
        playerLifeScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLife>();
        SetHealthFillSlider(playerLifeScript.playerHealth);
    }

    public void SetHealthFillSlider(float health)
    {
        healthFillSlider.value = health;
    }
}
