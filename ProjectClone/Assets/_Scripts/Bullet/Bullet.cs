using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;

    public bool isPlayer;
    public Vector2 velocity;

    private void Awake()
    {
        Destroy(gameObject, 5);
    }

    private void Update()
    {
        transform.position += (Vector3)velocity * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((isPlayer && collision.gameObject.CompareTag("Enemy")) || (!isPlayer && collision.gameObject.CompareTag("Player")))
        {
            var enemy = collision.GetComponent<Actor>();
            enemy.GetDamage(damage);
            Destroy(gameObject);

        }
    }
}
