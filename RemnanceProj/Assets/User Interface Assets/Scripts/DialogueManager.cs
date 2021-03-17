using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private const int Q_INPUT = 1;
    private const int MOUSE_INPUT = 2;
    private const int WASD_INPUT = 60;

    private const float fontToPixelRatio = 4 / 3;
    private float charSize;
    private int maxCharsPerLine;
    private int selectionBoxPadding;

    private Text subtitlesText;
    private Text gameDisplayText;
    private Transform dialogChoicesTransform;
    private Transform selectedChoiceBackground;
    private TextMeshProUGUI choicesText;
    private Transform scrollBar;

    private int userInputFlag;
    private int requiredInput;

    private AudioSource audioSource;

    private TreeNode currentNode;

    private float timer;

    private string[] choices;
    private int selectedChoice;

    private bool waitingForInput;
    private bool inTutorial;

    private float alphaFadeValue, fadeInTime;
    private Transform fadeInScreen;
    private bool doneFading;

    private bool isWristCommActive;

    /// <summary>
    /// A dictionary whose keys are the indexes of the choices and whose values are the number of lines before it
    /// </summary>
    private Dictionary<int, int> currentChoices;

    private Vector3 oldMouseInput;

    void Awake()
    {
        CheckGameStateValidity();

        subtitlesText = transform.GetChild(0).GetComponent<Text>();
        gameDisplayText = transform.GetChild(1).GetComponent<Text>();

        dialogChoicesTransform = transform.GetChild(2);
        selectedChoiceBackground = dialogChoicesTransform.GetChild(0);
        choicesText = dialogChoicesTransform.GetChild(1).GetComponent<TextMeshProUGUI>();
        scrollBar = dialogChoicesTransform.GetChild(2);

        userInputFlag = 0;
        requiredInput = 0;

        audioSource = gameObject.AddComponent<AudioSource>();

        subtitlesText.text = "";
        gameDisplayText.text = "";
        dialogChoicesTransform.gameObject.SetActive(false);

        timer = 0;
        choices = new string[0];
        selectedChoice = 0;

        waitingForInput = false;
        inTutorial = true;

        alphaFadeValue = 1;
        fadeInScreen = transform.GetChild(3);
        doneFading = false;

        isWristCommActive = false;

        currentChoices = new Dictionary<int, int>();

        currentNode = CreateTutorialDialogueTree();
    }

    void Start()
    {
        AudioSource source2 = gameObject.AddComponent<AudioSource>();
        source2.volume = 0.05f;
        source2.loop = true;
        source2.clip = DialogueAssets.clip_sandstorm;
        source2.Play();

        audioSource.clip = DialogueAssets.clip_intro_ramp;
        audioSource.Play();
        fadeInTime = DialogueAssets.clip_intro_ramp.length;

        float maxCharWidth = choicesText.fontSize * (4 / 3);
        float minCharWidth = choicesText.fontSize * (1 / 4);
        charSize = (maxCharWidth + minCharWidth) / 2;

        maxCharsPerLine = (int)(choicesText.gameObject.GetComponent<RectTransform>().rect.width / charSize);
        selectionBoxPadding = 15;
    }

    void FixedUpdate()
    {
        if (!doneFading)
        {
            alphaFadeValue = Mathf.Clamp01(alphaFadeValue - Time.deltaTime / fadeInTime);
            fadeInScreen.GetComponent<Image>().color = new Color(0, 0, 0, alphaFadeValue);
            
            if (alphaFadeValue == 0)
            {
                doneFading = true;
                DisplayNextSentence();
            }
        }
        else
        {
            // Fading is Done
            if (timer <= 0 && !waitingForInput)
            {
                if (currentNode != null)
                {
                    DisplayNextSentence();
                }
                else
                {
                    inTutorial = false;
                    subtitlesText.text = "";
                }
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }

    void Update()
    {
        if (inTutorial && waitingForInput)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                userInputFlag = userInputFlag | 1;
            }

            if (Input.GetKeyUp(KeyCode.Q))
            {
                userInputFlag = userInputFlag & 0x3E;
            }

            if (Input.mouseScrollDelta.y > 0)
            {
                if (choices.Length == 0)
                {
                    selectedChoice = 0;
                }
                else if (selectedChoice == 0)
                {
                    selectedChoice = choices.Length - 1;
                    UpdateSelectionBackground();
                }
                else
                {
                    selectedChoice--;
                    UpdateSelectionBackground();
                }
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                if (choices.Length == 0)
                {
                    selectedChoice = 0;
                }
                else
                {
                    selectedChoice = (selectedChoice + 1) % choices.Length;
                    UpdateSelectionBackground();
                }
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                userInputFlag = userInputFlag | 0x4;
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                if (requiredInput != WASD_INPUT)
                    userInputFlag = userInputFlag & 0x3B;
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                userInputFlag = userInputFlag | 0x8;
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                if (requiredInput != WASD_INPUT)
                    userInputFlag = userInputFlag & 0x37;
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                userInputFlag = userInputFlag | 0x10;
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                if (requiredInput != WASD_INPUT)
                    userInputFlag = userInputFlag & 0x2F;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                userInputFlag = userInputFlag | 0x20;
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                if(requiredInput != WASD_INPUT)
                    userInputFlag = userInputFlag & 0x1F;
            }

            if (Input.mousePosition != oldMouseInput)
            {
                userInputFlag = userInputFlag | 0x2;
            }
            else
            {
                userInputFlag = userInputFlag & 0x3D;
            }

            oldMouseInput = Input.mousePosition;
        }

        if (waitingForInput && (userInputFlag & requiredInput) == requiredInput) 
        {
            if (!isWristCommActive)
            {
                dialogChoicesTransform.gameObject.SetActive(true);
                isWristCommActive = true;
            }

            waitingForInput = false;
            currentNode = currentNode.GetNextChild(selectedChoice);
            selectedChoice = 0;
            DisplayNextSentence();
            userInputFlag = 0;
        }
    }


    void UpdateChoices(params string[] input)
    {
        choicesText.text = "";
        currentChoices.Clear();

        int numLinesOffset = 0;
        for (int i = 0; i < input.Length; i++)
        {
            string decision = input[i];
            choicesText.text += decision + "\n";

            currentChoices.Add(i, numLinesOffset);

            numLinesOffset += ((decision.Length - 1) / maxCharsPerLine) + 1;
        }

        selectedChoice = 0;

        UpdateSelectionBackground();
    }

    void UpdateSelectionBackground()
    {
        choicesText.ForceMeshUpdate();
        Canvas.ForceUpdateCanvases();

        int verticalPadding = 5;

        int tempIndex = currentChoices[selectedChoice];
        float tempHeight = choicesText.textInfo.lineInfo[tempIndex].lineHeight + verticalPadding;
        int curLength = choices[selectedChoice].Length - choicesText.textInfo.lineInfo[tempIndex].characterCount;
        int maxLength = choices[selectedChoice].Length;

        float yOffset = verticalPadding;
        for (int i = 0; i < tempIndex; i++)
        { 
            yOffset += choicesText.textInfo.lineInfo[i].lineHeight;
        }

        while (curLength > 0)
        {
            maxLength = maxCharsPerLine;

            curLength -= choicesText.textInfo.lineInfo[tempIndex].characterCount;
            tempHeight += choicesText.textInfo.lineInfo[tempIndex++].lineHeight;
        }

        int tempWidth = (int)((maxLength * charSize) + 2 * selectionBoxPadding);

        selectedChoiceBackground.transform.localPosition = new Vector3(choicesText.GetComponent<RectTransform>().anchoredPosition.x - selectionBoxPadding, choicesText.GetComponent<RectTransform>().localPosition.y - (yOffset) * selectedChoice, 0);

        selectedChoiceBackground.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, tempWidth);
        selectedChoiceBackground.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, tempHeight);
    }

    void DisplayNextSentence()
    {
        if (currentNode is PromptNode)
        {
            waitingForInput = true;

            PromptNode node = (PromptNode)currentNode;
            Tuple<string, int> temp = node.GetPrompt();
            gameDisplayText.text = temp.Item1;
            requiredInput = temp.Item2;

            choices = node.GetChoices();

            if (choices == null)
            {
                dialogChoicesTransform.gameObject.SetActive(false);
                isWristCommActive = false;
                choices = new string[0];
            }
            else
            {
                dialogChoicesTransform.gameObject.SetActive(true);
                UpdateChoices(choices);
                isWristCommActive = true;
            }
        }
        else
        {
            gameDisplayText.text = "";
            dialogChoicesTransform.gameObject.SetActive(false);
            isWristCommActive = false;

            DialogueNode node = (DialogueNode)currentNode;

            Tuple<string, AudioClip> temp = node.NextDialogue();

            if (temp == null)
            {
                currentNode = currentNode.GetNextChild(selectedChoice);
            }
            else
            {
                subtitlesText.text = temp.Item1;
                audioSource.clip = temp.Item2;
                audioSource.Play();
                timer = temp.Item2.length;
            }
        }
    }

    /// <summary>
    /// Checks whether the the scene is set up properly (i.e. certain number of children with certain names, etc.)
    /// </summary>
    void CheckGameStateValidity()
    {
        if (transform.childCount != 4)
        {
            Debug.LogError("Not the correct amount of children on the DialogueManager. Did one get added or removed?");
        }

        Transform child = transform.GetChild(0);
        if (child.name != "Subtitles")
        {
            Debug.LogError("The subtitle child did not have the correct name. Did it get changed or did the children get rearranged?");
        }
        else if (child.GetComponent<Text>() == null)
        {
            Debug.LogError("The subtitle child does not have a text component...");
        }

        child = transform.GetChild(1);
        if (child.name != "GameDisplayTextBox")
        {
            Debug.LogError("The game display child did not have the correct name. Did it get changed or did the children get rearranged?");
        }
        else if (child.GetComponent<Text>() == null)
        {
            Debug.LogError("The game display child does not have a text component...");
        }

        child = transform.GetChild(2);
        if (child.name != "DialogueChoices")
        {
            Debug.LogError("The subtitle child did not have the correct name. Did it get changed or did the children get rearranged?");
        }

        Transform temp = child;
        if (temp.childCount < 3)
        {
            Debug.LogError("Not the correct amount of children on the DialogueChoices. Did one get added or removed?");
        }

        child = temp.GetChild(0);
        if (child.name != "Text Background")
        {
            Debug.LogError("The text background child did not have the correct name. Did it get changed or did the children get rearranged?");
        }

        child = temp.GetChild(1);
        if (child.name != "Dialogue Text")
        {
            Debug.LogError("The choice text child did not have the correct name. Did it get changed or did the children get rearranged?");
        }

        child = temp.GetChild(2);
        if (child.name != "UI - Scroll Bar")
        {
            Debug.LogError("The scroll bar child did not have the correct name. Did it get changed or did the children get rearranged?");
        }

        child = transform.GetChild(3);
        if (child.name != "FadeInScreen")
        {
            Debug.LogError("The FadeInScreen child did not have the correct name. Did it get changed or did the children get rearranged?");
        }

        if (child.GetComponent<Image>() == null || child.GetComponent<RectTransform>() == null)
        {
            Debug.LogError("The FadeInScreen child does not have proper components: Image, RectTransform");
        }
    }

    static TreeNode CreateTutorialDialogueTree()
    {
        DialogueNode subtitle_initial_node = new DialogueNode(DialogueAssets.subtitle_tutorial_initial, DialogueAssets.clip_tutorial_initial_P1, DialogueAssets.clip_tutorial_initial_H1, DialogueAssets.clip_tutorial_initial_H2, DialogueAssets.clip_tutorial_initial_H3);
        PromptNode gameDisplay_1_node = new PromptNode(DialogueAssets.gameDisplay_tutorial_1, Q_INPUT);

        PromptNode choice_tree_1_node = new PromptNode(DialogueAssets.gameDisplay_tutorial_2, Q_INPUT, DialogueAssets.choice_tutorial_tree_1);
        DialogueNode subtitle_tree_1_c_1_node = new DialogueNode(DialogueAssets.subtitle_tutorial_tree_1_c_1, DialogueAssets.clip_tutorial_tree_1_choice_1);
        DialogueNode subtitle_tree_1_c_2_node = new DialogueNode(DialogueAssets.subtitle_tutorial_tree_1_c_2, DialogueAssets.clip_tutorial_tree_1_choice_2);

        DialogueNode subtitle_pre_choice_1_tree_1_node = new DialogueNode(DialogueAssets.subtitle_tutorial_pre_choice_1_tree_1, DialogueAssets.clip_tutorial_tree_1_a_H1, DialogueAssets.clip_tutorial_tree_1_a_H2);
        PromptNode gameDisplay_3_node = new PromptNode(DialogueAssets.gameDisplay_tutorial_3, WASD_INPUT);
        DialogueNode subtitle_post_choice_1_tree_1_node = new DialogueNode(DialogueAssets.subtitle_tutorial_post_choice_1_tree_1, DialogueAssets.clip_tutorial_tree_1_a_H3);

        PromptNode choice_tree_1_a_node = new PromptNode(DialogueAssets.choice_tutorial_tree_1_a, Q_INPUT);
        DialogueNode subtitle_tree_1_a_c_1_node = new DialogueNode(DialogueAssets.subtitle_tutorial_tree_1_a_c_1, DialogueAssets.clip_tutorial_tree_2_choice_1);
        DialogueNode subtitle_tree_1_a_c_2_node = new DialogueNode(DialogueAssets.subtitle_tutorial_tree_1_a_c_2, DialogueAssets.clip_tutorial_tree_2_choice_2);

        DialogueNode subtitle_pre_choice_1_tree_1_a_node = new DialogueNode(DialogueAssets.subtitle_tutorial_pre_choice_1_tree_1_a, DialogueAssets.clip_tutorial_tree_2_a_H1, DialogueAssets.clip_tutorial_tree_2_a_H2);
        PromptNode gameDisplay_4_node = new PromptNode(DialogueAssets.gameDisplay_tutorial_4, MOUSE_INPUT);
        DialogueNode subtitle_post_choice_1_tree_1_a_node = new DialogueNode(DialogueAssets.subtitle_tutorial_post_choice_1_tree_1_a, DialogueAssets.clip_tutorial_tree_2_a_P1);

        DialogueNode subtitle_end_tree_node = new DialogueNode(DialogueAssets.subtitle_tutorial_end_tree, DialogueAssets.clip_tutorial_end_P1, DialogueAssets.clip_tutorial_end_H1);

        subtitle_initial_node.SetChildren(gameDisplay_1_node);
        gameDisplay_1_node.SetChildren(choice_tree_1_node);
        choice_tree_1_node.SetChildren(subtitle_tree_1_c_1_node, subtitle_tree_1_c_2_node);
        subtitle_tree_1_c_1_node.SetChildren(subtitle_pre_choice_1_tree_1_node);
        subtitle_tree_1_c_2_node.SetChildren(subtitle_end_tree_node);
        subtitle_pre_choice_1_tree_1_node.SetChildren(gameDisplay_3_node);
        gameDisplay_3_node.SetChildren(subtitle_post_choice_1_tree_1_node);
        subtitle_post_choice_1_tree_1_node.SetChildren(choice_tree_1_a_node);
        choice_tree_1_a_node.SetChildren(subtitle_tree_1_a_c_1_node, subtitle_tree_1_a_c_2_node);
        subtitle_tree_1_a_c_1_node.SetChildren(subtitle_pre_choice_1_tree_1_a_node);
        subtitle_tree_1_a_c_2_node.SetChildren(subtitle_end_tree_node);
        subtitle_pre_choice_1_tree_1_a_node.SetChildren(gameDisplay_4_node);
        gameDisplay_4_node.SetChildren(subtitle_post_choice_1_tree_1_a_node);
        subtitle_post_choice_1_tree_1_a_node.SetChildren(subtitle_end_tree_node);

        return subtitle_initial_node;
    }
}

