using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static bool isPaused = false;
    [SerializeField] private GameObject pauseMenuContainer;
    private static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("There Is More Than One Game Manager");
        }
        instance = this;
    }

    public static GameManager GetInstance()
    {
        return instance;
    }


    private void Start()
    {
        
        
    }

    private void Update()
    {
        if (isPaused)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }


    public void Resume()
    {
        isPaused = false;
        pauseMenuContainer.SetActive(false);
        Time.timeScale = 1f;
        
    }

    private void Pause()
    {
        isPaused = true;
        pauseMenuContainer.SetActive(true);
        Time.timeScale = 0f;
        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Debug.Log("Back to Main Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit Game");
    }


}
