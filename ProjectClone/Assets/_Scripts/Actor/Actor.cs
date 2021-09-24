using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    [HideInInspector] public int HP;
    public int maxHP = 6;

    public float moveSpeed = 1f;

    protected virtual void Update()
    {
        Move();    
    }

    public virtual void Init()
    {
        HP = maxHP;
    }

    public virtual void Move() { }

    public virtual bool GetDamage(int power)
    {
        HP = Mathf.Clamp(HP - power, 0, maxHP);
        if (HP <= 0)
        {
            Dead();
        }

        return HP <= 0;
    }

    public virtual void Dead() { }
}

public interface IAttackabale
{
    public void Attack();
}