using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator: MonoBehaviour
{
    public Texture2D texture;
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


    public bool IsExist(Vector2Int pos)
    {
        return exist.ContainsKey(pos) && exist[pos];
    }

    public void Generator()
    {
        exist = new Dictionary<Vector2Int, bool>();
        array = new float[size.x * size.y];
        texture = new Texture2D(size.x, size.y, TextureFormat.RGBA32, false);
        FastNoise fastNoise= new FastNoise();
        fastNoise.SetFrequency(0.12f);
        fastNoise.SetFractalLacunarity(0);
        fastNoise.SetNoiseType(FastNoise.NoiseType.Cellular);
        fastNoise.SetFractalType(FastNoise.FractalType.Billow);
        fastNoise.SetCellularDistanceFunction(FastNoise.CellularDistanceFunction.Manhattan);
        fastNoise.SetCellularReturnType(FastNoise.CellularReturnType.CellValue);
        var pixels = texture.GetPixels();

        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {

                array[y*size.x+x] = (fastNoise.GetValue(y, x) + 2) / 3;
                var center = new Vector2Int(size.x / 2, size.y / 2);
                if ((x-center.x)*(x- center.x) + (y - center.y) * (y - center.y) < 300) 
                    array[y * size.x + x] = 1;
            }
        }

        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                float value = array[y * size.x + x];
                pixels[y * size.x + x] = (value > cut) ? Color.green : Color.blue;
                bool v = (value > cut);
                tilemap.SetTile(new Vector3Int(x, y, 0), v ? grass : water);

                exist.Add(new Vector2Int(x, y), v);
                if (value > cut && Random.Range(0, 100.0f) < 3.0f)
                {
                    tilemap1.SetTile(new Vector3Int(x, y, 0), random);
                }
            }
        }


      

        texture.SetPixels(pixels);
        texture.Apply();
        spriteRenderer.sprite = Sprite.Create(texture, new Rect(0, 0, size.x, size.y), Vector2.one / 2);

    }


}
