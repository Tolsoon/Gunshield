using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator leftDoor;
    public Animator rightDoor;

    GameObject gameManager;
    public GameObject MainMenuMusic;
    public GameObject MainMenuMusicPrefab;

    public GameObject[] players;
    public void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        MainMenuMusic = GameObject.FindGameObjectWithTag("MMM");
        if(MainMenuMusic == null)
        {
            Instantiate(MainMenuMusicPrefab);
        }
    }

    public void Update()
    {
       

        players = GameObject.FindGameObjectsWithTag("player");

        foreach(GameObject player in players)
        {
            Destroy(player);
        }
    }
    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }

    public void Lore()
    {
        SceneManager.LoadScene("InformationBase");
    }

    public void StartMatch()
    {
        StartCoroutine(MatchStarting());
    }

    public void ResetGame()
    {
        Destroy(gameManager);
        SceneManager.LoadScene(1);
    }

    public void gameQuit()
    {
        Application.Quit();
    }

    IEnumerator MatchStarting()
    {
        leftDoor.SetBool("GameStart", true);
        rightDoor.SetBool("GameStart", true);

        yield return new WaitForSeconds(1.5f);

        AsyncOperation asyncLoaded = SceneManager.LoadSceneAsync(4);
        while (!asyncLoaded.isDone)
        {
            yield return null;
        }
    }
}
