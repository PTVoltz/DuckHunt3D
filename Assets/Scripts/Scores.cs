using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scores : MonoBehaviour
{
    public int DucksGone;
    public int DucksMissed;
    public float Points;
    public float GlobalSpawnTime;
    //so the screen doesnt completely fill with ducks
    public int GlobalSpawnDelay;
    public int InternalSpawnDelay;
    public int SpawnCount;
    public int SpawnCap;
    public float Speed;
    public int WaveNo;

    public int WaveCap;

    bool isGameRunning;

    bool noHealth;

    public GameObject gameOverMenu;

    void Start()
    {
        WaveNo = 1;
        SpawnCap = 10;
        GlobalSpawnDelay = 3;
        InternalSpawnDelay = 8;

        isGameRunning = true;
        noHealth = false;
    }

    void Update()
    {
        GlobalSpawnTime += Time.deltaTime;
        if(DucksGone == SpawnCap)
        {
            WaveNo += 1;
            if(WaveNo < WaveCap && noHealth == false)
            {
                Speed += 20;
                SpawnCap += 5;
                GlobalSpawnDelay -= 1;
                InternalSpawnDelay -= 2;
                SpawnCount = 0;
                DucksGone = 0;
                DucksMissed = 0;
            }
            else
            {
                if(isGameRunning == true)
                {
                    Debug.Log("Points sent");
                    gameOverMenu.gameObject.SendMessage("GameOverMessage", Points);
                    isGameRunning = false;
                }
            }
            //can change these variables if you want, just an idea
        }
    }

    void Dead()
    {
        noHealth = true;
        DucksGone = SpawnCap;
    }
}
