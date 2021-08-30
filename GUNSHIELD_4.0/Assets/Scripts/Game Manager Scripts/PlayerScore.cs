using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public float score;
    public float lastManScore;
    public Text ScoreText;

    public GameObject Crown;

    public void Update()
    {
        ScoreText.text = "Score " + score.ToString();
    }
    public void addScore()
    {
        score += 1;
        ScoreText.text = "Score " + score.ToString();
    }

    public void addLastManScore()
    {
        lastManScore += 1;
        
    }





}
