using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadoutDontDestroy : MonoBehaviour
{
    GameManager gameManager;
    public loadoutSelection loadout;
    public Canvas setupPanel;
    public GameObject eventSys;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {


        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            
            Destroy(gameObject);
        }

        if(loadout.inGame == false)
        {
            switch (GetComponentInChildren<loadoutSelection>().PlayerIndex)
            {
                case 0:
                    transform.position = gameManager.uiSpawns[0].transform.position;
                    break;
                case 1:
                    transform.position = gameManager.uiSpawns[1].transform.position;
                    break;
                case 2:
                    transform.position = gameManager.uiSpawns[2].transform.position;
                    break;
                case 3:
                    transform.position = gameManager.uiSpawns[3].transform.position;
                    break;
            }
        }
        

        if (loadout.inGame)
        {
            setupPanel.enabled = false;
            eventSys.SetActive(false);
        }
    }
}
