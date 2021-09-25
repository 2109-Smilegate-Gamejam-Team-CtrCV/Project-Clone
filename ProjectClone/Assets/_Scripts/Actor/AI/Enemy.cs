using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    Clone targetClone;

    public override void Init()
    {
        base.Init();

        EnemyTags = new string[] { "Player" };

        nextAttackTime = DateTime.Now;

        SetTarget(GameManager.Instance.clone);
    }

    public override void Move()
    {
        if (targetClone == null)
            return;

        //if (IsAttackRange())
        //    return;

        moveDir = (targetClone.transform.position2D() - transform.position2D()).normalized;
        transform.Translate(moveDir * moveSpeed * Time.deltaTime);
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
        Destroy(gameObject);
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
