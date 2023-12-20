using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelSelect : Menu
{

    [Header("level&Stars")]
    [SerializeField] private Button[] levelButtons;
    [SerializeField] private Image[] starImages;
    [SerializeField] private Sprite[] starFilledSprites;
    [SerializeField] private Sprite unfiledStarSprite;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI totalStarsText;
    [SerializeField] private TextMeshProUGUI deathCounterText;

    private void Start()
    {
        DataPersistanceManager.instance.UnlockedLevel(1);
        UpdateLevelSelectionAll();
    }

    private void UpdateLevelSelectionAll()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            // membuat level dimulai dari angka 1
            int levelNumber = i + 1;

            // mengecek apakah level sudah keunlock atau belum
            bool isUnlocked = DataPersistanceManager.instance.IsLevelUnlocked(levelNumber);
            // jika level sudah kebuka maka akan di enable untuk interaksi
            levelButtons[i].enabled = isUnlocked;

            //Debug.Log("all star image is " + starImages[i]);

            //menampilkan bintang yang sudah di dapatkan ketika level kebuka atau sudah selesai
            if (isUnlocked && DataPersistanceManager.instance.IsLevelHaveStars(levelNumber))
            {
                //Debug.Log("unlocked State" + levelNumber);
                int starCounter = DataPersistanceManager.instance.GetStarCountForLevels(levelNumber);


                foreach (Image starImage in starImages)
                {
                    Debug.Log("Star Image: " + starImage.name);
                }

                Debug.Log("calulating the star" + starCounter);

                for (int j = 0; j < starImages.Length; j++)
                {
                    if (starImages[j] != null)
                    {
                        switch (starCounter)
                        {
                            case 1:
                                if (starFilledSprites != null)
                                {
                                    Debug.Log("Case 1: image " + starImages[i].name);
                                    starImages[i].sprite = starFilledSprites[0];
                                    Debug.Log("star Image : " + starImages[i].sprite);
                                }
                                break;
                            case 2:
                                if (starFilledSprites != null)
                                {
                                    Debug.Log("Case 2: image " + starImages[i].name);
                                    starImages[i].sprite = starFilledSprites[1];
                                    Debug.Log("star Image : " + starImages[i].sprite);
                                }
                                break;
                            case 3:
                                if (starFilledSprites != null)
                                {
                                    Debug.Log("Case 3: image " + starImages[i].name);
                                    starImages[i].sprite = starFilledSprites[2];
                                    Debug.Log("star Image : " + starImages[i].sprite);
                                }
                                break;
                            default:
                                if (unfiledStarSprite != null)
                                {
                                    Debug.Log("Case default: image " + starImages[i].name);
                                    starImages[i].sprite = unfiledStarSprite;
                                }
                                break;
                        }
                    }
                }


            }   
            else
            {
                //Debug.Log("Locked state" + levelNumber);
                SetLockedState(starImages[i]);
                levelButtons[i].interactable = false;
            }

            

        }
    }

   
    /*doesnt workk, so sad dudee
     * private void SetStarImages(Image[] starImages, int starCounter)
    {
        Debug.Log($"in SetStarImages() function.  Number of star images: {starImages.Length}, star Images :  {starImages}, star counter {starCounter}");

        foreach(Image starImage in starImages)
        {
            Debug.Log("Star Image: " + starImage.name);
        }


        for (int i = 0; i < starImages.Length; i++) 
        {
            if (starImages[i] != null)
            {
                switch (starCounter)
                {
                    case 1:
                        if (starFilledSprites != null)
                        {
                            Debug.Log("Case 1: image " + starImages[i].name);
                            starImages[i].sprite = starFilledSprites[0];
                            Debug.Log("star Image : " + starImages[i].sprite);
                        }
                        break;
                    case 2:
                        if (starFilledSprites != null)
                        {
                            Debug.Log("Case 2: image " + starImages[i].name);
                            starImages[i].sprite = starFilledSprites[1];
                            Debug.Log("star Image : " + starImages[i].sprite);
                        }
                        break;
                    case 3:
                        if (starFilledSprites != null)
                        {
                            Debug.Log("Case 3: image " + starImages[i].name);
                            starImages[i].sprite = starFilledSprites[2];
                            Debug.Log("star Image : " + starImages[i].sprite);
                        }
                        break;
                    default:
                        if (unfiledStarSprite != null)
                        {
                            Debug.Log("Case default: image " + starImages[i].name);
                            starImages[i].sprite = unfiledStarSprite;
                        }
                        break;
                }
            }
            

        }
    }
    */
    private void SetLockedState(Image starImages)
    {
        
        starImages.sprite = unfiledStarSprite;
    }

   

}
