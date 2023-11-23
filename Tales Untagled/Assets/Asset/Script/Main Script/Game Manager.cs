using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Menu
{

    public static bool isPaused = false;
    [SerializeField] private GameObject pauseMenuContainer;
    [SerializeField] private GameObject gameManager;
    private static GameManager instance;
    [SerializeField] Button button;

    [Header("PopUps")]
    [SerializeField] private PopUp popUp;
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
        if (isPaused)
        {
            Resume();
        }
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key pressed");
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
                return;
            }
        }  
    }


    public void Resume()
    {
        
        isPaused = false;
        pauseMenuContainer.SetActive(false);
        gameManager.SetActive(true);
        Time.timeScale = 1f;
        
        
    }

    private void Pause()
    {
        isPaused = true;
        pauseMenuContainer.SetActive(true);
        Time.timeScale = 0f;
        
        Debug.Log(isPaused);

    }

    public void ExitGame()
    {
        
    }

    public void OnBackToMenuClicked()
    {

        popUp.ActivatedMenu(
            "Are you sure you want to go back to Main Menu ?, all save will be lost ",
                // function untuk mengeksekusi jika kita menekan yes
                () =>
                {
                    SceneManager.LoadSceneAsync("Level Selection");
                },
                // function untuk mengeksekusi jika kita menekan tidak
                () =>
                {
                    isPaused = true;
                    gameObject.SetActive(false);
                    SetFirstSelected(button);
                    
                }
            );
    }

    public void OnTheExitGameClicked()
    {
        
        popUp.ActivatedMenu(
            "Are you sure you want to Exit Game ?",
                // function untuk mengeksekusi jika kita menekan yes
                () =>
                {
                    Application.Quit();
                    Debug.Log("Exit Game");
                },
                // function untuk mengeksekusi jika kita menekan tidak
                () =>
                {
                    isPaused = true;
                    gameObject.SetActive(false);
                    SetFirstSelected(button);
                    
                }
            );
    }
}
