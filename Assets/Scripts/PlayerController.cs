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

    //On start - set Camera object to main camera, and confine cursor into window boundaries
    void Start()
    {
        mainCam = gameObject.transform.GetChild(0).gameObject.GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Confined;
        BulletCounter = BulletMax;
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
            //Debug.Log("Clicked");
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

        doUIUpdate(BulletCounter);

    }

    void doUIUpdate(int BulletCount)
    {
        //set all hearts to empty
        for (int i = 0; i < BulletSprites.Length; i++)
        {
            BulletSprites[i].sprite = Bullet[0];
        }
        //Refill remaining hearts
        for (int j = 0; j < BulletCount; j++)
        {
            BulletSprites[j].sprite = Bullet[1];
        }
    }
}