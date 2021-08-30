using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

using UnityEngine.UI;
using System;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{   
    
    public GameObject[] players;
    public GameObject[] inGamePilots;


    public GameObject[] uiSpawns;
    public GameObject[] spawns;

    public bool matchStarted = false;
    public Scene currentScene;

    //bool to set winners
    public bool winnersSet = false;

    public int map;
    public GameObject mapSelect;
    public GameObject[] maps;
    public GameObject[] maps2;
    bool onMap = true;

    //buttons for gamemode selection
    public Sprite[] nextButtons;
    public Button start;
    public Button confirm;
    public Button increase;
    public Button field;

    //gamemode select
    public GameObject gamemodeSelect;
    public GameObject buttonSelect;

    public GameObject playerSpawner;

    public GameObject winUI;
    public float winScore;
    public Text winScoreText;

    //list that only includes the winner
    public List<GameObject> winners;
    //list to include all players and sort them by score
    public List<GameObject> podium;

    public List<GameObject> newList;

    public GameObject winner;
    public GameObject winnerCanvas;
    public Text winnerText;      

    public float playersReady;

    public float playersAlive;

    public Animation leftDoor;
    public Animation rightDoor;

    public Animation leftDoorFirst;
    public Animation rightDoorFirst;

    //bool for when game is paused by anyone
    public bool gamePaused;


    public GameObject countdownBeforeRounds;
    public Text countdown;

    public List<int> randomSpawns = new List<int> { 0, 1, 2, 3 };

    public GameObject BGM;
    public float timeInRound;
   
    public void Awake()
    {        

        currentScene = SceneManager.GetActiveScene();
        DontDestroyOnLoad(gameObject);
        winScoreText.text = "Win Score " + winScore.ToString();


    }

    public void Start()
    {
        StartCoroutine(DelayForDoors());
        randomSpawns = new List<int> { 0, 1, 2, 3 };
    }
    public void FindPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("player");
        int playerCount = players.Count();
        int pi = 0;

        foreach (GameObject player in players)
        {
            player.GetComponentInChildren<loadoutSelection>().PlayerIndex = pi;

            if (pi < playerCount)
            {
                pi += 1;
            }
        }
    }

    public void Update()
    {
        if (gamePaused)
        {
            Time.timeScale = 0;
        }
        else if (!gamePaused)
        {
            Time.timeScale = 1;
        }

        currentScene = SceneManager.GetActiveScene();
        spawns = GameObject.FindGameObjectsWithTag("spawn");

        

        if (playersReady == players.Length && players.Length >= 2)
        {
            StartMatch();
        }
        if (matchStarted)
        {
            matchStarted = false;
            
        }

        if (currentScene.name == "MegaDomeMap1"  || currentScene.name == "Factory1" || currentScene.name == "Field1" || currentScene.name == "TrainingZone1")
        {
            if(BGM == null)
            {
                BGM = GameObject.FindGameObjectWithTag("BGM");
                timeInRound = 0;
            }
            

            if (playersAlive <= 1)
            {               

                StartCoroutine(NextRound());
            }
            
        }

        if(currentScene.name == "WinScene" && !winnersSet)
        {
            spawns = GameObject.FindGameObjectsWithTag("spawn");
            newList.Count();
            for(int i = 0; i < newList.Count(); i++)
            {
                newList[i].GetComponentInChildren<MoveTest3>().alive = true;
                newList[i].GetComponentInChildren<MoveTest3>().gameObject.transform.position = spawns[i].transform.position;
            }

            winnersSet = true;
        }

        timeInRound += Time.deltaTime;

        if (20f < timeInRound && timeInRound < 40f)
        {
            //indicate energy change

        }
        else if (timeInRound > 40f )
        {
            //indicate energy change
        }
    }

    

    public void StartMatch()
    {

        playerSpawner.SetActive(false);
        foreach (GameObject player in players)
        {
            player.GetComponentInChildren<loadoutSelection>().ready = false;
            
            playersReady = 0;
        }
        playersAlive = players.Count();

        StartCoroutine(LoadSceneAsync());

    }

    

    IEnumerator LoadSceneAsync()
    {
        string sceneToLoad = "";

        if(map == 1)
        {
            sceneToLoad = "MegaDomeMap1";
        }
        else if(map == 2)
        {
            sceneToLoad = "Factory1";
        }
        else if(map == 3)
        {
            sceneToLoad = "Field1";
        }
        else if (map == 4)
        {
            sceneToLoad = "TrainingZone1";
        }


        AsyncOperation asyncLoaded = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncLoaded.isDone)
        {
            yield return null;
        }

        countdownBeforeRounds.SetActive(true);
        foreach (GameObject player in players)
        {
            player.GetComponentInChildren<RobotVFX>().SpawnVFX.transform.position = spawns[randomSpawns[player.GetComponentInChildren<loadoutSelection>().PlayerIndex]].transform.position;
        }


        countdown.text = "Match Starts In: 3";
        yield return new WaitForSeconds(1f);
        countdown.text = "Match Starts In: 2";
        yield return new WaitForSeconds(1f);
        countdown.text = "Match Starts In: 1";
        yield return new WaitForSeconds(1f);
        countdownBeforeRounds.SetActive(false);

        foreach (GameObject player in players)
        {
            player.GetComponentInChildren<RobotVFX>().SpawnVFX.transform.position = new Vector3 (1000f,1000f,1000f);
        }

        matchStarted = true;
        timeInRound = 0;
        
        BGM.GetComponent<AudioSource>().pitch = 1f;
        foreach (GameObject player in players)
        {
            player.GetComponentInChildren<loadoutSelection>().inGame = true;
        }

    }
    public IEnumerator NextRound()
    {
       
        //randomize spawns
        for (int i = 0; i < randomSpawns.Count; i++)
        {
            int temp = randomSpawns[i];
            int randomIndex = UnityEngine.Random.Range(i, randomSpawns.Count);
            randomSpawns[i] = randomSpawns[randomIndex];
            randomSpawns[randomIndex] = temp;
        }

        foreach (GameObject points in winUI.GetComponent<PointsList>().points)
        {
            points.SetActive(true);
        }

        foreach (GameObject player in players)
        {            

            if (player.GetComponentInChildren<MoveTest3>().alive)
            {
                player.GetComponentInChildren<PlayerScore>().addScore();
                player.GetComponentInChildren<PlayerScore>().addLastManScore();
                player.GetComponentInChildren<MoveTest3>().invincible = true;
            }

           

        }

        playersAlive = players.Count();

        foreach (GameObject player in players)
        {
            if (player.GetComponentInChildren<PlayerScore>().score >= winScore)
            {
                Debug.Log("adds a winner");
                winners.Add(player);
            }
        }



        if(winners.Count() > 1)
        {
            float tieScore = 0;
            foreach(GameObject player in winners)
            {
                float playerTieScore = player.GetComponentInChildren<PlayerScore>().lastManScore + 
                                       ((player.GetComponentInChildren<PlayerScore>().score - player.GetComponentInChildren<PlayerScore>().lastManScore) / 2f);

                if (playerTieScore > tieScore)
                {
                    tieScore = playerTieScore;
                    winner = player;
                }
            }
        }
        else if(winners.Count() == 1)
        {
            winner = winners[0];
        }

        yield return new WaitForSeconds(3f);

        foreach (GameObject points in winUI.GetComponent<PointsList>().points)
        {
            points.SetActive(false);
        }


        if(winner != null)
        {
            winner.GetComponentInChildren<PlayerScore>().Crown.SetActive(true);
            foreach (GameObject player in players)
            {
                podium.Add(player);
            }

            newList = podium.OrderByDescending(x => x.GetComponentInChildren<PlayerScore>().score).ToList();
            


            Debug.Log("winner is " + winner.GetComponentInChildren<loadoutSelection>().PlayerIndex.ToString());
            SceneManager.LoadScene(5);
            winnerText.text = "Champion is Player " + (winner.GetComponentInChildren<loadoutSelection>().PlayerIndex + 1).ToString();
            winnerCanvas.SetActive(true);
        }
        else
        {
            countdownBeforeRounds.SetActive(true);

            foreach (GameObject player in players)
            {
                player.GetComponentInChildren<RobotVFX>().SpawnVFX.transform.position = spawns[randomSpawns[player.GetComponentInChildren<loadoutSelection>().PlayerIndex]].transform.position;
            }

            countdown.text = "Next Round In: 3";
            yield return new WaitForSeconds(1f);
            countdown.text = "Next Round In: 2";
            yield return new WaitForSeconds(1f);
            countdown.text = "Next Round In: 1";
            yield return new WaitForSeconds(1f);
            countdownBeforeRounds.SetActive(false);

            foreach (GameObject player in players)
            {
                player.GetComponentInChildren<RobotVFX>().SpawnVFX.transform.position = new Vector3(1000f, 1000f, 1000f);
            }

            matchStarted = true;
            timeInRound = 0;
            
            BGM.GetComponent<AudioSource>().pitch = 1f;
            foreach (GameObject points in winUI.GetComponent<PointsList>().points)
            {
                points.SetActive(false);
            }
            foreach (GameObject player in players)
            {
                player.GetComponentInChildren<MoveTest3>().newRound();
            }

        }

        
    }
    public IEnumerator RestartGame()
    {        
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(3);
    }

    public void WinScorePlus()
    {
        winScore += 5;
        if (winScore > 30)
        {
            winScore = 30;
        }

        winScoreText.text = "Win Score " + winScore.ToString();
    }

    public void WinScoreMinus()
    {
        winScore -= 5;
        if (winScore < 5)
        {
            winScore = 5;
        }

        winScoreText.text = "Win Score " + winScore.ToString();
    }

    public void LoadPlayerSelectScene()
    {
        StartCoroutine(LoadPlayerSelect());
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(1);
        Destroy(gameObject);
    }

    //megadome map 1
    public void SetMap()
    {
        map = 1;
        AllMapsInactive();
        maps[0].SetActive(true);
        maps2[0].SetActive(true);
    }

    //factory map 1
    public void SetMap2()
    {
        map = 2;
        AllMapsInactive();
        maps[1].SetActive(true);
        maps2[1].SetActive(true);
    }

    //field map 1
    public void SetMap3()
    {
        map = 3;
        AllMapsInactive();
        maps[2].SetActive(true);
        maps2[2].SetActive(true);
    }

    public void SetMap4()
    {
        map = 4;
        AllMapsInactive();
        maps[3].SetActive(true);
        maps2[3].SetActive(true);
    }

    IEnumerator LoadPlayerSelect()
    {
        leftDoor.Play("LeftDoorGM_CLose");
        rightDoor.Play("RightDoorGM_Close");
        leftDoorFirst.Play("LeftDoorGM_CLose");
        rightDoorFirst.Play("RightDoorGM_Close");

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(2);
        mapSelect.SetActive(false);
        gamemodeSelect.SetActive(false);
        buttonSelect.SetActive(false);
        playerSpawner.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        leftDoor.Play("LeftDoorGM_Open");
        rightDoor.Play("RightDoorGM_Open");
        leftDoorFirst.Play("LeftDoorGM_Open");
        rightDoorFirst.Play("RightDoorGM_Open");

    }

    IEnumerator DelayForDoors()
    {
        yield return new WaitForSeconds(0.5f);

        leftDoorFirst.Play("LeftDoorGM_Open");
        rightDoorFirst.Play("RightDoorGM_Open");

    }

    public void AllMapsInactive()
    {
        foreach(GameObject map in maps)
        {
            map.SetActive(false);
        }

        foreach (GameObject map in maps2)
        {
            map.SetActive(false);
        }
    }

    

    public void swapScreens()
    {
        if (onMap)
        {
            start.GetComponent<Button>().image.sprite = nextButtons[0];

            Navigation NewNav = new Navigation();
            NewNav.mode = Navigation.Mode.Explicit;

            //Set what you want to be selected on down, up, left or right;
            NewNav.selectOnUp = increase.GetComponent<Button>();
            NewNav.selectOnDown = confirm.GetComponent<Button>();
            //Assign the new navigation to your desired button or ui Object
            start.GetComponent<Button>().navigation = NewNav;
            
            mapSelect.SetActive(false);
            onMap = false;
        }
        else if (!onMap)
        {
            start.GetComponent<Button>().image.sprite = nextButtons[1];
            Navigation NewNav = new Navigation();
            NewNav.mode = Navigation.Mode.Explicit;

            //Set what you want to be selected on down, up, left or right;
            NewNav.selectOnLeft = field.GetComponent<Button>();            

            //Assign the new navigation to your desired button or ui Object
            start.GetComponent<Button>().navigation = NewNav;
            
            mapSelect.SetActive(true);
            onMap = true;
        }
    }

    
}