using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public GameObject unlockedText;
    public GameObject door;
    public GameObject winText;

    //public int numOfHearts;
    //public GameObject healthUI;
    //public Image[] hearts;

    //public GameObject heart1;
    //public GameObject heart2;
    //public GameObject heart3;   

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

    private void Start()
    {
        door = GameObject.FindGameObjectWithTag("door");
        DontDestroyOnLoad(unlockedText);
        //DontDestroyOnLoad(door);
        DontDestroyOnLoad(winText);
        //DontDestroyOnLoad(healthUI);
        //DontDestroyOnLoad(heart1);
        //DontDestroyOnLoad(heart2);
        //DontDestroyOnLoad(heart3);
        //hearts = healthUI.GetComponentsInChildren<Image>();
        UpdateHearts();
    }

    public void DecreaseLives(int live)
    {
        _lives -= live;
        UpdateHearts();
    }

    public int GetLives()
    {
        return _lives;
    }

    public void UpdateHearts()
    {
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

        //for (int i = 0; i < hearts.Length; i++)
        //{
        //    if(i < numOfHearts)
        //    {
        //        hearts[i].enabled = true;
        //    }
        //    else
        //    {
        //        hearts[i].enabled = false;
        //    }
        //}

    }
public void UnlockDoor()
    {
        Debug.Log("Unlocking door");
        if (!_doorIsUnlocked)
        {
            door = GameObject.FindGameObjectWithTag("door");
            Debug.Log("Door is Unlocked");
            _doorIsUnlocked=true;
            unlockedText.SetActive(true);
            Destroy(door);
        }
    }

    public void GameWin()
    {
        winText.SetActive(true);
    }
}
