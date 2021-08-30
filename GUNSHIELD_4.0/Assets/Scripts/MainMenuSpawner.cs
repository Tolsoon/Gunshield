using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSpawner : MonoBehaviour
{

    public GameObject mainMenuSpawn;

    public GameObject mainMenu;

    // Update is called once per frame
    void Update()
    {
        if (mainMenu == null)
        {
            Instantiate(mainMenuSpawn);
            mainMenu = GameObject.FindGameObjectWithTag("mainMenu");
        }
    }
}
