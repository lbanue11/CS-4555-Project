using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Slider slider;
    public PlayerStats playerStats;

    void Update()
    {
        if (playerStats != null && slider != null)
        {
            slider.maxValue = playerStats.maxHealth;
            slider.value = playerStats.currentHealth;
        }
    }
}