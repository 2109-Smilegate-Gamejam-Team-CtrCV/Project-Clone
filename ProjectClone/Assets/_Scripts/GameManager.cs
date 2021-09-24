using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private List<ICell> grids;
    public Transform cell;

    protected override void Awake()
    {
        base.Awake();
        grids = new List<ICell>();
    }

    private void Update()
    {
        cell.position = (Vector3Int)Utility.World2Grid(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    public bool IsExist(Vector2Int pos)
    {
        return grids.Any(p => p.IsExist(pos));
    }
}
