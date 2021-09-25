using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("스크립트 참조")]
    public GamePresenter gamePresenter;
    public MapGenerator mapGenerator;

    [SerializeField]
    private Building[] building;
    public int index = 0;
    private List<Cell> grids;

    public Stone[] stones;
    public Tree[] trees;
    protected override void Awake()
    {
        grids = new List<Cell>();
        base.Awake();
        gamePresenter.Init();
        mapGenerator.Generator();

        for (int i = 0; i < 20; i++)
        {
            var pos = new Vector2Int(Random.Range(0, mapGenerator.size.x), Random.Range(0, mapGenerator.size.y));
            var stonePrefab = stones[Random.Range(0, stones.Length)];
            if (!IsExist(stonePrefab, pos) && stonePrefab.patterns.All(p => mapGenerator.IsExist(p + pos)))
            {
                var stone  = Instantiate(stonePrefab, (Vector2)pos, Quaternion.identity);
                stone.position = pos;
                AddCell(stone);
            }
        }

        for (int i = 0; i < 20; i++)
        {
            var pos = new Vector2Int(Random.Range(0, mapGenerator.size.x), Random.Range(0, mapGenerator.size.y));
            var treePrefab = trees[Random.Range(0, trees.Length)];
            if (!IsExist(treePrefab, pos) && treePrefab.patterns.All(p => mapGenerator.IsExist(p + pos)))
            {
                var tree = Instantiate(treePrefab, (Vector2)pos, Quaternion.identity);
                tree.position = pos;
                AddCell(tree);
            }
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            var b = building[index];
            var pos = Utility.World2Grid(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if(!IsExist(b, pos)&& b.patterns.All(p => mapGenerator.IsExist(p + pos)))
            {
                var i = Instantiate(b, (Vector2)pos, Quaternion.identity);
                i.position = pos;
                AddCell(i);
            }
        }
    }

    private void AddCell(Cell building)
    {
        grids.Add(building);
    }
    public bool IsExist(Cell building,Vector2Int pos)
    {
        return grids.Any(p => !p.IsExist(building, pos));
    }
}
