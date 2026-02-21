using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private int _lives = 3;

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

    public void DecreaseLives(int live)
    {
        _lives -= live;
    }

    public int GetLives()
    {
        return _lives;
    }
}
