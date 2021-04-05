using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    private float alphaFadeValue, fadeInTime;
    private Transform fadeInScreen;
    private bool doneFading;
    private AudioSource audioSource;
    public AudioMixerGroup masterMixer;


    public void Awake()
    {
        alphaFadeValue = 1;
        fadeInScreen = transform.GetChild(0);
        doneFading = false;
        fadeInTime = 3f;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.GetComponent<AudioSource>().outputAudioMixerGroup = masterMixer;
    }

    public void Start()
    {
        audioSource.clip = DialogueAssets.clip_menuMusic;
        audioSource.loop = true;
        audioSource.Play();
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

}
