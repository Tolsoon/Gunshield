using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGameManager : MonoBehaviour
{
    public GameObject gameManagerSpawn;

    public GameObject gameManager;

    //bool game manager found
    public bool GMFound;

    public void Update()
    {        
        if(gameManager == null)
        {
            Instantiate(gameManagerSpawn);
            gameManager = GameObject.FindGameObjectWithTag("GameManager");
        }
    }


}
