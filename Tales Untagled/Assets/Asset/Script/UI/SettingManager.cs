using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public static SettingManager instance;

    [Header("Sound")]
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [SerializeField] private float defaultMusicVolume = 0.5f;
    [SerializeField] private float deafultSFXVolume = 0.5f;

    [Header("Graphic")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private float defaultBrigtness = 1f;
    [SerializeField] private int defaultQualtiy = 4;
    [SerializeField] private int defaultResolution = 10;
    [SerializeField] private bool defaultFullScreen = false;

    private int qualityLevel;
    private bool isFullScreen;
    private float brightnessLevel;

    [Header("Resolution Dropdown")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;


    private string volumeDataPath = "VolumeData.dat";

    private string SettingDataPath = "SettingData.dat";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
        }
        else
        {
            Debug.Log("menemukan Setting Manager 2");
        }
            

    }

    private void Start()
    {

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int CurrentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                CurrentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = CurrentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        LoadVolume();
       
    }


    private void Update()
    {
        SetMusicVolume();
        SetSFXVolume();


    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }

    public void SaveVolume()
    {
        float musicVolume = musicSlider.value;
        float sfxVolume = sfxSlider.value;
       

        using (FileStream file = File.Create(volumeDataPath))
        {
            Dictionary<string, float> volumeData = new Dictionary<string, float>();
            volumeData.Add("Music", musicVolume);
            volumeData.Add("SFX", sfxVolume);

            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, volumeData);
        }
    }

    public void LoadVolume()
    {
        if (File.Exists(volumeDataPath))
        {
            using (FileStream fileStream = File.Open(volumeDataPath, FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                Dictionary<string, float> volumeData = (Dictionary<string, float>)bf.Deserialize(fileStream);

                musicSlider.value = volumeData["Music"];
                sfxSlider.value = volumeData["SFX"];
            }
        }
        else
        {
            Debug.LogWarning("Volume data file not found at " + volumeDataPath);
            SetToDefault();
        }
    }


    public void SetToDefault()
    {
        musicSlider.value = defaultMusicVolume;
        sfxSlider.value = deafultSFXVolume;

        // mengaplikasikan default value kepada audio mixer
        SetMusicVolume();
        SetSFXVolume();
    }


    public void SetBrightness(float brightness)
    {
        
        brightnessLevel = brightness;
        
    }

    public void SetFullScreen(bool FullScreen)
    {
        Screen.fullScreen = FullScreen;
    }

    public void SetQuality(int qualityIndex)
    {
        qualityLevel = qualityIndex;
        QualitySettings.SetQualityLevel(qualityLevel);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    

    public void SetDefaultGraphic()
    {
        SetBrightness(defaultBrigtness);
        SetFullScreen(defaultFullScreen);
        SetQuality(defaultQualtiy);
        SetResolution(defaultResolution);
    }
}
