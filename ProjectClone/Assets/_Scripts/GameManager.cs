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
    public CameraTargeter cameraTargeter;
    [SerializeField] Clone clonePrefab;
    public Clone clone;
    public BuildingItem Building { get; internal set; }

    public int index = 0;
    private List<Cell> grids;

    [SerializeField]
    private bool isBuildingMode;

    int playerNumber;


    protected override void Awake()
    {
        grids = new List<Cell>();
        playerNumber = 1;

        base.Awake();
        gamePresenter.Init();
        mapGenerator.Generator(this);
        CreateNextClone();

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
            gamePresenter.gameModel.AddOrganism(1000);
            gamePresenter.gameModel.AddMineral(1000);
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
            spriteRenderer.sprite = b.Icon;
            if (!IsExist(b.cell, pos) && b.cell.patterns.All(p => mapGenerator.IsExist(p + pos)))
                spriteRenderer.color = new Color(0, 1, 0, 0.5f);

            else
                spriteRenderer.color = new Color(1, 0, 0, 0.5f);

            if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                if (!IsExist(b.cell, pos) && b.cell.patterns.All(p => mapGenerator.IsExist(p + pos)))
                {
                    gamePresenter.gameModel.mineral.Value -= b.Mineral;
                    gamePresenter.gameModel.organism.Value -= b.Organism;
                    var i = Instantiate(b.cell, (Vector2)pos, Quaternion.identity);
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

    public void CreateNextClone()
    {
        // todo : 클론이 죽고 다음 클론 생성
        clone = CreateClone();
        cameraTargeter.target = clone.transform;
    }

    Clone CreateClone()
    {
        var pos = (Vector3Int)mapGenerator.size / 2;
        Clone newClone = Instantiate(clonePrefab, pos, Quaternion.identity);
        newClone.name = string.Format("Player_{0}", playerNumber++);
        newClone.Init();

        return newClone;
    }
}
