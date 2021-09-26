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
    public int attackPower = 1;
    public float attackSpeed = 1f;
    public float attackRadius = 3f;

    DateTime nextAttackTime;
    Vector2 moveDir;
    Vector3 leftScale = new Vector3(-1f, 1f, 1f);
    Vector3 rightScale = new Vector3(1f, 1f, 1f);

    Clone targetClone;
    Transform weapon;

    public int Rank = 1;
    EWeaponType weaponType;

    public override void Init()
    {
        base.Init();

        EnemyTags = new string[] { "Player" };

        nextAttackTime = DateTime.Now;

        weapon = root.GetChild(0);
        Dead();
    }

    protected override void Update()
    {
        if (targetClone == null)
            SetTarget(GameManager.Instance.clone);

        base.Update();
    }

    public override void Move()
    {
        if (targetClone == null)
            return;

        //if (IsAttackRange())
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

        Debug.Log("attack player");
        nextAttackTime = DateTime.Now.AddSeconds(attackSpeed);

        // todo : 투사체 발사
    }

    bool IsAttackRange()
    {
        if (targetClone == null)
            return false;

        return transform.position2D().IsInRange(targetClone.transform.position2D(), attackRadius);
    }

    public override void Dead()
    {
        // todo : 사망 처리
        animator.SetTrigger("Death");
        gameObject.tag = "Untagged";
        Destroy(this);
    }

    public void SetTarget(Clone clone)
    {
        targetClone = clone;
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = attackGizmoColor;
        Gizmos.DrawWireSphere(transform.position, attackRadius);

        if (targetClone != null)
        {
            Gizmos.color = moveGizmoColor;
            Gizmos.DrawRay(transform.position, targetClone.transform.position2D() - transform.position2D());
        }
    }
#endif
}
