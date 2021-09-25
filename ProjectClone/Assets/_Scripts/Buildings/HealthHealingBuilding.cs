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
        timer += Time.deltaTime;
        if (timer > healthHealDelay)
        {
            timer -= healthHealDelay;
            healthHealDelay += healthHealDelaySpace;
            GameManager.Instance.gamePresenter.gameModel.HeartHeal(1);
        }
    }
}
