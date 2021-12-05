using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreScript : MonoBehaviour
{
    public Text score;
    public Text bestScore;
    public void WriteScore(int passedScore, int passedBest)
    {
        score.text = "Final Score: " + passedScore.ToString();
        bestScore.text = "Best Score: " + passedBest.ToString();
    }
}
