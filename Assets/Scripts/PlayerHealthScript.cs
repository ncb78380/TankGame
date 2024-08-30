using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour
{
    public int healthValue = 100;
    public Slider health_Slider;

    // Start is called before the first frame update
    void Start()
    {
        health_Slider.value = healthValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyDamage(int damageAmount)
    {
        healthValue -= damageAmount;
        if (healthValue < 0)
        {
            healthValue = 0;
        }
        health_Slider.value = healthValue;

        if (healthValue == 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        }
    }
}
