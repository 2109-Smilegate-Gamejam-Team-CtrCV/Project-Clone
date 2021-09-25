using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Coord : IComparer<int>
{
    public int x;
    public int y;
    public Vector2 ToVector2
    {
        get
        {
            return new Vector2(x, y);
        }
    }

    public Coord(int dx, int dy)
    {
        x = dx;
        y = dy;
    }

    public int Compare(int dx, int dy)
    {
        if (x.CompareTo(dx) == 0)
        {
            return y.CompareTo(dy);
        }
        else
        {
            return x.CompareTo(dx);
        }
    }
}

public class Portal : Cell
{
#if UNITY_EDITOR
    [SerializeField] Color summonGizmoColor = Color.yellow;
#endif

    [SerializeField] Enemy[] enemyPrefabs;

    public int summonRadius = 10;

    HashSet<Coord> coordSet = new HashSet<Coord>();
    //HashSet<Vector2Int> v2iSet = new HashSet<Vector2Int>();

    public void StartWave(int count)
    {
        SummonEnemy(count);
    }

    public void SummonEnemy(int count)
    {
        for (int i = 0; i == count;)
        {
            var randomPos = transform.position2D() + Random.insideUnitCircle * summonRadius;
            Vector2Int v2i = Vector2Int.RoundToInt(randomPos);

            Coord summonCoord = new Coord(v2i.x, v2i.y);
            if (coordSet.Contains(summonCoord) == false)
            {
                coordSet.Add(summonCoord);
                ++i;
                CreateEnemy(summonCoord);
            }
        }
    }

    public void SummonEnemy_old(int count)
    {
        Vector2Int v2i = Vector2Int.RoundToInt(transform.position);
        
        int minX = Mathf.Clamp(v2i.x - summonRadius, 0, GameManager.Instance.mapGenerator.size.x);
        int maxX = Mathf.Clamp(v2i.x + summonRadius, 0, GameManager.Instance.mapGenerator.size.x);

        int minY = Mathf.Clamp(v2i.y - summonRadius, 0, GameManager.Instance.mapGenerator.size.y);
        int maxY = Mathf.Clamp(v2i.y + summonRadius, 0, GameManager.Instance.mapGenerator.size.y);

        for (int i = 0; i == count; )
        {
            Coord summonCoord = new Coord(Random.Range(minX, maxX), Random.Range(minY, maxY));
            if (coordSet.Contains(summonCoord) == false)
            {
                coordSet.Add(summonCoord);
                ++i;
                CreateEnemy(summonCoord);
            }
        } 
    }

    void CreateEnemy(Coord summonCoord)
    {
        Enemy enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Enemy enemy = Instantiate(enemyPrefab, summonCoord.ToVector2, Quaternion.identity);
        enemy.transform.position = summonCoord.ToVector2;
        enemy.Init();
    }

    //public void SummonEnemy()
    //{
    //    Vector2Int v2i = Vector2Int.RoundToInt(transform.position);
    //    int minX = Mathf.Clamp(v2i.x - summonRadius, 0, GameManager.Instance.mapGenerator.size.x);
    //    int maxX = Mathf.Clamp(v2i.x + summonRadius, 0, GameManager.Instance.mapGenerator.size.x);

    //    int minY = Mathf.Clamp(v2i.y - summonRadius, 0, GameManager.Instance.mapGenerator.size.y);
    //    int maxY = Mathf.Clamp(v2i.y + summonRadius, 0, GameManager.Instance.mapGenerator.size.y);

    //    var summonPos = new Vector2(Random.Range(minX,maxX), Random.Range(minY, maxY));
    //    var enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
    //    var enemy = Instantiate(enemyPrefab, summonPos, Quaternion.identity);
    //    enemy.transform.position = summonPos;


    //    //while (GameManager.Instance.IsExist(enemyPrefab, summonPos))
    //    //{
    //    //    summonPos = new Vector2Int(Random.Range(minX, maxX), Random.Range(minY, maxY));
    //    //}
    //}

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = summonGizmoColor;
        Gizmos.DrawWireSphere(transform.position, summonRadius);
    }
#endif
}
