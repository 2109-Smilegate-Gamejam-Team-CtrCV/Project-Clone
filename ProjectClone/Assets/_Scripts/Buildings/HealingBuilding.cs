using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingBuilding : Building
{
    [SerializeField]
    private float radius;

#if UNITY_EDITOR
    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,radius);
    }
#endif
}
