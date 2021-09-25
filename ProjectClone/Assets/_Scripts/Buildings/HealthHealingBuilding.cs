using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHealingBuilding : Building
{
    [SerializeField]
    private float range;

    [SerializeField]
    private float healthHealDelay;

    [SerializeField]
    private float healthHealDelaySpace;

    private float timer;

    private void Update()
    {
        if(isCreate)
        {
            timer += Time.deltaTime;
            if (timer > healthHealDelay)
            {
                timer -= healthHealDelay;
                healthHealDelay += healthHealDelaySpace;
                GameManager.Instance.gamePresenter.gameModel.HeartHeal(1);
            }
        }
    }
#if UNITY_EDITOR
    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = new Color(1, 0.56f, 0);
        Gizmos.DrawWireSphere(transform.position, range);
    }

#endif
}
