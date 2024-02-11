using Unity.VisualScripting;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private ParticleSystem hitEffectPrefab;
    [SerializeField] private GameObject deathEffectPrefab;
    
    private int currentHealth_;

    private void Start()
    {
        currentHealth_ = maxHealth;
    }

    public void Damage(int _damage)
    {
        currentHealth_ -= _damage;
        if (currentHealth_ <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void DeathEffect(RaycastHit _result)
    {
        GameObject deathEffect = Instantiate(deathEffectPrefab);
        deathEffect.transform.position = transform.position;
        Destroy(deathEffect.gameObject, 1.0f);
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