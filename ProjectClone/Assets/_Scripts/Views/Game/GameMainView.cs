using System;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class GameMainView : MonoBehaviour
{
    [SerializeField]
    private Transform heartTransform;

    [SerializeField]
    private GameObject heartUI;


    [SerializeField]
    private Image metalGauge;

    [SerializeField]
    private TextMeshProUGUI mineralText;

    [SerializeField]
    private TextMeshProUGUI organismText;



    [Header("Shop")]

    [SerializeField]
    private Transform shopHeaderTransform;
    [SerializeField]
    private Transform shopContextTransform;

    [SerializeField]
    private GameObject shopHeader;

    [SerializeField]
    private GameShopContextItem shopContextItem;



    private Subject<int> subject = new Subject<int>();


    private IObservable<int> observable;

    public float MetalGauge
    {
        set => metalGauge.fillAmount = value;
    }

    public string MineralText
    {
        set => mineralText.text = $"���� : {value}";
    }

    public string OrganismText
    {
        set => organismText.text = $"����ü : {value}";
    }
    public IObservable<int> ShopTapChanged
    {
        get => subject;
    }

    public void SetHeart(int heart)
    {
        foreach (Transform item in heartTransform)
        {
            Destroy(item.gameObject);
        }

        for (int i = 0; i < heart; i++)
        {
            Instantiate(heartUI, Vector3.zero, Quaternion.identity, heartTransform);
        }
    }
    public void AddShopHeader(string name)
    {
        var childIndex = shopHeaderTransform.childCount;
        var item = Instantiate(shopHeader, Vector3.zero, Quaternion.identity, shopHeaderTransform);
        item.transform.localPosition = Vector3.zero;
        Toggle toggle = item.GetComponentInChildren<Toggle>();
        toggle.group = shopHeaderTransform.GetComponentInChildren<ToggleGroup>();
        toggle.OnValueChangedAsObservable().Subscribe(_ => subject.OnNext(childIndex));
        item.GetComponentInChildren<TextMeshProUGUI>().text = name;

    }
    public void SetValueChanged(BuildingItem[] buildingItems)
    {
        for (int i = shopContextTransform.childCount - 1; i >= 0; i--)  
        {
            Destroy(shopContextTransform.GetChild(i).gameObject);
        }
        foreach (var item in buildingItems)
        {
            var contextItem = Instantiate(shopContextItem, shopContextTransform);
            contextItem.Icon = item.Icon;
            contextItem.OnClick.Subscribe(_ =>
            {
                GameManager.Instance.Building = item.cell;
                GameManager.Instance.SetBuildingMode(true);
            });
            contextItem.transform.localPosition = Vector3.zero;
        }
    }
}
