using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using System;

public class GameShopContextItem : MonoBehaviour
{
    [SerializeField]
    private Image icon;

    [SerializeField]
    private Button button;


    public Sprite Icon
    {
        set => icon.sprite = value;
    }

    public IObservable<Unit> OnClick
    {
        get => button.OnClickAsObservable();
    }
}
