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

    protected Transform root;
    protected SpriteRenderer render;
    protected Rigidbody2D rb;
    protected Animator animator;

    protected bool isDead = false;

    protected virtual void Awake()
    {
        root = transform.GetChild(0);

        render = GetComponentInChildren<SpriteRenderer>();
        render.sortingOrder = -Utility.World2Grid(transform.position).y;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    protected void FixedUpdate()
    {
        render.sortingOrder = -Utility.World2Grid(transform.position + Vector3.down * 0.7f).y;
    }

    protected virtual void Update()
    {
        if (isDead)
            return;

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
        //Debug.LogFormat("{0} HP : {1}", gameObject.name, HP);

        if (HP <= 0)
        {
            Dead();
        }

        return HP <= 0;
    }

    public virtual void Dead() {
        SoundManager.Instance.PlayFXSound("Death_"+Random.Range(0,2));
        isDead = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (ContainEnemyTag(collision.collider.tag))
        {
            Vector2 reboundDir = (transform.position2D() - collision.transform.position2D()).normalized;

            int attackPower = GetEnemyPower(collision.collider);
            Rebound(reboundDir, attackPower * reboundPower);
            GetDamage(attackPower);
        }
    }

    int GetEnemyPower(Collider2D collider)
    {
        Enemy enemy = null;

        if (gameObject.CompareTag("Enemy"))
        {
            enemy = gameObject.GetComponent<Enemy>();
        }
        else if (collider.CompareTag("Enemy"))
        {
            enemy = collider.GetComponent<Enemy>();
        }

        return enemy == null ? 0 : enemy.attackPower;
    }

    protected bool ContainEnemyTag(string tag)
    {
        if (EnemyTags == null || EnemyTags.Length < 1)
            return false;

        return EnemyTags.Contains(tag);
    }

    public void Rebound(Vector2 direction, float power)
    {
        // todo : 충돌 시, 반대 방향으로 튕겨나간다.
        rb.AddForce(direction * power, ForceMode2D.Impulse);
    }
}

public interface IReboundable
{
    public void Rebound(Vector2 direction, float power);
}

public interface IAttackabale
{
    public void Attack();
}