using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[System.Serializable]

public class ScoreBoard : MonoBehaviour
{
    public Score[] Scores;
    private RequestHandler requestHandler;
    private PlayerStats playerStats;
    void Start()
    {
        requestHandler = GameObject.Find("RequestHandler").GetComponent<RequestHandler>();
        playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        requestHandler.getScores();
    }
    /////////////////////////////////////lädt Scores
    public void loadScores(){
        for (int i = 1; i < 6; i++)
        {
            transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = Scores[i-1].username;
            transform.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = Scores[i-1].highscore.ToString();
        }
        transform.GetChild(6).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Me";
        transform.GetChild(6).GetChild(1).GetComponent<TextMeshProUGUI>().text = playerStats.getHighScore().ToString();
    }

}

[System.Serializable]
public class Score{
    public string username;
    public int highscore;
}
