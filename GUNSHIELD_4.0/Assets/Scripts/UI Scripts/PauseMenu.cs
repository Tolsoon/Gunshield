using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject gamemanager;
    public MoveTest3 robots;
    public void Start()
    {
        gamemanager = GameObject.FindGameObjectWithTag("GameManager");
    }
    public void MainMenu()
    {
        if (robots.paused)
        {
            Time.timeScale = 1;
            foreach(GameObject player in gamemanager.GetComponent<GameManager>().players)
            {
                Destroy(player);
            }
            Destroy(gamemanager);
            //Destroy(gameObject);
            StartCoroutine(LoadSceneAsync());
            
            
        }
        
        
    }

    public void QuitGame()
    {
        if (robots.paused)
        {
            Application.Quit();
        }
       
    }

    IEnumerator LoadSceneAsync()
    {
        
        AsyncOperation asyncLoaded = SceneManager.LoadSceneAsync(1);
        while (!asyncLoaded.isDone)
        {
            yield return null;
        }


    }
}
