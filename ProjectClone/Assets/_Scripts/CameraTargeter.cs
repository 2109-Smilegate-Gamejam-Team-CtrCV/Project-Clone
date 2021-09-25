using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargeter : MonoBehaviour
{
    public Transform target;


    private void LateUpdate()
    {
        var targetPos = Vector3.SlerpUnclamped((Vector2)transform.position, (Vector2)target.position, 2 * Time.deltaTime);
        var pos = transform.position;
        pos.x = targetPos.x;
        pos.y = targetPos.y;
        transform.position = pos;
    }
}
