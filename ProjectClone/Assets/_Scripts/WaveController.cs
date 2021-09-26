using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField] Portal portalPrefab;

    public int waveLevel = 0;
    public bool isMonsterCarnival { get { return waveLevel % 5 == 0; } }
    public bool isAddPortal { get { return waveLevel % 20 == 0; } }
    public float waveReadyTime = 60f;
    public float waveDelayTime = 15f;
    public float delayIncreaseTime = 1.5f;
    public int portalRadius = 35; // 포탈 생성 반경

    System.DateTime nextWaveTime;

    MapGenerator generator;
    List<Portal> portalList = new List<Portal>();
    bool isInit = false;

    public void Init()
    {
        isInit = true;
        generator = GameManager.Instance.mapGenerator;
        ResetGame();
    }

    public void ResetGame()
    {
        waveLevel = 0;
        nextWaveTime = System.DateTime.Now.AddSeconds(waveReadyTime);

        if (portalList.Count > 0)
        {
            for (int i = 0; i < portalList.Count; ++i)
            {
                portalList[i].ResetGame();
                Destroy(portalList[i].gameObject);
            }

            portalList.Clear();
        }

        Portal portal = CreatePortal();
        portalList.Add(portal);

        portal = CreatePortal();
        portalList.Add(portal);

        //Portal anotherPortal = CreateOtherPortal(portal.position);
        //portalList.Add(anotherPortal);
    }

    void Update()
    {
        if (isInit == false)
            return;

        if (nextWaveTime.IsEnoughTime())
        {
            nextWaveTime = System.DateTime.Now.AddSeconds(GetNextWaveTime());

            waveLevel++;
            int[] enemySummonCounts = GetSummonMonsterCount();

            if (isAddPortal)
            {
                Portal portal = CreatePortal();
                portalList.Add(portal);
            }

            for (int i = 0; i < portalList.Count; ++i)
            {
                portalList[i].StartWave(enemySummonCounts);
            }
        }
    }

    public float GetNextWaveTime()
    {
        return waveDelayTime + waveLevel * delayIncreaseTime;
    }

    public int[] GetSummonMonsterCount()
    {
        int[] rankCounts = new int[4];
        if (isMonsterCarnival) // 몬스터 카니발
        {
            rankCounts[0] = waveLevel + waveLevel;
            rankCounts[1] = (waveLevel / 3) + (waveLevel / 5);
            rankCounts[2] = (waveLevel / 8) + (waveLevel / 10);
            rankCounts[3] = waveLevel / 15;
        }
        else
        {
            rankCounts[0] = waveLevel;
            rankCounts[1] = waveLevel / 3;
            rankCounts[2] = waveLevel / 8;
            rankCounts[3] = 0;
        }

        //Debug.LogFormat("Wave Level : {4} -> 1등급 : {0} 마리, 2등급 : {1} 마리, 3등급 : {2} 마리, 4등급 : {3} 마리", rankCounts[0], rankCounts[1], rankCounts[2], rankCounts[3], waveLevel);
        return rankCounts;
    }

    public Vector3 GetMapCenterPosition()
    {
        return (Vector3Int)generator.size / 2;
    }

    public Portal CreateRandomPortal()
    {
        var portalPos = new Vector2Int(Random.Range(0, generator.size.x), Random.Range(0, generator.size.y));
        while (GameManager.Instance.IsExist(portalPrefab, portalPos))
        {
            portalPos = new Vector2Int(Random.Range(0, generator.size.x), Random.Range(0, generator.size.y));
        }

        var portal = Instantiate(portalPrefab, (Vector2)portalPos, Quaternion.identity, transform);
        portal.position = portalPos;
        return portal;
    }

    public Portal CreateOtherPortal(Vector2Int portalPos)
    {
        var otherPos = -portalPos;
        while (GameManager.Instance.IsExist(portalPrefab, otherPos))
        {
            otherPos = new Vector2Int(Random.Range(0, generator.size.x), Random.Range(0, generator.size.y));
        }

        var portal = Instantiate(portalPrefab, (Vector2)otherPos, Quaternion.identity, transform);
        portal.position = otherPos;
        return portal;
    
    }

    public Vector2Int GetPortalPosition()
    {
        int degree = Random.Range(0, 360);
        float radian = degree * Mathf.Deg2Rad;

        float float_x = Mathf.Cos(radian) * portalRadius;
        float float_y = Mathf.Sin(radian) * portalRadius;

        int int_x = Mathf.RoundToInt(float_x); 
        int int_y = Mathf.RoundToInt(float_y); 

        Vector2Int v2i = new Vector2Int(int_x, int_y);
        v2i += generator.size / 2;

        return v2i;
    }

    public Portal CreatePortal()
    {
        var portalPos = GetPortalPosition();
        while (GameManager.Instance.IsExist(portalPrefab, portalPos))
        {
            portalPos = GetPortalPosition();
        }

        var portal = Instantiate(portalPrefab, (Vector2)portalPos, Quaternion.identity, transform);
        portal.position = portalPos;
        return portal;
    }

    //public void CreatePortal_backup()
    //{
    //    var playerPos = GameManager.Instance.clone.transform.position;
    //    Vector2Int v2i = Vector2Int.RoundToInt(playerPos);

    //    int minX = Mathf.Clamp(v2i.x - portalRadius, 0, generator.size.x);
    //    int maxX = Mathf.Clamp(v2i.x + portalRadius, 0, generator.size.x);

    //    int minY = Mathf.Clamp(v2i.y - portalRadius, 0, generator.size.x);
    //    int maxY = Mathf.Clamp(v2i.y + portalRadius, 0, generator.size.x);

    //    var portalPos = new Vector2Int(Random.Range(minX, maxX), Random.Range(minY, maxY));
    //    while (GameManager.Instance.IsExist(portalPrefab, portalPos))
    //    {
    //        portalPos = new Vector2Int(Random.Range(minX, maxX), Random.Range(minY, maxY));
    //    }

    //    var portal = Instantiate(portalPrefab, (Vector2)portalPos, Quaternion.identity);
    //    portal.position = portalPos;


    //    var otherPos = -portalPos;
    //    while (GameManager.Instance.IsExist(portalPrefab, otherPos))
    //    {
    //        otherPos = new Vector2Int(Random.Range(minX, maxX), Random.Range(minY, maxY));
    //    }

    //    portal = Instantiate(portalPrefab, (Vector2)otherPos, Quaternion.identity);
    //    portal.position = otherPos;
    //}
}
