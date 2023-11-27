using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    //High Score array
    public GameObject[] ScoreSlots;

    Text ScoreValue;
    int currentScore;

    //A selected slot number to be replaced with a new highscore
    int highScoreSlot

    void Start()
    {
        Time.timeScale = 1;
    }

    public void GameOverMessage(int score)
    {
        //Makes all parts of the End-Screen visible
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }

        //Initialise Highscore table - get saved Highscores from PlayerPrefs
        for (int j = 0; j < ScoreSlots.Length; j++)
        {
            ScoreValue = ScoreSlots[j].GetComponent<Text>();
            currentScore = PlayerPrefs.GetInt("Score" + j.ToString(), 0);
            ScoreValue.text = currentScore.ToString();
        }

        //Check High-score Table entries sequentially for per-score scripts
        for (int i=0; i<ScoreSlots.Length; i++)
        {
            //If a score that's smaller than the players current score is found, it replaces that entry as a new High-Score
            ScoreValue = ScoreSlots[i].GetComponent<Text>();
            if (int.Parse(ScoreValue.text) < score)
            {
                ScoreValue.text = score.ToString();
            }

            //PlayerPrefs - used to save High-scores to an external file
            PlayerPrefs.SetInt(("Score" + i.ToString()), int.Parse(ScoreValue.text));
            PlayerPrefs.Save();
            Debug.Log("Saved");
        }
        Time.timeScale = 0;
    }
}
