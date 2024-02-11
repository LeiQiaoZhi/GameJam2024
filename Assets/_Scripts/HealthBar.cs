using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Damagable))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBarSlider;
    
    private Damagable damagable_;

    private void Start()
    {
        if (damagable_ == null)
            damagable_ = GetComponent<Damagable>();
        healthBarSlider.maxValue = damagable_.MaxHealth;
        healthBarSlider.value = damagable_.CurrentHealth;
    }

    private void OnEnable()
    {
        if (damagable_ == null)
            damagable_ = GetComponent<Damagable>();
        damagable_.OnDamage += UpdateHealthBar;
        damagable_.OnDeath += DestroyHealthBar;
    }

    private void OnDisable()
    {
        damagable_.OnDamage -= UpdateHealthBar;
        damagable_.OnDeath -= DestroyHealthBar;
    
    }

    private void DestroyHealthBar()
    {
        Destroy(healthBarSlider.gameObject);
    }

    private void UpdateHealthBar()
    {
        healthBarSlider.value = damagable_.CurrentHealth;
    }
}