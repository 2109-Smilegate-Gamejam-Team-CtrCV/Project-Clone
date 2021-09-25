using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProduceBuilding : Building
{
    [SerializeField]
    private float delayTimer = 0;
    private float timer = 0;



    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= delayTimer)
        {
            timer -= delayTimer;
            Give();
        }
    }

    private void Give()
    {
        Debug.Log("È¹µæ");
        GameManager.Instance.gamePresenter.gameModel.AddMineral(10);
    }

}
