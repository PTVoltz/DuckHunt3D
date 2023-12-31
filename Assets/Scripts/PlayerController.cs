using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Startup Variables
    public float mouseSpeed;
    public int horizontalRotation;
    public int verticalRotation;
    public int VerticalOffset;
    Camera mainCam;
    private float timer = 0f;

    //Bullet Sprites
    public Image[] BulletSprites;
    public Sprite[] Bullet;

    //Health Sprites
    public Image[] HealthSprites;
    public Sprite[] Health;

    //On-screen mouse position
    float mouseX;
    float mouseY;

    //Public gameobjects - Gun Aim object, Bullet particle emitter, and Bullet UI sprites
    public GameObject gunAim;
    public ParticleSystem bulletEmitter;

    //Bullet Counter
    public int BulletCounter;
    public int BulletMax;
    bool isReloading;

    public int HealthCounter;

    public GameObject gameManager;
    
    AudioSource gunShootAudio;
    public AudioClip gunFire;

    //On start - set Camera object to main camera, and confine cursor into window boundaries
    void Start()
    {
        mainCam = gameObject.transform.GetChild(0).gameObject.GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Confined;
        BulletCounter = BulletMax;
        HealthCounter = 3;
        gunShootAudio = GetComponent<AudioSource>();
    }

    // All references to "Time.timeScale" are used to check if the game is paused or not
    void Update()
    {
        //Get mouse input and rotate the camera accordingly
        mouseX += mouseSpeed * Input.GetAxis("Mouse X") * Time.timeScale;
        mouseY += mouseSpeed * Input.GetAxis("Mouse Y") * Time.timeScale;

        mouseX = Mathf.Clamp(mouseX, -horizontalRotation, horizontalRotation);
        mouseY = Mathf.Clamp(mouseY, -(verticalRotation-VerticalOffset), (verticalRotation+VerticalOffset));

        mainCam.transform.rotation = Quaternion.Euler(-mouseY, mouseX, 0f);

        //Debug.Log("Mouse Coords: " + mouseX + ", " + mouseY);

        //Set Gun Aim position to ray-cast direction
        RaycastHit hit;
        Ray gunRay = Camera.main.ScreenPointToRay(Input.mousePosition*Time.timeScale);
        if (Physics.Raycast(gunRay, out hit))
        {
            gunAim.transform.position = hit.point;
        }

        //Detect mouse click and fire Bullet particle
        if (Input.GetButtonDown("Fire1") && (BulletCounter > 0) && (Time.timeScale>0))
        {
            bulletEmitter.Emit(1);
            gunShootAudio.PlayOneShot(gunFire, 1.0f);
            BulletCounter -= 1;
        }

        if ((BulletCounter < BulletMax) && (isReloading == false))
        {
            timer += Time.deltaTime;

            if (timer >= 1)
            {
                timer = 0f;
                BulletCounter++;
            }
        }

        doUIUpdate(BulletCounter, HealthCounter);

    }

    void targetMiss()
    {
        HealthCounter -= 1;
        if (HealthCounter <= 0)
        {
            gameManager.gameObject.SendMessage("Dead");
        }
    }

    void doUIUpdate(int BulletCount, int HealthCount)
    {
        //set all to empty
        for (int i = 0; i < BulletSprites.Length; i++)
        {
            BulletSprites[i].sprite = Bullet[0];
        }
        //Refill remaining
        for (int j = 0; j < BulletCount; j++)
        {
            BulletSprites[j].sprite = Bullet[1];
        }

        //set all to empty
        for (int i = 0; i < HealthSprites.Length; i++)
        {
            HealthSprites[i].sprite = Health[0];
        }
        //Refill remaining
        for (int j = 0; j < HealthCount; j++)
        {
            HealthSprites[j].sprite = Health[1];
        }
    }
}