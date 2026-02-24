using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingLizController : MonoBehaviour
{
    private Animator anim;
    public float raycastDistance = 10f;

    public Transform firePoint;
    public GameObject bullet;
    public bool facingRight = false;


    public float attackRate = 0.5f;
    private float nextAttackTime = 0;

    private int health = 100;

    private bool isFiring = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine("SearchForPlayer");
    }

    IEnumerator SearchForPlayer()
    {
        while (true)
        {

            

            if (isFiring && Time.time >= nextAttackTime)
            {
                nextAttackTime = Time.time + attackRate; //Set this to allow future attack time
                anim.SetTrigger("isShooting");

                Instantiate(bullet, firePoint.position, facingRight ? firePoint.rotation : Quaternion.Euler(0, 180, 0)); //Terniary Operator

            }

            isFiring = false;
            

            yield return new WaitForSeconds(2f);
        }

    }

    private void Update()
    {

        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, raycastDistance);

        if (hitLeft)
        {
            isFiring = true;
        }
        

        anim.SetBool("isShooting", isFiring);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            health -= 25;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}