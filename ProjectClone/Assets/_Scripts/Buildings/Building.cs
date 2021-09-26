using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Building : Cell
{
    [Header("°Ç¼³")]
    public float createCount;
    private float createCounter;
    public bool isCreate;


    private void Start()
    {
        createCounter = 0;
        GetComponent<SpriteRenderer>().material.SetFloat("_Gray", 0);

        GetComponent<SpriteRenderer>().color = Color.gray;
    }

    internal void AddCount(float damage)
    {
        if(!isCreate)
        {
            createCounter += damage;
            GetComponent<SpriteRenderer>().material.DOFloat(createCounter / createCount, "_Gray", 0.25f);

            SoundManager.Instance.PlayFXSound("Hammer");
            transform.DOScaleY(1, 0.5f).From(1.2f);
            if(createCounter >= createCount)
            {
                GetComponent<SpriteRenderer>().color = Color.white;
                isCreate = true;
            }
        }
    }
}
