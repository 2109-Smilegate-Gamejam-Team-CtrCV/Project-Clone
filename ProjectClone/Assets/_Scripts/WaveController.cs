using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField] Portal portalPrefab;

    public int waveLevel = 0;
    public float waveDelayTime = 15f;
    public float delayIncreaseTime = 1.5f;

    System.DateTime nextWaveTime;

    MapGenerator generator;

    public void Init()
    {
        generator = GameManager.Instance.mapGenerator;
        nextWaveTime = System.DateTime.Now;
    }

    public float GetNextWaveTime()
    {
        return waveDelayTime + waveLevel * delayIncreaseTime;
    }

    public void GetCenterPos()
    {
        Vector3 centor = (Vector3Int)generator.size / 2;
    }

    public void CreatePortal()
    {
        var portalPos = new Vector2Int(Random.Range(0, generator.size.x), Random.Range(0, generator.size.y));
        while (GameManager.Instance.IsExist(portalPrefab, portalPos))
        {
            portalPos = new Vector2Int(Random.Range(0, generator.size.x), Random.Range(0, generator.size.y));
        }

        var portal = Instantiate(portalPrefab, (Vector2)portalPos, Quaternion.identity);
        portal.position = portalPos;


        var otherPos = -portalPos;
        while (GameManager.Instance.IsExist(portalPrefab, otherPos))
        {
            otherPos = new Vector2Int(Random.Range(0, generator.size.x), Random.Range(0, generator.size.y));
        }

        portal = Instantiate(portalPrefab, (Vector2)otherPos, Quaternion.identity);
        portal.position = otherPos;
    }

    //public void CreatePortal2()
    //{
    //    var playerPos = GameManager.Instance.clone.transform.position;
    //    Vector2Int v2i = Vector2Int.RoundToInt(playerPos);

    //    int minX = Mathf.Clamp(v2i.x - createDistance, 0, generator.size.x);
    //    int maxX = Mathf.Clamp(v2i.x + createDistance, 0, generator.size.x);

    //    int minY = Mathf.Clamp(v2i.y - createDistance, 0, generator.size.x);
    //    int maxY = Mathf.Clamp(v2i.y + createDistance, 0, generator.size.x);


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
