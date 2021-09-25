using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class GameShopContextItem : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]
    private Image icon;

    [SerializeField]
    private Button button;


    public BuildingItem item;
    public GameMainView gameMainView;


    public Sprite Icon
    {
        set => icon.sprite = value;
    }

    public IObservable<Unit> OnClick
    {
        get => button.OnClickAsObservable();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameMainView.toolTipGameObject.gameObject.SetActive(true);
        gameMainView.toolTipGameObject.nameText.text = item.Name;
        gameMainView.toolTipGameObject.descriptionText.text = item.Description;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameMainView.toolTipGameObject.gameObject.SetActive(false);
    }
}
