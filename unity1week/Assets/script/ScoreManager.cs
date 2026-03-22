using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int totalScore = 0;
    private readonly int ScoreNice = 50;
    private readonly int ScoreBad = 10;
    private readonly int ScoreMiss = 0;

    public void AddScore(int type)
    {
        switch (type)
        {
            case 0: totalScore += ScoreNice; break;
            case 2: totalScore += ScoreBad; break;
            case 1: totalScore += ScoreMiss; break;
        }

        if (scoreText != null)
            scoreText.text = totalScore.ToString("D5");
    }
}
