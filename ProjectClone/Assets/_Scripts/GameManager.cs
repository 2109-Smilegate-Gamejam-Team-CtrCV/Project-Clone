using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : Singleton<GameManager>
{
    [Header("스크립트 참조")]
    public GamePresenter gamePresenter;
    public MapGenerator mapGenerator;
    public WaveController waveControl;

    public Clone clone;
    public Cell Building { get; internal set; }

    public int index = 0;
    private List<Cell> grids;

    [SerializeField]
    private bool isBuildingMode;


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

    private void Update()
    {
        if(Input.mouseScrollDelta.y > 0.7f)
        {
            index += 1;

        }
        if (Input.mouseScrollDelta.y < -0.7f)
        {
            index -= 1;

        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            SetBuildingMode(!isBuildingMode);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            if(!gamePresenter.gameSkillView.gameObject.activeSelf)
                gamePresenter.gameSkillView.Show();
            else
                gamePresenter.gameSkillView.HIde();

        }


        if (isBuildingMode)
        {
            var pos = Utility.World2Grid(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            preview.transform.position = (Vector3Int)pos;
            var b = Building;
            SpriteRenderer spriteRenderer = preview.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = b.GetComponent<SpriteRenderer>().sprite;
            if (!IsExist(b, pos) && b.patterns.All(p => mapGenerator.IsExist(p + pos)))
                spriteRenderer.color = new Color(0, 1, 0, 0.5f);

            else
                spriteRenderer.color = new Color(1, 0, 0, 0.5f);

            if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                if (!IsExist(b, pos) && b.patterns.All(p => mapGenerator.IsExist(p + pos)))
                {
                    var i = Instantiate(b, (Vector2)pos, Quaternion.identity);
                    i.position = pos;
                    AddCell(i);
                }
            }
            if (Input.GetMouseButtonUp(1) && !EventSystem.current.IsPointerOverGameObject())
            {
                SetBuildingMode(false);
            }
        }
    }

    public GameObject preview;


    public void SetBuildingMode(bool flag)
    {
        isBuildingMode = flag;
        preview.gameObject.SetActive(flag);
    }

    public void RemoveCell(Cell building)
    {
        grids.Remove(building);
    }

    public void AddCell(Cell building)
    {
        grids.Add(building);
    }
    public bool IsExist(Cell building, Vector2Int pos)
    {
        return grids.Any(p => !p.IsExist(building, pos)) || !building.patterns.All(p => mapGenerator.IsExist(p + pos));
    }
}
