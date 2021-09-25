using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceProduceBuilding : Building
{
    [SerializeField]
    private int experienceCreateValue;

    [SerializeField]
    private float experienceCreateDelay;

    [SerializeField]
    private float experienceCreateDelaySpace;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > experienceCreateDelay)
        {
            timer -= experienceCreateDelay;
            experienceCreateDelay += experienceCreateDelaySpace;
            GameManager.Instance.gamePresenter.gameModel.AddExperience(experienceCreateValue);
        }
    }
}
