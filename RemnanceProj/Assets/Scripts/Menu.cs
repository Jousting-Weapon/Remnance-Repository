using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private float alphaFadeValue, fadeInTime;
    private Transform fadeInScreen;
    private bool doneFading;
    private AudioSource audioSource;
    

    public void Awake()
    {
        alphaFadeValue = 1;
        fadeInScreen = transform.GetChild(0);
        doneFading = false;
        fadeInTime = 3f;

        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Start()
    {
        audioSource.clip = DialogueAssets.clip_menuMusic;
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
