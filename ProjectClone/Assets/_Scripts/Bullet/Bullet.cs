using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;

    public bool isPlayer;
    public Vector2 velocity;
    public float lifeTime;
    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        float d = Vector2.SignedAngle(Vector2.up, velocity) + 90;

        transform.rotation = Quaternion.Euler(0, 0, d);
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
