using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator: MonoBehaviour
{
    public float cut;
    public SpriteRenderer spriteRenderer;
    public Tilemap tilemap;
    public Tilemap tilemap1;
    public TileBase grass;
    public TileBase water;
    public TileBase random;
    public Vector2Int size;

    [Header("초기 스폰")]
    public Vector2Int spawnSize;

    public float[] array;
    public Dictionary<Vector2Int, bool> exist;

    public int treeCount;
    public int stoneCount;
    public Stone[] stones;
    public Tree[] trees;
    public bool IsExist(Vector2Int pos)
    {
        return exist.ContainsKey(pos) && exist[pos];
    }
    public int minSize;
    public void Generator(GameManager gameManager)
    {
        exist = new Dictionary<Vector2Int, bool>();
        array = new float[size.x * size.y];
        FastNoise fastNoise= new FastNoise();
        fastNoise.SetFrequency(0.11f);
        fastNoise.SetFractalLacunarity(0);
        fastNoise.SetNoiseType(FastNoise.NoiseType.Cellular);
        fastNoise.SetCellularJitter(1.5f);
        fastNoise.SetFractalType(FastNoise.FractalType.Billow);
        fastNoise.SetCellularDistanceFunction(FastNoise.CellularDistanceFunction.Manhattan);
        fastNoise.SetCellularReturnType(FastNoise.CellularReturnType.CellValue);
        var center = new Vector2Int(size.x / 2, size.y / 2);

        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {

                array[y*size.x+x] = (fastNoise.GetValue(y, x) + 2) / 3;
                if ((x-center.x)*(x- center.x) + (y - center.y) * (y - center.y) < 300) 
                    array[y * size.x + x] = 1;


                bool v = (array[y * size.x + x] > cut);
                exist.Add(new Vector2Int(x, y), v);
            }
        }
        Vector2Int[] dir = new Vector2Int[]
        {

            new Vector2Int(1,0),
            new Vector2Int(-1,0),
            new Vector2Int(0,1),
            new Vector2Int(0,-1),
        };
        bool[] e = new bool[size.x * size.y];
        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {

                if (!exist[new Vector2Int(x,y)]) continue;

                List<Vector2Int> list = new List<Vector2Int>();
                Queue<Vector2Int> queue = new Queue<Vector2Int>();
                queue.Enqueue(new Vector2Int() { x=x, y=y });
                while (queue.Count > 0)
                {
                    var pos = queue.Dequeue();
                    foreach (var item in dir)
                    {
                        var newPos = item + pos;
                        if (newPos.x >= 0 && newPos.y >= 0 && newPos.x < size.x && newPos.y < size.y && !e[newPos.y * size.x + newPos.x] && exist[newPos])
                        {
                            e[newPos.y * size.x + newPos.x] = true;
                            list.Add(newPos);
                            queue.Enqueue(newPos);
                        }
                    }
                }

                if(list.Count < minSize)
                {
                    foreach (var item in list)
                    {
                        array[item.y * size.x + item.x] = 0;
                        exist[item] = false;
                    }
                }
            }
        }

        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                float value = array[y * size.x + x];
                var v = exist[new Vector2Int(x, y)];
                tilemap.SetTile(new Vector3Int(x, y, 0), v ? grass : water);

                if (value > cut && Random.Range(0, 100.0f) < 1.0f)
                {
                    tilemap1.SetTile(new Vector3Int(x, y, 0), random);
                }
            }
        }

        for (int i = 0; i < stoneCount; i++)
        {
            var pos = new Vector2Int(Random.Range(0, size.x), Random.Range(0, size.y));
            var stonePrefab = stones[Random.Range(0, stones.Length)];
            if (!gameManager.IsExist(stonePrefab, pos) )
            {
                var stone = Instantiate(stonePrefab, (Vector2)pos, Quaternion.identity);
                stone.position = pos;
                gameManager.AddCell(stone);
            }
        }

        for (int i = 0; i < treeCount; i++)
        {
            var pos = new Vector2Int(Random.Range(0, size.x), Random.Range(0, size.y));
            var treePrefab = trees[Random.Range(0, trees.Length)];
            if (!gameManager.IsExist(treePrefab, pos) && (pos.x - center.x) * (pos.x - center.x) + (pos.y - center.y) * (pos.y - center.y) >= 6*6)
            {
                var tree = Instantiate(treePrefab, (Vector2)pos, Quaternion.identity);
                tree.position = pos;
                gameManager.AddCell(tree);
            }
        }


    }


}
