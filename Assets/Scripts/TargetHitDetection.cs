using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHitDetection : MonoBehaviour
{
    //The value of this target
    public int TargetValue;
    public GameObject Scoreboard;

    public ParticleSystem BulletParticles;

    public List<ParticleCollisionEvent> collisionEvents;

    //Every time a particle hits the target, send a Hit message to the Scoreboard
    void OnParticleCollision(GameObject other)
    {   
        Scoreboard.gameObject.SendMessage("BulletHit", TargetValue);
        Debug.Log("Message Sent");
    }
}