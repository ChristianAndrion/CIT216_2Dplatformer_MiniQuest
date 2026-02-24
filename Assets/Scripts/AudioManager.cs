using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    public void play_pickup_audio()
    {
        AudioSource pickup = gameObject.GetComponent<AudioSource>();
        pickup.Play();
    }
}
