using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreControl : MonoBehaviour
{
    public static int Score;

    private static Text ScoreText;

    void Start()
    {
        Score = 0;
        ScoreText = GetComponent<Text>();
    }

    public static void AddScore(int add)
    {
        Score += add;
        ScoreText.text = "Score : " + Score;
    }
}
