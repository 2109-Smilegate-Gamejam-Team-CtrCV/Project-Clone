using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;

    public Vector2 velocity;

    private void Awake()
    {
        Destroy(gameObject, 5);
    }

    private void Update()
    {
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
}
