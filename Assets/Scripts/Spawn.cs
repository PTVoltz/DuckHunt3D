using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
    //the cube is temporary btw, just so i could see where they were
{
    public float InternalSpawnTime;
    public Scores scores;
    public GameObject SpawningDuck;

    void Update()
    {
        if(scores.SpawnCount < scores.SpawnCap)
            //the number is just max ducks spawned, can change it
        {
            InternalSpawnTime += Time.deltaTime;
            if (scores.GlobalSpawnTime >= scores.GlobalSpawnDelay && InternalSpawnTime >= scores.InternalSpawnDelay)
            //doesn't spawn unless it has been [GlobalSpawnDelay] seconds since the last duck, and [InternalSpawnDelay] seconds since the last duck from this spawn (kinda)
            {
                GameObject Duck = Instantiate(SpawningDuck, transform.position, Quaternion.identity);
                scores.SpawnCount += 1;
                scores.GlobalSpawnTime = 0;
                InternalSpawnTime = Random.Range(0, 5);
                //bit a rng just to make it more randomy
            }
        }
    }
}
