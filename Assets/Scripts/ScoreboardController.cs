using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreboardController : MonoBehaviour
{
    //Value of each shot target
    public int TargetValue;

    //Main Score Counting integer and list of counter digits
    int ScoreCounter;
    int[] CounterDigits = new int[7];

    //----Public variables for Counter objects----
    //Used to assign score object - don't use in code
    public GameObject ScoreObject;

    void Start()
    {
        //Declare and Reset Score Counter
        ScoreCounter = 0;
    }


    //Use target value as input - in case input values get changed later for per-target-type values
    public void BulletHit(int TargetValue)
    {
        Debug.Log("Message Received");
        //Get TextMeshPro component on Score Gameobject - use this for setting score text
        Text ScoreText = ScoreObject.GetComponent<Text>();

        //Add input target value to main score, then set Score Display text to the new score value
        ScoreCounter += TargetValue;
        ScoreText.text = ScoreCounter.ToString();
    }
}
