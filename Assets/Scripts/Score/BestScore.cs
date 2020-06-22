using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScore : MonoBehaviour
{
    public SaveJson saveJsonScore;
    public Text ScoreText;
    private int score = -1;

	private void Update()
	{
        int scoreJson= saveJsonScore.GetScore();
        if (scoreJson > score)
        {
            score = scoreJson;
            ScoreText.text = "Best Score: " + score;
        }
    }

}
