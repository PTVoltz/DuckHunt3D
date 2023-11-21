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
    public int Speed;
    public int WaveNo;

    void Start()
    {
        WaveNo = 1;
        SpawnCap = 10;
        GlobalSpawnDelay = 3;
        InternalSpawnDelay = 8;
    }

    void Update()
    {
        GlobalSpawnTime += Time.deltaTime;
        if(DucksGone == SpawnCap)
        {
            WaveNo += 1;
            if(WaveNo < 3)
            {
                Speed += 1;
                SpawnCap += 5;
                GlobalSpawnDelay -= 1;
                InternalSpawnDelay -= 2;
                SpawnCount = 0;
                DucksGone = 0;
                DucksMissed = 0;
            }
            //can change these variables if you want, just an idea
        }
    }
}
