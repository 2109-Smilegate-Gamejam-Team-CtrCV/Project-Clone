using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProduceBuilding : Building
{
    public enum ProduceItemType
    {
        Mineral,
        Organism,
        Both
    }

    [SerializeField]
    private float delayTimer = 0;

    [SerializeField]
    private ProduceItemType produceItemType;

    [SerializeField]
    private int itemCount;

    private float timer = 0;



    private void Update()
    {
        if(isCreate)
        {
            timer += Time.deltaTime;
            if (timer >= delayTimer)
            {
                timer -= delayTimer;
                Give();
            }
        }
    }

    private void Give()
    {
        Debug.Log("È¹µæ");

        transform.DOScaleY(1, 0.5f).From(1.2f);
        if (produceItemType == ProduceItemType.Mineral)
        {
            GameManager.Instance.gamePresenter.gameModel.AddMineral(itemCount);
        }
        else if (produceItemType == ProduceItemType.Organism)
        {
            GameManager.Instance.gamePresenter.gameModel.AddOrganism(itemCount);
        }
        else if (produceItemType == ProduceItemType.Both)
        {
            GameManager.Instance.gamePresenter.gameModel.AddOrganism(itemCount);
            GameManager.Instance.gamePresenter.gameModel.AddMineral(itemCount);
        }



    }

}
