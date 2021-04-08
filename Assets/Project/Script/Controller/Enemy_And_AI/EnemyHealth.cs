using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float health;
    public float maxHealth;
    [Header("Objects")]
    public GameObject healthBarUI;
    public Slider slider;
    void Start()
    {
        health = maxHealth;
        slider.value = CalculateHealth();
    }
    void Update()
    {
        slider.value = CalculateHealth();

        if(health < maxHealth)
        {
            healthBarUI.SetActive(true);
        }
        if(health <= 0f)
        {
            Destroy(transform.parent.gameObject);
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    float CalculateHealth()
    {
        return health / maxHealth;
    }

    public void damageEnemy(float dmg)
    {
        health -= dmg;
    }
}
