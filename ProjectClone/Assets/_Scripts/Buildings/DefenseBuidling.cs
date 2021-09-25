using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            var enemys = Physics2D.OverlapCircleAll(transform.position, attackRange)
                .Where(p => p.gameObject.CompareTag("Enemy"));

            if (enemys.Count() > 0)
            {
                var enemy = enemys.OrderBy(p => Vector2.SqrMagnitude(p.transform.position - transform.position)).FirstOrDefault();
                var b = Instantiate(bullet, transform.position, Quaternion.identity);
                b.velocity = attackSpeed * (enemy.transform.position - transform.position).normalized;
                b.damage = attackDamage;
            }
        }
    }

#if UNITY_EDITOR
    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = new Color(1,0.5f,0);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
#endif
}
