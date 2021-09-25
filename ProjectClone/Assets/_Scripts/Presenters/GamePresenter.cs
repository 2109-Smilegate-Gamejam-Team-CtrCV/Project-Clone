using System;
using UniRx;
using UnityEngine;

public class GamePresenter : MonoBehaviour
{
    [SerializeField]
    private GameMainView gameMainView;

    [SerializeField]
    private BuildShopList[] buildShopList;

    public GameModel gameModel;
    public void Init()
    {
        gameModel = new GameModel();

        gameModel.heart.Subscribe(value => gameMainView.SetHeart(value));
        gameModel.mineral.Subscribe(value => gameMainView.MineralText = value.ToString());
        gameModel.organism.Subscribe(value => gameMainView.OrganismText = value.ToString());
        gameMainView.ShopTapChanged.Subscribe(p => gameMainView.SetValueChanged(buildShopList[p].buildingItems));
        foreach (var item in buildShopList)
        {
            gameMainView.AddShopHeader(item.Name);
        }
    }
}
[Serializable]
public struct BuildShopList
{
    public string Name;
    public BuildingItem[] buildingItems;
}