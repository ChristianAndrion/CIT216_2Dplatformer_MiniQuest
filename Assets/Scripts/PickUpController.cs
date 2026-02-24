using System.Collections;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    private AudioSource pickup_sound;
    public float distance = 3f;
    public float movementMultiplier;

    private int counter = 5; //How many times the object moves in a direction

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pickup_sound = GetComponent<AudioSource>();
        StartCoroutine("MoveObject");        
    }

    IEnumerator MoveObject()
    {
        while (true)
        {
            transform.Translate(new Vector2(0, distance * movementMultiplier));
            counter--;
            if(counter<=0)
            {
                movementMultiplier *= -1; //Move in opposite direction
                counter = 5; // Reset counter
            }
            
            yield return new WaitForSeconds(.25f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.play_pickup_audio();
            Destroy(gameObject);
        }
    }
}
