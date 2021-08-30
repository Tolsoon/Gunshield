using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "MegaDomeMap1" || SceneManager.GetActiveScene().name == "Factory1" || SceneManager.GetActiveScene().name == "Field1" || SceneManager.GetActiveScene().name == "TrainingZone1")
        {
            Destroy(gameObject);
        }
    }
}
