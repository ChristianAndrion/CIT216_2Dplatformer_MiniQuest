using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float bulletForce = 300f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector2 direction = player.GetComponent<PlayerController>().GetDirection();

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.right * bulletForce);
        rb.linearVelocity = direction * bulletForce;
        Invoke("Die", 3f);
    }

    void Die()
    {
        Destroy(gameObject); //Dont use "this" as it refers to the component/script
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("enemy"))
        {
            Destroy(gameObject); //gameObject refers to game object the script is attatched to
        }
    }
}
