using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    //High Score array
    public GameObject[] ScoreSlots;

    //Text component of the current high-score
    Text ScoreValue;

    //Text Component of the score to be moved downward (k)
    Text currentSlot;

    //Text Component of the score to be replaced (k+1)
    Text nextSlot;

    int currentScore;

    //A selected slot number to be replaced with a new highscore
    int highScoreSlot;

    AudioSource scoreboardSource;
    public AudioClip ScoreboardClip;

    void Start()
    {
        Time.timeScale = 1;
        Debug.Log("Time Unfrozen");
        scoreboardSource = GetComponent<AudioSource>();
    }

    public void GameOverMessage(int score)
    {
        scoreboardSource.PlayOneShot(ScoreboardClip, 1.0f);

        //Makes all parts of the End-Screen visible
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }


        for (int j = 0; j < ScoreSlots.Length; j++)
        {
            ScoreValue = ScoreSlots[j].GetComponent<Text>();
            currentScore = PlayerPrefs.GetInt(ScoreSlots[j].ToString(), 0);
            ScoreValue.text = currentScore.ToString();
        }

        //Check High-score Table entries sequentially for per-score scripts
        for (int i=0; i<ScoreSlots.Length; i++)
        {
            //If a score that's smaller than the players current score is found, it replaces that entry as a new High-Score
            ScoreValue = ScoreSlots[i].GetComponent<Text>();
            if (int.Parse(ScoreValue.text) < score)
            {
                for(int k=ScoreSlots.Length-2; k>=i; k--)
                {
                    ScoreSlots[k + 1].GetComponent<Text>().text = ScoreSlots[k].GetComponent<Text>().text;
                    PlayerPrefs.SetInt((ScoreSlots[k+1].ToString()), int.Parse(ScoreSlots[k + 1].GetComponent<Text>().text));
                    PlayerPrefs.Save();
                    Debug.Log("Score " + ScoreSlots[k + 1].GetComponent<Text>().text + " replaced with score " + ScoreSlots[k].GetComponent<Text>().text);
                }
                ScoreValue.text = score.ToString();
                PlayerPrefs.SetInt((ScoreSlots[i].ToString()), int.Parse(ScoreValue.text));
                PlayerPrefs.Save();
                break;
            }
        }
        Time.timeScale = 0;
        Debug.Log("Time Frozen");
    }
}