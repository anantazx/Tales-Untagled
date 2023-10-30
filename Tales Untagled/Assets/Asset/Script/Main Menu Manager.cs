using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void NewGame()
    {
        DataPersistanceManager.instance.NewGame();
    }

    public void LoadGame()
    {
        DataPersistanceManager.instance.LoadGame();
    }

    public void ExitButton()
    {
        Application.Quit();
    }

}
