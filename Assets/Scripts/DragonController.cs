using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using System.ComponentModel;

public class DragonController : MonoBehaviour
{

    //LERP MOVEMENT
    /*
    private Vector2 pointA;
    private Vector2 pointB;

    public float speed = 2f;
    public float duration = 3f;

    void Start()
    {
        pointA = transform.position;
        pointB = new Vector2(transform.position.x + 5, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        
        float t = Mathf.PingPong(Time.time * speed, 1f);
        //float t = Mathf.PingPong(Time.time / duration, 1f);
        transform.position = Vector2.Lerp(pointA, pointB, t);
    }*/

    public float distance = 3f;
    public float movementMultiplier;
    public float raycastDistance = 2f;

    private int health = 100;
    private bool facingRight = false;

    private void Start()
    {
        StartCoroutine("MoveObject");
    }
    IEnumerator MoveObject()
    {
        while (true)
        {
            transform.Translate(new Vector2(distance * movementMultiplier, 0));
            //counter--;

            RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance);
            //RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, raycastDistance);
            Debug.DrawRay(transform.position, Vector2.down, Color.red, 1f);//Draws line in scene for 1 second

            if (!hitDown)
            {
                movementMultiplier *= -1; //Move in opposite direction
                //counter = 5; // Reset counter
            }

            yield return new WaitForSeconds(.25f);
        }
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

    private void Update()
    {
        if (movementMultiplier < 0 && facingRight)//Moving left but facing right
        {
            Flip();
            facingRight = false;
        }
        else if (movementMultiplier > 0 && !facingRight)//Moving right but facing left
        {
            Flip();
            facingRight = true;
        }
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x = theScale.x * -1;// Inverts the x
        transform.localScale = theScale;//Set game object to new scale
    }
}