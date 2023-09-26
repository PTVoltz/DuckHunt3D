using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int mouseSpeed;
    public int horizontalRotation;
    public int verticalRotation;
    public int VerticalOffset;
    Camera mainCam;

    float mouseX;
    float mouseY;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = gameObject.transform.GetChild(0).gameObject.GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX += mouseSpeed * Input.GetAxis("Mouse X");
        mouseY += mouseSpeed * Input.GetAxis("Mouse Y");

        mouseX = Mathf.Clamp(mouseX, -horizontalRotation, horizontalRotation);
        mouseY = Mathf.Clamp(mouseY, -(verticalRotation-VerticalOffset), (verticalRotation+VerticalOffset));

        mainCam.transform.rotation = Quaternion.Euler(-mouseY, mouseX, 0f);

        Debug.Log("Mouse Coords: " + mouseX + ", " + mouseY);
    }
}