    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;

    public class MainMenuManager : Menu
    {
        [SerializeField]private SaveSlotManager slotManager;

        [Header("Menu Button")]
        [SerializeField] private Button LoadGameButton;

        private void Start()
        {
            DisableButtonDependingOnData();
        }

        private void DisableButtonDependingOnData()
        {
            if (!DataPersistanceManager.instance.HasGameData())
            {

                LoadGameButton.interactable = false;
            }
        }


        public void NewGame()
        {
            slotManager.ActivationMenu(false);
        
        }

        public void LoadGame()
        {
            slotManager.ActivationMenu(true);
        }
        
        public void ExitButton()
        {
            Application.Quit();
        }

    }
