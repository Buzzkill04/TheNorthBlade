using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBar : MonoBehaviour
{
    //The slider controls the width of the fill object
    public Slider abilityFillSlider;
    public PlayerLife playerLifeScript;
    public PlayerCombat playerCombatScript;

    //Called every frame
    private void Update()
    {
        //Get the life script and the combat script attached to the player
        playerLifeScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLife>();
        playerCombatScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
        //Set the fill of the slider to the correct ability status
        SetAbilityFillSlider(playerLifeScript.playerLevel, playerCombatScript.characterAbilityStatus);
    }

    public void SetAbilityFillSlider(int playerLevel, int abilityStatus)
    {
        //The max value should be the amount needed to use the ability which is 7 - the player level
        abilityFillSlider.maxValue = 7 - playerLevel;
        //Set the value to the current ability status
        abilityFillSlider.value = abilityStatus;
    }


}
