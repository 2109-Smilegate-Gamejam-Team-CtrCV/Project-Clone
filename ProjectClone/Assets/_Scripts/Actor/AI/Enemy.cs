using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EWeaponType
{
    Hand,
    Sword,
    Bow,
    Wand
}

public class Enemy : Actor, IAttackabale
{
#if UNITY_EDITOR
    [SerializeField] Color moveGizmoColor = Color.cyan;
    [SerializeField] Color attackGizmoColor = Color.red;
#endif
    [SerializeField] Bullet bulletPrefab;

    public int attackPower = 1;
    public float attackSpeed = 1f;
    public float attackCoolTime = 1f;
    public float attackRadius = 3f;

    DateTime nextAttackTime;
    Vector2 moveDir;
    Vector3 leftScale = new Vector3(-1f, 1f, 1f);
    Vector3 rightScale = new Vector3(1f, 1f, 1f);

    Clone targetClone;
    Transform weapon;

    public int Rank = 1;
    public EWeaponType weaponType;
    public Transform health;
    public bool isBulletAttack 
    { 
        get 
        {
            return weaponType == EWeaponType.Bow || weaponType == EWeaponType.Wand; 
        } 
    }

    public override void Init()
    {
        base.Init();

        EnemyTags = new string[] { "Player" };

        nextAttackTime = DateTime.Now;

        weapon = root.GetChild(0);
    }
    public override bool GetDamage(int power)
    {
        HP = Mathf.Clamp(HP - power, 0, maxHP);
        health.transform.localScale = new Vector3(HP / (float)maxHP, 1, 1);

        if (HP <= 0)
        {
            Dead();
        }

        return HP <= 0;
    }
    protected override void Update()
    {
        if (isDead)
            return;

        if (targetClone == null)
            SetTarget(GameManager.Instance.clone);

        base.Update();

        //RotateWeapon();

        if (isBulletAttack)
            Attack();
    }

    public override void Move()
    {
        if (targetClone == null)
            return;

        //if (isBulletAttack && IsAttackRange())
        //    return;

        moveDir = (targetClone.transform.position2D() - transform.position2D()).normalized;
        transform.Translate(moveDir * moveSpeed * Time.deltaTime);
        SetMoveAnimation(moveDir);
    }

    public void SetMoveAnimation(Vector2 moveDir)
    {
        if (moveDir.x != 0)
            root.localScale = moveDir.x > 0 ? leftScale : rightScale;
    }

    public void Attack()
    {
        if (nextAttackTime.IsEnoughTime() == false || IsAttackRange() == false)
            return;

        nextAttackTime = DateTime.Now.AddSeconds(attackCoolTime);

        // todo : 투사체 발사
        Bullet bullet = Instantiate(bulletPrefab, weapon.position, Quaternion.identity);
        Debug.Log(bullet);
        bullet.isPlayer = false;
        bullet.velocity = attackSpeed * (targetClone.transform.position - weapon.position).normalized;
        bullet.damage = attackPower;
        bullet.lifeTime = attackRadius / attackSpeed;
    }

    public float ___offset;

    void RotateWeapon()
    {
        float dy = targetClone.transform.position.y - weapon.position.y;
        float dx = targetClone.transform.position.x - weapon.position.x;
        float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        weapon.rotation = Quaternion.Euler(0f, 0f, angle + ___offset); //+ Quaternion.Euler(0, 0, -40f); // offset
        //weapon.rotation = Quaternion.AngleAxis(angle + 45f, Vector3.forward); //+ Quaternion.Euler(0, 0, -40f); // offset
    }

    bool IsAttackRange()
    {
        if (targetClone == null)
            return false;

        return transform.position2D().IsInRange(targetClone.transform.position2D(), attackRadius);
    }

    public override void Dead()
    {
        if (isDead)
            return;

        base.Dead();
        // todo : 사망 처리
        animator.SetTrigger("Death");
        gameObject.tag = "Untagged";

        //Destroy(this); // 스크립트 바로 제거
        Destroy(gameObject, 30f); // 사망 애니메이션 30초 후 제거
    }

    public void SetTarget(Clone clone)
    {
        targetClone = clone;
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = attackGizmoColor;
        Gizmos.DrawWireSphere(transform.position, attackRadius);

        if (targetClone != null)
        {
            Gizmos.color = moveGizmoColor;
            Gizmos.DrawRay(weapon.position, targetClone.transform.position2D() - transform.position2D());
        }
    }
#endif
}
