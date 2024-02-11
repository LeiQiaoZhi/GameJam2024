using Unity.VisualScripting;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private ParticleSystem hitEffectPrefab;
    
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

    public void DamageEffect(RaycastHit _result)
    {
        ParticleSystem hitEffect = Instantiate(hitEffectPrefab);
        hitEffect.transform.position = _result.point;
        hitEffect.transform.forward = _result.normal;
        hitEffect.Play();       
        Destroy(hitEffect.gameObject, hitEffect.main.duration);
    }
}