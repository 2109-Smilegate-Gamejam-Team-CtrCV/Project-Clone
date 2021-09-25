using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NewBehaviourScript : MonoBehaviour
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

    [Header("�ʱ� ����")]
    public Vector2Int spawnSize;

    public float[] array;
    void Start()
    {
        array = new float[size.x * size.y];
        texture = new Texture2D(size.x, size.y, TextureFormat.RGBA32, false);
           FastNoise fastNoise= new FastNoise();
        fastNoise.SetFrequency(0.12f);
        fastNoise.SetNoiseType(FastNoise.NoiseType.Value);
        fastNoise.SetSeed(Random.Range(0, 10000));
        fastNoise.SetFractalType(FastNoise.FractalType.Billow);
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
                tilemap.SetTile(new Vector3Int(x, y, 0), (value > cut) ? grass : water);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
