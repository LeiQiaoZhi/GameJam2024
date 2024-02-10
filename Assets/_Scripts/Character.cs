using UnityEngine;

[CreateAssetMenu(fileName = "Charater Stats", menuName = "Create Character")]
public class Character : ScriptableObject
{
    [SerializeField] private string name_;
    [SerializeField] private float speed_;
    [SerializeField] private float attackDamage_;

    public string Name_ { get => name_; }
    public float Speed_ { get => speed_; set => speed_ = value; }
    public float AttackDamage_ { get => attackDamage_; set => attackDamage_ = value; }

    public Character(string name_)
    {
        this.Speed_ = 1.0f;
        this.AttackDamage_ = 1.0f;
    }
}
