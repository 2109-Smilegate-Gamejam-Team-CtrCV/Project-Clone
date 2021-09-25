using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Building : Cell
{
    [Header("°Ç¼³")]
    public int createCount;
    public int createCounter;
    public bool isCreate;


    private void Start()
    {

        GetComponent<SpriteRenderer>().material.SetFloat("_Gray", 0);

        GetComponent<SpriteRenderer>().color = Color.gray;
    }

    internal void AddCount()
    {
        if(!isCreate)
        {
            createCounter += 1;
            GetComponent<SpriteRenderer>().material.DOFloat(createCounter / (float)createCount, "_Gray", 0.25f);

            transform.DOScaleY(1, 0.5f).From(1.2f);
            if(createCounter >= createCount)
            {
                GetComponent<SpriteRenderer>().color = Color.white;
                isCreate = true;
            }
        }
    }
}
