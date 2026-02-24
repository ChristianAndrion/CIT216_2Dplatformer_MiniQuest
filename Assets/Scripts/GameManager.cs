using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    //public GameObject healthUI;

    public GameObject unlockedText;
    public GameObject door;
    public GameObject winText;

    //public GameObject healthUI;


    private int _lives = 3;
    private bool _doorIsUnlocked = false;

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

    //private void Start()
    //{
    //    UpdateHearts();
    //}

    //public void DecreaseLives(int live)
    //{
    //    _lives -= live;
    //    UpdateHearts();
    //}

    public int GetLives()
    {
        return _lives;
    }

    //public void UpdateHearts()
    //{
    //    //int heartsAmount = healthUI.gameObject.transform.childCount;
    //    //for (int i = 0; i < heartsAmount; i++)
    //    //{
    //    //    Debug.Log(i);
    //    //    if (i < _lives)
    //    //        healthUI.transform.GetChild(i).gameObject.SetActive(true);
    //    //    else
    //    //        healthUI.transform.GetChild(i).gameObject.SetActive(false);
    //    //}

    //    for(int i = 0; i < 3;i++)
    //    {
    //        healthUI.transform.GetChild(i).gameObject.SetActive(false);
    //    }
        
    //    for (int i = 0; i < _lives; i++)
    //    {
    //        healthUI.transform.GetChild(i).gameObject.SetActive(true);
    //    }
        

    //}
    public void UnlockDoor()
    {
        Debug.Log("Unlocking door");
        if (!_doorIsUnlocked)
        {
            Debug.Log("Door is Unlocked");
            _doorIsUnlocked=true;
            unlockedText.SetActive(true);
            door.SetActive(false);
        }
    }

    public void GameWin()
    {
        winText.SetActive(true);
    }
}
