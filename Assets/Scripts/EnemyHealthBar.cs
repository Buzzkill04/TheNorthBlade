using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    //The slider controls the width of the fill object
    public Slider healthFillSlider;
    public BossAI bossAI;

    // Update is called once per frame
    void Update()
    {
        //Set the value of the health slider to the boss' health
        SetHealthFillSlider(bossAI.bossHealth);
    }

    public void SetHealthFillSlider(float health)
    {
        //If the health is less than or equal to 0 set the value of the slider to 0
        if (health <= 0)
        {
            healthFillSlider.value = 0;
        }
        else
        {
            //Set the health slider value to the health
            healthFillSlider.value = health;
        }
    }

}
