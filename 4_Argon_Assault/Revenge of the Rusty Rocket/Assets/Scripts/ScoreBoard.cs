using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    int score = 0;
    TMP_Text scoreText;

    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "Start!";
    }

    public void ChangeScore(int increaseAmount)
    {
        score += increaseAmount;
        scoreText.text = score.ToString();
    }
}