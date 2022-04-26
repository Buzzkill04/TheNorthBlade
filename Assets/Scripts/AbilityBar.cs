using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBar : MonoBehaviour
{
    public Slider abilityFillSlider;
    public PlayerLife playerLifeScript;
    public PlayerCombat playerCombatScript;

    //Called every frame
    private void Update()
    {
        playerLifeScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLife>();
        playerCombatScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
        SetAbilityFillSlider(playerLifeScript.playerLevel, playerCombatScript.characterAbilityStatus);
    }

    public void SetAbilityFillSlider(int playerLevel, int abilityStatus)
    {
        //The max value should be the amount needed to use the ability which is 7 - the player level
        abilityFillSlider.maxValue = 7 - playerLevel;
        Debug.Log(abilityFillSlider.maxValue);
        //Set the value to the current ability status
        abilityFillSlider.value = abilityStatus;
        Debug.Log(abilityFillSlider.value);
    }


}
