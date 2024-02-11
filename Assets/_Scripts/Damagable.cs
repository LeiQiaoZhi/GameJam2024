using System;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private ParticleSystem hitEffectPrefab;
    [SerializeField] private GameObject deathEffectPrefab;
    // [SerializeField] private AudioSource deathAudio;

    public event Action OnDamage;
    public event Action OnDeath;

    private int currentHealth_;

    // returns maxhealth
    public int MaxHealth
    {
        get => maxHealth;
    }

    public int CurrentHealth
    {
        get => currentHealth_;
    }

    private void Awake()
    {
        currentHealth_ = maxHealth;
    }

    public void Damage(int _damage)
    {
        currentHealth_ -= _damage;
        OnDamage?.Invoke();
        if (currentHealth_ <= 0)
        {
            Death();
        }
    }

    protected virtual void Death()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }

    public void DeathEffect(RaycastHit _result)
    {
        if (deathEffectPrefab != null)
        {
            GameObject deathEffect = Instantiate(deathEffectPrefab);
            deathEffect.transform.position = transform.position;
            Destroy(deathEffect.gameObject, 2.0f);
        }
    }

    public void DamageEffect(RaycastHit _result)
    {
        ParticleSystem hitEffect = Instantiate(hitEffectPrefab);
        hitEffect.transform.position = _result.point;
        hitEffect.transform.forward = _result.normal;
        hitEffect.Play();
        Destroy(hitEffect.gameObject, hitEffect.main.duration);

        if (currentHealth_ <= 0)
        {
            DeathEffect(_result);
        }
    }
}