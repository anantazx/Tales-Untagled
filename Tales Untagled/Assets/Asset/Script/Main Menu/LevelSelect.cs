using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelSelect : MonoBehaviour
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

            //menampilkan bintang yang sudah di dapatkan ketika level kebuka atau sudah selesai
            if (isUnlocked && DataPersistanceManager.instance.IsLevelHaveStars(levelNumber))
            {
                
                int starCounter = DataPersistanceManager.instance.GetStarCountForLevels(levelNumber);
                SetStarImages(starImages[i], starCounter);
            }
            else
            {
                
                SetLockedState(starImages[i]);
                levelButtons[i].interactable = false;
            }

        }
    }

    private void SetStarImages(Image starImages, int starCounter)
    {
        for(int i = 0;i < starImages.transform.childCount;i++)
        {
            Image childStar = starImages.transform.GetChild(i).GetComponent<Image>();

            if (childStar != null)
            {
               switch (starCounter)
                {
                    case 1:
                        childStar.sprite = starFilledSprites[1];
                        break;
                    case 2:
                        childStar.sprite = starFilledSprites[2];
                        break;
                    case 3:
                        childStar.sprite = starFilledSprites[3];
                        break;
                    default:
                        childStar.sprite = unfiledStarSprite;
                        break;
                }
            }

        }
    }

    private void SetLockedState(Image starImages)
    {
        starImages.sprite = unfiledStarSprite;
    }

    

}
