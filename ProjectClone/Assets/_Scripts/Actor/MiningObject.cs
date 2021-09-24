using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EResource
{
    Mineral,
    Organic,
}

[RequireComponent(typeof(Collider2D))]
public class MiningObject : MonoBehaviour, IGatherable
{
    [HideInInspector] public int HP;
    public int maxHP = 20;
    public EResource eResource;
    public int amount = 10;

    public void Init()
    {
        HP = maxHP;
    }

    public bool GetDamage(int power)
    {
        HP = Mathf.Clamp(HP - power, 0, maxHP);
        
        if (HP <= 0)
        {
            GainResource();
            ReturnToPool();
        }

        return HP <= 0;
    }

    public void GainResource()
    {
        //todo : amount¸¸Å­ eResource È¹µæ
    }

    public void ReturnToPool()
    {
        Destroy(this);
    }
}

public interface IGatherable
{
}

public interface IBuildable
{

}