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
    private Sprite[] shopHeaderSprite;

    [SerializeField]
    public ShopToolTip toolTipGameObject;
    
    [SerializeField]
    private GameShopContextItem shopContextItem;



    private Subject<int> subject = new Subject<int>();


    public float MetalGauge
    {
        set => metalGauge.fillAmount = value;
    }

    public string MineralText
    {
        set => mineralText.text = $"±§π∞ : {value}";
    }

    public string OrganismText
    {
        set => organismText.text = $"¿Ø±‚√º : {value}";
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
        item.GetComponent<Image>().sprite = shopHeaderSprite[childIndex];
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
            contextItem.gameMainView = this;
            contextItem.item = item;
            contextItem.Icon = item.Icon;
            contextItem.OnClick.Subscribe(_ =>
            {
                if(GameManager.Instance.gamePresenter.gameModel.organism.Value >= item.Organism && GameManager.Instance.gamePresenter.gameModel.organism.Value >= item.Organism)
                {
                    GameManager.Instance.Building = item;
                    GameManager.Instance.SetBuildingMode(true);
                }
                else
                {
                    GameManager.Instance.SetBuildingMode(false);
                }
            });
            contextItem.transform.localPosition = Vector3.zero;
        }
    }
}
