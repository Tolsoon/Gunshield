using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitBox : MonoBehaviour
{
    public GameManager gameManager;
    

    public float totalTime; //in seconds

   

    public Text timerText;

    public bool timerIsRunning = true;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("robot"))
        {
            timerIsRunning = true;
            
            StartCoroutine(LoadSceneAsync());

        }
    }

    
    // Update is called once per frame
    void Update()
    {


        if (timerIsRunning)
        {
            timerText.enabled = true;
            if (totalTime > 0)
            {
                totalTime -= Time.deltaTime;
                int timeToDisplay = Convert.ToInt32(totalTime);
                timerText.text = "Back to menu in: " + timeToDisplay.ToString();
            }
            else
            {
                Debug.Log("Time has run out!");
                totalTime = 0;
                timerIsRunning = false;
            }
        }

    }




 
    IEnumerator LoadSceneAsync()
    {
        yield return new WaitForSeconds(4f);

        foreach (GameObject player in gameManager.players)
        {
            Destroy(player);
        }
        Destroy(gameManager.gameObject);
        //Destroy(gameObject);

        yield return new WaitForSeconds(1f);

        AsyncOperation asyncLoaded = SceneManager.LoadSceneAsync(1);
                
        while (!asyncLoaded.isDone)
        {
            yield return null;
        }


    }
}
