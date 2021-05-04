using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class Menu : MonoBehaviour
{
    private float alphaFadeValue, fadeInTime;
    private Transform fadeInScreen;
    private bool doneFading;
    private AudioSource audioSource;
    public AudioMixerGroup masterMixer;
    public GameObject resolutionDropDown;
    Resolution[] resolutions;

    public void Awake()
    {
        alphaFadeValue = 1;
        fadeInScreen = transform.GetChild(0);
        doneFading = false;
        fadeInTime = 3f;

        audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.GetComponent<AudioSource>().outputAudioMixerGroup = masterMixer;

        SetFullscreen(true);
    }

    public void Start()
    {
        audioSource.clip = DialogueAssets.clip_menuMusic;
        audioSource.loop = true;
        audioSource.Play();

        resolutions = Screen.resolutions;

        resolutionDropDown.GetComponent<TMP_Dropdown>().ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height) 
            {
                currentResolutionIndex = i;
            }
        }
        
        resolutionDropDown.GetComponent<TMP_Dropdown>().AddOptions(options);
        resolutionDropDown.GetComponent<TMP_Dropdown>().value = currentResolutionIndex;
        resolutionDropDown.GetComponent<TMP_Dropdown>().RefreshShownValue();
    }

    public void FixedUpdate()
    {
        if (!doneFading)
        {
            alphaFadeValue = Mathf.Clamp01(alphaFadeValue - Time.deltaTime / fadeInTime);
            fadeInScreen.GetComponent<Image>().color = new Color(0, 0, 0, alphaFadeValue);

            if (alphaFadeValue == 0)
            {
                doneFading = true;
            }
        }
    }

    public void SetFullscreen(bool isFullscreen) 
    {
        Screen.fullScreen = isFullscreen;
    }

    public void ChangeResolution()
    {
        Screen.SetResolution(resolutions[resolutionDropDown.GetComponent<TMP_Dropdown>().value].width, resolutions[resolutionDropDown.GetComponent<TMP_Dropdown>().value].height, Screen.fullScreenMode);
    }

}
