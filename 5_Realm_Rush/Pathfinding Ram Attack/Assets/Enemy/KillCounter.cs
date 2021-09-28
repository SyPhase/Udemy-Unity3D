using UnityEngine;
using TMPro;

public class KillCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI displayKills;
    [SerializeField] TextMeshProUGUI displayHighScore;
    int currentKillCount;
    public int killHighScore;


    void Start()
    {
        currentKillCount = 0;
        killHighScore = PlayerPrefs.GetInt("Highscore");
        UpdateDisplay();
        UpdateHighScore();
    }

    public void AddToKillCount()
    {
        currentKillCount++;
        UpdateDisplay();
        SetHighScore();
    }

    void UpdateDisplay()
    {
        displayKills.text = "Ram Kills: " + currentKillCount;
    }

    public void SetHighScore()
    {
        killHighScore = PlayerPrefs.GetInt("Highscore");

        if (currentKillCount > killHighScore)
        {
            PlayerPrefs.SetInt("Highscore", currentKillCount);
            killHighScore = currentKillCount;
            UpdateHighScore();
        }
    }

    void UpdateHighScore()
    {
        displayHighScore.text = "High Score: " + killHighScore;
    }
}
