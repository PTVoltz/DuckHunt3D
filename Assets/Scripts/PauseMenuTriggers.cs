using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuTriggers : MonoBehaviour
{
    //isPaused Boolean
    public bool isPaused = false;
    public GameObject pauseMenu;

    //Get child Pause Menu object to enable/disable
    void Start()
    {
        pauseMenu = gameObject.transform.GetChild(0).gameObject;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }


    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if(isPaused == false)
            {
                Pause();
            }
            else
            {
                UnPause();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        isPaused = true;

    }

    public void UnPause()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    public void EnterMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
