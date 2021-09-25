using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargeter : MonoBehaviour
{
    public Transform target;
    public float maxRadius;

    private void LateUpdate()
    {
        if (target == null)
            target = GameManager.Instance.clone.transform;

        var center = new Vector2(Screen.width, Screen.height) * 0.5f;
        var mouse = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var dir = (mouse - (Vector2)target.position);
        var radius = Mathf.Min(maxRadius, dir.magnitude);
        var targetPos = Vector3.SlerpUnclamped((Vector2)transform.position, (Vector2)target.position + dir.normalized * radius, 2 * Time.deltaTime);
        var pos = transform.position;
        pos.x = targetPos.x;
        pos.y = targetPos.y;
        transform.position = pos;
    }
}
