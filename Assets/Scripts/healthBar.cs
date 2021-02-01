using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public Slider slider;
    public GameObject player;
    private ReceiveDamage playerDamage;

    void Start()
    {
        playerDamage = player.GetComponent<ReceiveDamage>();

        if(playerDamage) {
            slider.maxValue = playerDamage.StartHealth;
            slider.value = playerDamage.CurrentHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDamage) {
            slider.value = playerDamage.CurrentHealth;
        }
    }
}
