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

    Rigidbody m_Rigidbody;
    ParticleSystemRenderer render;
    ParticleSystem emitter;

    void Start()
    {
        //Get particle renderer and emitter, for setting rotations and flipping
        render = GetComponent<ParticleSystemRenderer>();
        emitter = GetComponent<ParticleSystem>();

        //Get Rigidbody component for velocity
        m_Rigidbody = GetComponent<Rigidbody>();

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
        //Note - changed this section to use Rigidbody Velocity
        m_Rigidbody.velocity = transform.forward * (scores.Speed)*20 * Time.deltaTime;
        if (transform.position.x <= -40 || transform.position.x >= 41 || transform.position.y >= 30)
        {
            scores.DucksGone += 1;
            scores.DucksMissed += 1;
            Destroy(gameObject);  
            //duck is die
        }
        TimeAlive += Time.deltaTime;
        //keeps track of the time the duck's been alive for

        //Set particle roll to object rotation

        //Check if duck is moving left or right in world-space, flip sprite to track
        //Also set particle rotation to local object direction - inverted if flipped to ensure correct direction


        if (m_Rigidbody.velocity.x > 0)
        {
            render.flip = new Vector3(0,0,0);
            emitter.startRotation = transform.rotation.x;
        }
        else if (m_Rigidbody.velocity.x < 0)
        {
            render.flip = new Vector3(1,0,0);
            emitter.startRotation = transform.rotation.x*-1;
        }
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
