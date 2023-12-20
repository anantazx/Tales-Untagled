using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using TMPro;

public class aftercutscene : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private string sceneToLoad;
    [SerializeField] private float skipTime = 5f;
    [SerializeField] private GameObject SkipCutscene;

    private void Start()
    {
        videoPlayer.loopPointReached += LoadScene;
        StartCoroutine(ShowSkipText());
    }

    private void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.E))
        {
            videoPlayer.time += skipTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            videoPlayer.time = videoPlayer.length;
        }
    }

    private void LoadScene(VideoPlayer source)
    {
        SceneManager.LoadSceneAsync(sceneToLoad);
    }

    private IEnumerator ShowSkipText()
    {
        yield return new WaitForSeconds(4f);
        SkipCutscene.SetActive(true);
        yield return new WaitForSeconds(4f);
        SkipCutscene.SetActive(false);
    }

}
