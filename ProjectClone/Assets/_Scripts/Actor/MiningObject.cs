using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EResource
{
    Mineral,
    Organism,
}

[RequireComponent(typeof(CircleCollider2D))]
public class MiningObject : MonoBehaviour, IGatherable
{
    public int HP;
    public int maxHP = 20;
    public EResource eResource;
    public int amount = 10;

    void Awake()
    {
        Init();    
    }

    public void Init()
    {
        HP = maxHP;
    }

    public bool GetDamage(int power)
    {
        HP = Mathf.Clamp(HP - power, 0, maxHP);
        Debug.Log("cur hp : " + HP);
        if (HP <= 0)
        {
            GainResource();
            ReturnToPool();
        }

        return HP <= 0;
    }

    public void GainResource()
    {
        // todo : amount¸¸Å­ eResource È¹µæ
        // todo : È¹µæ ¿¬Ãâ ÇÊ¿ä -> µå¶ø µÈ °É ÁÝ´ø°¡, type + amount ui ÇÊ¿ä
        GameManager.Instance.clone.GainResource(eResource, amount);
    }

    public void ReturnToPool()
    {
        Destroy(gameObject);
    }
}

public interface IGatherable
{
    public bool GetDamage(int power);
    public void GainResource();
}

public interface IBuildable
{

}