using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    bool gameHasEnded = false;
    public float restartDelay = 1f;

    public GameObject completeUI;
    public GameObject gameoverUI;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void CompleteLevel ()
    {
        Debug.Log("LevelComplete");
        completeUI.SetActive(true);
        Cursor.visible = true;
        //Time.timeScale = 0;
    }

    public void EndGame ()
    {
        if(gameHasEnded == false)
        {
            Debug.Log("GameOver");
            gameHasEnded = true;
            gameoverUI.SetActive(true);
            Cursor.visible = true;
            //Restart(); - Old function
            //Invoke("Restart", restartDelay); - Game over after duration?

        }
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame");
        Application.Quit();
    }

    public void OpenMainMenu()
    {
        //Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void RetryLevel()
    {
        //Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
 
    }

    void Restart()
    {
        //gameoverUI.SetActive(true);
        //Cursor.visible = true;
        //Time.timeScale = 0;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
