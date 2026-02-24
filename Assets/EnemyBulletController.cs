using UnityEngine;

public class EnemyBulletController : BulletController
{
    private Rigidbody2D rigidBody;

    private void Start()
    {
        Vector2 direction = Vector2.left;

        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.AddForce(Vector2.right * bulletForce);
        rigidBody.linearVelocity = direction * bulletForce;
        Invoke("Die", 10f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.DecreaseLives(1);
        }
    }
}
