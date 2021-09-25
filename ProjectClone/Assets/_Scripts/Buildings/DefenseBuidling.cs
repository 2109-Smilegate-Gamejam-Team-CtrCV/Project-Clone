using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseBuidling : Building
{
    [SerializeField]
    private float attackDelay;

    [SerializeField]
    private float attackRange;

    [SerializeField]
    private int attackDamage;

    [SerializeField]
    private float attackSpeed;

    [SerializeField]
    private Bullet bullet;

    private float timer = 0;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= attackDelay)
        {
            timer -= attackDelay;

            var b = Instantiate(bullet, transform.position, Quaternion.identity);
            b.velocity = attackSpeed * Vector2.up;
            b.damage = attackDamage;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1,0.5f,0);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
#endif
}
