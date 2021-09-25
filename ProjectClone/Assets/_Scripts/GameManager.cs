using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("스크립트 참조")]
    public GamePresenter gamePresenter;
    public MapGenerator mapGenerator;
    public WaveController waveControl;

    public Clone clone;

    [SerializeField]
    private Building[] building;
    public int index = 0;
    private List<Cell> grids;

    protected override void Awake()
    {
        grids = new List<Cell>();
        base.Awake();
        gamePresenter.Init();
        mapGenerator.Generator(this);
        clone.transform.position = (Vector3Int)mapGenerator.size / 2;

        if (waveControl != null)
            waveControl.Init();
    }

/*    private void Update()
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
*/
    public void AddCell(Cell building)
    {
        grids.Add(building);
    }
    public bool IsExist(Cell building,Vector2Int pos)
    {
        return grids.Any(p => !p.IsExist(building, pos)) || !building.patterns.All(p => mapGenerator.IsExist(p + pos));
    }
}
