using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ChoiceManager : MonoBehaviour
{
    // A reference to the UI
    public GameObject DialogueManager;

    // A reference to the actual UI element that will display the text
    public TextMeshProUGUI subtitleDisplay;

    // Represents the speed at which dialogue is "printed" out
    public float typingSpeed;

    // A boolean representing if the UI is visible or not
    public bool isVisible;

    // A reference to the Dialogue Handler
    public GameObject dialogueHandler;

    // A reference to the Canvas Handler
    public GameObject canvasHandler;

    // A reference to the Game Display Textbox
    public Text gameDisplayText;

    // Represents how far into the dialogue tree a player is.
    private int progression;

    // Initializes our game timer - used to track dialogue readouts
    private float timer = 0.0f;

    // Represents the amount of time between subtitle readouts
    private static float timeGap = 2.0f;

    // If the UI is interactable
    bool interactableUI = false;

    // Varaibles used in tandem with DialogueAssets to print out the UI
    bool tutorialSubtitleOne = true;
    bool firstTime = true;
    bool skipAudioWait = false;

    bool tutorialGameDisplayOne = false;
    bool tutorialC1aSubtitles = false;
    bool c1aTree = true;
    bool c2aTree = false;
    bool c1bTree = false;
    bool tutorialC1aTreeSubtitles = false;
    bool tutorialC1bSubtitles = false;

    bool tutorialC2aSubtitles = false;
    bool tutorialC2bSubtitles = false;    
    bool tutorialC2aSubtitles_Part_2 = false;

    bool c2a_Tree_Branch = false;
    bool mouse_tutorial = false;

    bool movementFlag = false;
    bool endDialogue = false;

    private Vector3 mouseChange = Vector3.zero;
    private int counter = 0;

    private AudioSource audioSource;
    public AudioClip audioFile;

    /// <summary>
    /// Iterates through the inputted Dialogue Asset and prints it to the screen.
    /// </summary>
    /// <param name="sentencesToDisplay">The subtitle text</param>
    /// <returns></returns>
    void DisplaySubtitles(string[] sentencesToDisplay, int index, AudioClip audioToPlay)
    {
        subtitleDisplay.text = sentencesToDisplay[index];
        audioSource.clip = audioToPlay;
        audioSource.Play();
        progression++;
        timeGap = audioToPlay.length;
    }

    //// Start is called before the first frame update
    //void Start()
    //{
    //    // Initialize an Audio Source
    //    audioSource = this.gameObject.AddComponent<AudioSource>();

    //    // Initialize Progression to 0.
    //    progression = 0;

    //    // Disables the UI when the game launches
    //    isVisible = false;
    //    dialogueHandler.SetActive(false);
    //    gameDisplayText.enabled = false;

    //    // Set the Dialogue UI to display the first set of possible choices.
    //    DialogueManager.GetComponent<Dialogue>().UpdateChoices(DialogueAssets.tutorial_c1a_tree);

    //    // Sets the default text to empty
    //    subtitleDisplay.text = string.Empty;

    //    //Types out the initial message
    //    DisplaySubtitles(DialogueAssets.tutorial_subtitle_1, 0, DialogueAssets.tutorial_clip_P1);
    //}

    //private void FixedUpdate()
    //{
    //    timer += Time.deltaTime;

    //    if (tutorialSubtitleOne && timer > timeGap)
    //    {
    //        if (firstTime == true)
    //        {
    //            counter = 1;
    //            DisplaySubtitles(DialogueAssets.tutorial_subtitle_1, counter, DialogueAssets.tutorial_clip_H1);
    //        }
    //        else if (counter == 1)
    //        {
    //            counter++;
    //            DisplaySubtitles(DialogueAssets.tutorial_subtitle_1, counter, DialogueAssets.tutorial_clip_H2); 
    //        }
    //        else
    //        {
    //            DisplaySubtitles(DialogueAssets.tutorial_subtitle_1, counter, DialogueAssets.tutorial_clip_H3);
    //        }

    //        timer = 0;

    //        if (firstTime == true)
    //        {
    //            firstTime = false;
    //        }
    //        else
    //        {
    //            counter++;
    //        }

    //        if(counter == DialogueAssets.tutorial_subtitle_1.Length)
    //        {
    //            tutorialSubtitleOne = false;
    //            tutorialGameDisplayOne = true;
    //            counter = 0;
    //        }
    //    }

    //    // Display the next dialogue prompt
    //    if(tutorialGameDisplayOne && timer > timeGap)
    //    {
    //        gameDisplayText.text = "Press 'Q' to bring up your wrist comlink.";
    //        gameDisplayText.enabled = true;
    //        interactableUI = true;
    //        timer = 0;
    //    }

    //    // Once a player brings up the UI, change the display message
    //    if(dialogueHandler.active == true && tutorialC1aSubtitles == false)
    //    {
    //        gameDisplayText.text = "Use the ‘up’ and ‘down’ arrow keys to navigate the comlink.";
    //        tutorialGameDisplayOne = false;
    //        timer = 0;
    //        counter = 0;
    //        skipAudioWait = true;
    //        gameDisplayText.text = String.Empty;
    //        gameDisplayText.enabled = false;
    //    }

    //    if (tutorialC1aSubtitles && timer > timeGap || tutorialC1aSubtitles && skipAudioWait)
    //    {
    //        if (counter == 0)
    //        {
    //            Debug.Log(counter);
    //            DisplaySubtitles(DialogueAssets.tutorial_c1a_subtitles, counter, DialogueAssets.tutorial_clip_C1a);
    //            counter = DialogueAssets.tutorial_c1a_subtitles.Length-1;
    //        }

    //        timer = 0;
    //        counter++;

    //        if (counter == DialogueAssets.tutorial_c1a_subtitles.Length)
    //        {
    //            tutorialC1aSubtitles = false;
    //            tutorialC1aTreeSubtitles = true;
    //            counter = 0;
    //            skipAudioWait = false;
    //        }
    //    }

    //    if (tutorialC1bSubtitles && timer > timeGap || tutorialC1bSubtitles && skipAudioWait)
    //    {
    //        counter = 1;
    //        DisplaySubtitles(DialogueAssets.tutorial_c1a_subtitles, counter, DialogueAssets.tutorial_clip_C1b);

    //        timer = 0;
    //        tutorialC1bSubtitles = false;
    //        c1bTree = true;
    //        skipAudioWait = false;
    //    }

    //    if (c1bTree && timer > timeGap)
    //    {
    //        counter = 0;
    //        DisplaySubtitles(DialogueAssets.tutorial_c1b_tree, counter, DialogueAssets.tutorial_clip_C1b_Tree_P1);
    //        timer = 0;
    //        c1bTree = false;
    //        endDialogue = true; //Will need to be changed
    //    }

    //    if (tutorialC1aTreeSubtitles && timer > timeGap || tutorialC1aTreeSubtitles && skipAudioWait)
    //    {
    //        if (counter == 0)
    //        {
    //            DisplaySubtitles(DialogueAssets.tutorial_c1a_tree_2_subtitles, counter, DialogueAssets.C1a_Tree_H1);              
    //        }
    //        else if (counter == 1)
    //        {
    //            DisplaySubtitles(DialogueAssets.tutorial_c1a_tree_2_subtitles, counter, DialogueAssets.C1a_Tree_H2);               
    //        }
    //        else if (counter == 2)
    //        {
    //            gameDisplayText.text = "Use ‘W,’ ‘A,’ ‘S,’ and ‘D’ for movement.";
    //            gameDisplayText.enabled = true;
    //            counter--;
    //            skipAudioWait = true;
    //            if(movementFlag)
    //            {
    //                gameDisplayText.enabled = false;
    //                counter++;                    
    //            }                
    //        }
    //        else
    //        {
    //            DisplaySubtitles(DialogueAssets.tutorial_c1a_tree_2_subtitles, counter-1, DialogueAssets.C1a_Tree_H3);
    //        }

    //        timer = 0;
    //        counter++;

    //        if (counter == DialogueAssets.tutorial_c1a_tree_2_subtitles.Length + 1)
    //        {
    //            tutorialC1aTreeSubtitles = false;
    //            c2aTree = true; // May need to change
    //            counter = 0;
    //            dialogueHandler.SetActive(true);
    //            DialogueManager.GetComponent<Dialogue>().UpdateChoices(DialogueAssets.tutorial_tree_2);
    //        }
    //    }

    //    if (tutorialC2aSubtitles && timer > timeGap || tutorialC2aSubtitles && skipAudioWait)
    //    {
    //        DisplaySubtitles(DialogueAssets.tutorial_c2a_subtitles, counter, DialogueAssets.clip_C2a_Audio);
    //        timer = 0;
    //        skipAudioWait = false;
    //        tutorialC2aSubtitles = false;
    //        c2a_Tree_Branch = true;
    //    }

    //    if(c2a_Tree_Branch && timer > timeGap)
    //    {
    //        if(counter == 0)
    //        {
    //            DisplaySubtitles(DialogueAssets.c2a_tree_subtitles, counter, DialogueAssets.clip_C2a_Tree_H1);
    //        }
    //        else
    //        {
    //            DisplaySubtitles(DialogueAssets.c2a_tree_subtitles, counter, DialogueAssets.clip_C2a_Tree_H2);
    //        }
    //        counter++;
    //        timer = 0;

    //        if(counter == DialogueAssets.c2a_tree_subtitles.Length-1)
    //        {
    //            c2a_Tree_Branch = false;
    //            counter = 0;
    //            mouse_tutorial = true;
    //        }
    //    }

    //    if(mouse_tutorial == true)
    //    {
    //        gameDisplayText.text = "Use the mouse to look around.";
    //        gameDisplayText.enabled = true;

    //        if(!firstTime)
    //        {
    //            firstTime = true;
    //            mouseChange = Input.mousePosition;
    //        }

    //        if(mouseChange != Input.mousePosition)
    //        {
    //            gameDisplayText.text = String.Empty;
    //            gameDisplayText.enabled = false;
    //            tutorialC2aSubtitles_Part_2 = true;
    //            mouse_tutorial = false;
    //            skipAudioWait = true;
    //        }

    //        timer = 0;
    //    }

    //    if(tutorialC2aSubtitles_Part_2 && timer > timeGap || tutorialC2aSubtitles_Part_2 && skipAudioWait)
    //    {
    //        counter = 2;
    //        DisplaySubtitles(DialogueAssets.c2a_tree_subtitles, counter, DialogueAssets.clip_C2a_Tree_P1);
    //        tutorialC2aSubtitles_Part_2 = false;
    //        c1bTree = true;
    //        timer = 0;
    //    }


    //    if(tutorialC2bSubtitles && timer > timeGap || tutorialC2bSubtitles && skipAudioWait)
    //    {
    //        DisplaySubtitles(DialogueAssets.tutorial_c2b_subtitles, counter, DialogueAssets.clip_C2b_Audio);
    //        timer = 0;
    //        skipAudioWait = false;
    //        tutorialC2bSubtitles = false;
    //        c1bTree = true;
    //    }

    //    //END DIALOGUE
    //    if (endDialogue == true && timer > timeGap)
    //    {
    //        canvasHandler.SetActive(false);
    //    }
    //} // End of Fixed Update

    //// Update is called once per frame
    void Update()
    {
        if (isVisible == false)
        {
            if (Input.GetKeyDown(KeyCode.Q) && interactableUI == true)
            {
                dialogueHandler.SetActive(true);
                isVisible = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (c1aTree == true)
                {
                    if (DialogueManager.GetComponent<Dialogue>().GetSelectedChoiceIndex() == 0) // Check to see if the first option is selected.
                    {
                        tutorialC1aSubtitles = true; 
                    }
                    else if (DialogueManager.GetComponent<Dialogue>().GetSelectedChoiceIndex() == 1) // Check to see if the second option is selected.
                    {
                        tutorialC1bSubtitles = true;
                    }
                    c1aTree = false;
                }
                else if (c2aTree == true)
                {
                    if (DialogueManager.GetComponent<Dialogue>().GetSelectedChoiceIndex() == 0) // Check to see if the first option is selected.
                    {
                        tutorialC2aSubtitles = true;
                    }
                    else if (DialogueManager.GetComponent<Dialogue>().GetSelectedChoiceIndex() == 1) // Check to see if the second option is selected.
                    {
                        tutorialC2bSubtitles = true;
                    }
                    c2aTree = false;
                }
                dialogueHandler.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            movementFlag = true;
        }

            if (progression == 1)
        {
            //Type(DialogueAssets.tutorial_gameDisplay_1);
        }
    }
}