abstract class TreeNode
{
    private TreeNode[] children;

    public TreeNode()
    {
        children = new TreeNode[] { null };
    }

    public TreeNode(params TreeNode[] _children)
    {
        children = _children;
    }

    public void SetChildren(params TreeNode[] _children)
    {
        children = _children;
    }

    public TreeNode GetNextChild(int index)
    {
        return children[index];
    }
}

class DialogueNode : TreeNode 
{
    private string[] sentencesToDisplay;
    private AudioClip[] audioClips;
    private int currentSentence;
    
    public DialogueNode(string[] sentences, params AudioClip[] clips)
    {
        sentencesToDisplay = sentences;
        audioClips = clips;
        currentSentence = 0;
    }

    public Tuple<string, AudioClip> NextDialogue()
    {
        if(currentSentence >= sentencesToDisplay.Length)
            return null;
        return new Tuple<string, AudioClip>(sentencesToDisplay[currentSentence], audioClips[currentSentence++]);
    }
}

class PromptNode : TreeNode
{
    private string promptText;
    private int requiredInput;
    private string[] choices;

    public PromptNode(string text, int input)
    {
        promptText = text;
        requiredInput = input;
        choices = null;
    }

    public PromptNode(string[] _choices, int input)
    {
        promptText = "";
        requiredInput = input;
        choices = _choices;
    }

    public PromptNode(string text, int input, string[] _choices)
    {
        promptText = text;
        requiredInput = input;
        choices = _choices;
    }

    public Tuple<string, int> GetPrompt()
    {
        return new Tuple<string, int>(promptText, requiredInput);
    }

    public string[] GetChoices()
    {
        return choices;
    }
}