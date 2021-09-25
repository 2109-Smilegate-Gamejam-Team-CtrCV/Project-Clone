using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private Building[] building;
    public GamePresenter gamePresenter;
    public int index = 0;
    private List<Building> grids;

    protected override void Awake()
    {
        base.Awake();
        gamePresenter.Init();
        grids = new List<Building>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            var b = building[index];
            var pos = Utility.World2Grid(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if(!IsExist(b, pos))
            {
                var i = Instantiate(b, (Vector2)pos, Quaternion.identity);
                i.position = pos;
                AddBuilding(i);
            }
        }
    }

    private void AddBuilding(Building building)
    {
        grids.Add(building);
    }
    public bool IsExist(Building building,Vector2Int pos)
    {
        return grids.Any(p => !p.IsExist(building,pos));
    }
}
