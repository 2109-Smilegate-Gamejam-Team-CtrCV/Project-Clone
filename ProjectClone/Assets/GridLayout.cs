using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class GridLayout : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private void Update()
    {
        spriteRenderer.size = new Vector2((float)Screen.width / Screen.height * Camera.main.orthographicSize * 2,Camera.main.orthographicSize * 2);

        Vector3 pos = transform.localPosition;
        pos.x = (float)Screen.width / Screen.height * -Camera.main.orthographicSize;
        pos.y = -Camera.main.orthographicSize;
        transform.localPosition = pos;

        var offset = spriteRenderer.material.mainTextureOffset;
        offset.x = transform.position.x - (int)transform.position.x;
        offset.y = transform.position.y - (int)transform.position.y;
        spriteRenderer.material.mainTextureOffset = offset;
    }


}
