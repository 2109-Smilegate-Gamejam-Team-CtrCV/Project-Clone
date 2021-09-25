using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Actor : MonoBehaviour, IReboundable
{
    protected string[] EnemyTags; // 피격 판정 있는 태그

    public int HP;
    public int maxHP = 6;

    public float moveSpeed = 1f;
    public float reboundPower = 1f;

    protected SpriteRenderer render;
    protected Rigidbody2D rb;
    protected Animator animator;

    protected virtual void Awake()
    {
        render = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        Init();
    }

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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (ContainEnemyTag(collision.collider.tag))
        {
            Vector2 reboundDir = (transform.position2D() - collision.transform.position2D()).normalized;
            Rebound(reboundDir);
        }
    }

    protected bool ContainEnemyTag(string tag)
    {
        if (EnemyTags == null || EnemyTags.Length < 1)
            return false;

        return EnemyTags.Contains(tag);
    }

    public void Rebound(Vector2 direction)
    {
        // todo : 충돌 시, 반대 방향으로 튕겨나간다.
        rb.AddForce(direction * reboundPower, ForceMode2D.Impulse);
    }
}

public interface IReboundable
{
    public void Rebound(Vector2 direction);
}

public interface IAttackabale
{
    public void Attack();
}