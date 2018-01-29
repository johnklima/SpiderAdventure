using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    bool gameHasEnded = false;
    public float restartDelay = 1f;

    public GameObject completeUI;
    public GameObject gameoverUI;

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
            gameHasEnded = true;
            Debug.Log("GameOver");
            Restart();
            //Invoke("Restart", restartDelay);
            
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
        gameoverUI.SetActive(true);
        Cursor.visible = true;
        //Time.timeScale = 0;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
