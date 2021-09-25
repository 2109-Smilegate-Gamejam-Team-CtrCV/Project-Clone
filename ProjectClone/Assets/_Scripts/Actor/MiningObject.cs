using DG.Tweening;
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
        GetComponent<SpriteRenderer>().material = new Material(GetComponent<SpriteRenderer>().material.shader);
    }

    public void Init()
    {
        HP = maxHP;
    }

    public bool GetDamage(int power)
    {
        HP = Mathf.Clamp(HP - power, 0, maxHP);
        Debug.Log("cur hp : " + HP);
        transform.DOShakePosition(0.25f,new Vector2(0.35f, 0.2f),50);
        GetComponent<SpriteRenderer>().material.DOColor(Color.black, "_Addtive", 0.25f).From(Color.white);
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
        if (eResource == EResource.Organism)
        {
            GetComponent<SpriteRenderer>().DOFade(0, 0.5f).From(1);
            transform.DORotate(new Vector3(0, 0, 90),0.5f);

        }
        else
        {
            GetComponent<SpriteRenderer>().DOFade(0, 0.5f).From(1);
            transform.DOScaleY(0.1f, 0.5f);
        }

        Destroy(gameObject,0.5f);
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