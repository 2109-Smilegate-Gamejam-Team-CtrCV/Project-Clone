using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    [SerializeField] List<Enemy> enemyPrefabs = new List<Enemy>();

    public int summonRadius = 10;

    HashSet<Coord> coordSet = new HashSet<Coord>();
    //HashSet<Vector2Int> v2iSet = new HashSet<Vector2Int>();

    public List<Enemy> EnemyList { get { return enemyList; } }
    List<Enemy> enemyList = new List<Enemy>();

    public void StartWave(int[] enemySummonCounts)
    {
        SummonEnemy(enemySummonCounts);
    }

    public void ResetGame()
    {
        if (enemyList.Count > 0)
        {
            for (int i = 0; i < enemyList.Count; ++i)
            {
                Destroy(enemyList[i].gameObject);
            }

            enemyList.Clear();
        }

        coordSet.Clear();
    }

    public void SummonEnemy(int[] enemySummonCounts)
    {
        for (int rank = 1; rank <= enemySummonCounts.Length; ++rank)
        {
            int count = enemySummonCounts[rank - 1];

            for (int i = 0; i != count;)
            {
                var randomPos = transform.position2D() + Random.insideUnitCircle * summonRadius;
                Vector2Int v2i = Vector2Int.RoundToInt(randomPos);

                Coord summonCoord = new Coord(v2i.x, v2i.y);
                if (coordSet.Contains(summonCoord) == false)
                {
                    Enemy enemy = CreateEnemy(rank, summonCoord);
                    if (enemy != null)
                    {
                        enemyList.Add(enemy);
                        coordSet.Add(summonCoord);
                        ++i;
                    }
                }
            }
        }
    }

    //public void SummonEnemy_old(int count)
    //{
    //    Vector2Int v2i = Vector2Int.RoundToInt(transform.position);
        
    //    int minX = Mathf.Clamp(v2i.x - summonRadius, 0, GameManager.Instance.mapGenerator.size.x);
    //    int maxX = Mathf.Clamp(v2i.x + summonRadius, 0, GameManager.Instance.mapGenerator.size.x);

    //    int minY = Mathf.Clamp(v2i.y - summonRadius, 0, GameManager.Instance.mapGenerator.size.y);
    //    int maxY = Mathf.Clamp(v2i.y + summonRadius, 0, GameManager.Instance.mapGenerator.size.y);

    //    for (int i = 0; i == count; )
    //    {
    //        Coord summonCoord = new Coord(Random.Range(minX, maxX), Random.Range(minY, maxY));
    //        if (coordSet.Contains(summonCoord) == false)
    //        {
    //            coordSet.Add(summonCoord);
    //            ++i;
    //            Enemy enemy = CreateEnemy(0, summonCoord);
    //            enemyList.Add(enemy);
    //        }
    //    } 
    //}

    Enemy CreateEnemy(int rank, Coord summonCoord)
    {
        List<Enemy> targetList = enemyPrefabs.FindAll(e => e.Rank == rank);
        int index = Random.Range(0, targetList.Count);

        Enemy enemyPrefab = targetList[index];
        if (enemyPrefab == null)
        {
            Debug.LogErrorFormat("Can not Find Correct Enemy. Rank : {0}", rank);
            return null;
        }

        Enemy enemy = Instantiate(enemyPrefab, summonCoord.ToVector2, Quaternion.identity, transform);
        enemy.transform.position = summonCoord.ToVector2;
        enemy.Init();

        //Debug.LogFormat("Summon Enemy. Name {0}, Rank : {1}", enemy.name, rank);
        return enemy;
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
    void OnDrawGizmosSelected()
    {
        Gizmos.color = summonGizmoColor;
        Gizmos.DrawWireSphere(transform.position, summonRadius);
    }
#endif
}
