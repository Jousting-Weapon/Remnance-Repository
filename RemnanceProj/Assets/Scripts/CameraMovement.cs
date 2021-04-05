using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    public TextMeshPro startText;

    public float speed = 10.0F;
    private float startTime;
    private float journeyLength;

    private Transform StartMarker;
    private Transform EndMarker;
    private Transform MovementMarker;
    private GameObject OptionsMenu;
    private GameObject optionsScreenButton;

    public bool optionsCutscene;
    public bool exitOptionsMenu;

    public bool inTransition;
    public bool inOptionsMenu;

    Button exitOptionsMenuButton;

    private void Awake()
    {
        inTransition = true;
        inOptionsMenu = false;
        MovementMarker = GameObject.FindGameObjectWithTag("Camera Marker").transform;
        StartMarker = MovementMarker.GetChild(0);
        EndMarker = MovementMarker.GetChild(1);

        OptionsMenu = GameObject.FindGameObjectWithTag("Options Menu");
        exitOptionsMenuButton = OptionsMenu.transform.GetChild(0).GetComponent<Button>();
        optionsScreenButton = GameObject.FindGameObjectWithTag("OptionsScreenButton");

        OptionsMenu.SetActive(false);
    }

    void Start()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(StartMarker.position, EndMarker.position);
    }
    void Update()
    {
        if(inTransition)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(StartMarker.position, EndMarker.position, fracJourney);
            transform.rotation = Quaternion.Lerp(StartMarker.rotation, EndMarker.rotation, fracJourney);
            if(Vector3.Distance(transform.position, EndMarker.position) < 5)
            {
                inTransition = false;
            }
        }
        if(optionsCutscene)
        {
            StartMarker = EndMarker;
            EndMarker = MovementMarker.GetChild(2);
            inTransition = true;

            startTime = Time.time;
            journeyLength = Vector3.Distance(StartMarker.position, EndMarker.position);
            inOptionsMenu = true;
            optionsCutscene = false;
        }

        if(inOptionsMenu && !inTransition && Input.GetKeyDown(KeyCode.Escape) || inOptionsMenu && !inTransition && exitOptionsMenu)
        {
            StartMarker = EndMarker;
            EndMarker = MovementMarker.GetChild(1);
            inTransition = true;
            inOptionsMenu = false;
            OptionsMenu.SetActive(false);
            optionsScreenButton.SetActive(true);
            exitOptionsMenu = false;
            optionsScreenButton.transform.GetChild(0).gameObject.SetActive(false);
            optionsScreenButton.GetComponent<TextMeshPro>().color = Color.white;

            startTime = Time.time;
            journeyLength = Vector3.Distance(StartMarker.position, EndMarker.position);
        }

        if(inOptionsMenu && !inTransition)
        {
            OptionsMenu.SetActive(true);
            optionsScreenButton.SetActive(false);
        }

    }

    public void onExitButtonClicked()
    {
        exitOptionsMenu = true;
    }

}
