using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckMov : MonoBehaviour
{
    public GameObject Manager;
    public GameObject Scoreboard;
    public Scores scores;
    float TimeAlive;
    float PointsGiven;
    int yRotation;
    int xRotation;
    void Start()
    {
        Manager = GameObject.Find("GameManager");
        Scoreboard = GameObject.Find("Scoreboard");
        scores = Manager.GetComponent<Scores>();
        if(transform.position.y > 1)
        {
            xRotation = 0;
        }
        else
        {
            xRotation = Random.Range(-15, -91);
            //randomizes angle
        }
        //these two are to prevent unhittable ducks
        if (transform.position.x > 1)
        {
            yRotation = -90;
        }
        else
        {
            yRotation = 90;
        }
        //these two make duck face correct way
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        //sets direction
    }
    void Update()
    {
        transform.position += transform.forward * (scores.Speed)/20 * Time.deltaTime;
        if (transform.position.x <= -21 || transform.position.x >= 21 || transform.position.y >= 15)
        {
            scores.DucksGone += 1;
            scores.DucksMissed += 1;
            Destroy(gameObject);  
            //duck is die
        }
        TimeAlive += Time.deltaTime;
        //keeps track of the time the duck's been alive for
    }
    void OnParticleCollision(GameObject other)
        //this should work
    {
        PointsGiven = (1000 - (TimeAlive * 200));
        PointsGiven = Mathf.Round(PointsGiven * 1f) * 1f;
        if (PointsGiven < 0)
        {
            PointsGiven = 0;
        }
        scores.Points += PointsGiven;
        scores.DucksGone += 1;
        Scoreboard.gameObject.SendMessage("BulletHit", PointsGiven);
        Destroy(gameObject);
        //sends the points to the gamemanager, faster the kill, more points, defaults to 0 if you took longer than 5 seconds
    }
}
