using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private List<ICell> grids;


    protected override void Awake()
    {
        base.Awake();
        grids = new List<ICell>();
    }
    public bool IsExist(Vector2Int pos)
    {
        return grids.Any(p => p.IsExist(pos));
    }
}
