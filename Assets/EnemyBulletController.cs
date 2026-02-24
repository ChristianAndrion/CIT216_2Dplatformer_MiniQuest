using UnityEngine;

public class EnemyBulletController : BulletController
{
    private Rigidbody2D rb;

    private void Start()
    {
        Vector2 direction = Vector2.left;

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.right * bulletForce);
        rb.linearVelocity = direction * bulletForce;
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
