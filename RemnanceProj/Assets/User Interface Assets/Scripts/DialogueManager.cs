using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class DialogueManager : MonoBehaviour
{
    public Animator armAnimator;
    public GameObject playerArms;

    private GameObject pictureCamera;

    public int test = 9; // TODO: DELETE ME
    private const int Q_INPUT = 1;
    private const int MOUSE_INPUT = 2;
    private const int WASD_INPUT = 60;
    private const int F_INPUT = 64;
    private const int PICTURE_INPUT = 448;

    private float charSize;
    private int maxCharsPerLine;
    private int selectionBoxPadding;

    private Text subtitlesText;
    private Text gameDisplayText;
    private Transform dialogChoicesTransform;
    private Transform selectedChoiceBackground;
    private TextMeshProUGUI choicesText;
    //private Scrollbar scrollBar;
    private Transform closeText;

    private int userInputFlag;
    private int requiredInput;

    private AudioSource audioSource;
    public AudioMixerGroup masterMixer;

    private TreeNode currentNode;

    private float timer;

    private string[] choices;
    private int selectedChoice;

    private bool waitingForInput;

    private float alphaFadeValue, fadeInTime;
    private Transform fadeInScreen;
    private bool doneFading;

    private bool isWristCommActive;
    private bool canOpenComm;

    private float nodeTimer;

    public bool inTutorial { get; private set; }
    private bool inEntrance;
    private bool inCave;
    private bool inEndGame;
    private bool saidNo; // TEMP REMOVE LATER

    public bool finalItemVisible;

    /// <summary>
    /// A dictionary whose keys are the indexes of the choices and whose values are the number of lines before it
    /// </summary>
    private Dictionary<int, int> currentChoices;

    private List<Tuple<TreeNode, string>> explorationChoices;

    private Vector3 oldMouseInput;

    void Awake()
    {
        pictureCamera = GameObject.FindGameObjectWithTag("PictureCamera");

        CheckGameStateValidity();

        subtitlesText = transform.GetChild(0).GetComponent<Text>();
        gameDisplayText = transform.GetChild(1).GetComponent<Text>();

        dialogChoicesTransform = transform.GetChild(2);
        selectedChoiceBackground = dialogChoicesTransform.GetChild(0);
        choicesText = dialogChoicesTransform.GetChild(1).GetComponent<TextMeshProUGUI>();
        choicesText.text = "";
        //scrollBar = dialogChoicesTransform.GetChild(2).GetComponent<Scrollbar>();
        closeText = dialogChoicesTransform.GetChild(3);
        closeText.gameObject.SetActive(false);

        userInputFlag = 0;
        requiredInput = 0;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.GetComponent<AudioSource>().outputAudioMixerGroup = masterMixer;

        subtitlesText.text = "";
        gameDisplayText.text = "";
        dialogChoicesTransform.gameObject.SetActive(false);

        // Animation Logic
        armAnimator.SetBool("InComms", false);

        timer = 0;
        choices = new string[0];
        selectedChoice = 0;

        waitingForInput = false;
        inTutorial = true;

        alphaFadeValue = 1;
        fadeInScreen = transform.GetChild(3);
        doneFading = false;

        isWristCommActive = false;
        canOpenComm = false;

        nodeTimer = 0;
        inEntrance = false;
        inCave = false;
        inEndGame = false;
        saidNo = false;

        currentChoices = new Dictionary<int, int>();
        explorationChoices = new List<Tuple<TreeNode, string>>();

        currentNode = CreateTutorialDialogueTree();
    }

    void Start()
    {
        if (FindObjectOfType<GameManager>() == null)
            Debug.Log("???");
        // For Animation
        playerArms.SetActive(false);

        AudioSource source2 = gameObject.AddComponent<AudioSource>();
        source2.volume = 0.05f;
        source2.loop = true;
        source2.clip = DialogueAssets.clip_sandstorm;
        source2.outputAudioMixerGroup = masterMixer;
        source2.Play();

        audioSource.clip = DialogueAssets.clip_intro_ramp;
        audioSource.Play();
        fadeInTime = DialogueAssets.clip_intro_ramp.length;

        float maxCharWidth = choicesText.fontSize * (4 / 3);
        float minCharWidth = choicesText.fontSize * (1 / 4);
        charSize = (maxCharWidth + minCharWidth) / 2;

        maxCharsPerLine = (int)(choicesText.gameObject.GetComponent<RectTransform>().rect.width / charSize) + 2; // make slightly longer
        selectionBoxPadding = 15;

        Debug.Log("Press: Y for site entrance, U for cave entrance, I for artifact picture... Press WASD to 'pick up' artifact for now");
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
                    subtitlesText.text = "";
                    canOpenComm = true;
                }
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }

        if (currentNode is TimerNode)
        {
            if (nodeTimer <= 0)
            {
                currentNode = CreateExplorationDialogueTree();
                nodeTimer = 0;
                gameDisplayText.text = "";
                inEntrance = false;
                canOpenComm = true;
                closeText.gameObject.SetActive(true);
            }
            else
            {
                nodeTimer -= Time.deltaTime;
            }
        }
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Y) && !inTutorial)
        {
            TriggerSiteEntrance();
        }

        if (Input.GetKeyDown(KeyCode.U) && !inTutorial)
        {
            TriggerCaveEntrance();
        }

        if (Input.GetKeyDown(KeyCode.I) && !inTutorial)
        {
            TriggerArtifactDialogue();
        }*/
        

        if (waitingForInput && !inTutorial && !inEntrance && !inCave)
        {
            // NOTE: Pressing Q or another input will remove this input...may be a problem for future
            if (Input.GetKeyDown(KeyCode.F))
            {
                if ((userInputFlag & 0x40) == 0)
                {
                    userInputFlag = userInputFlag | 0x40;
                }
                else
                {
                    userInputFlag = userInputFlag & 0x3F;
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                // If the F key is not active or the camera UI is active
                if ((userInputFlag & 0x40) == 0 || (userInputFlag & 0x80) != 0)
                {
                    // "Deactivate" camera UI
                    userInputFlag = userInputFlag & 0x7F;
                }
                else
                {
                    userInputFlag = userInputFlag | 0x80;
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                // If the F key is active and the camera UI is active
                if ((userInputFlag & 0x40) != 0 && (userInputFlag & 0x80) != 0)
                {
                    userInputFlag = userInputFlag | 0x100;
                }
            }
        }

        if ((inTutorial || inEndGame) && waitingForInput)
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
                playerArms.SetActive(true);
                pictureCamera.SetActive(false);
                dialogChoicesTransform.gameObject.SetActive(true);
                isWristCommActive = true;

                // Animation Logic
                armAnimator.SetBool("InComms", true);
            }

            if (inEndGame)
            {
                if (selectedChoice == 1)
                {
                    // "take" artifact
                    foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Pickup"))
                    {
                        if (obj.name == "FinalCube")
                            Destroy(obj);
                    }
                }
                else
                {
                    if (saidNo)
                    {
                        // "take" artifact
                        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Pickup"))
                        {
                            if (obj.name == "FinalCube")
                                Destroy(obj);
                        }
                    }
                    else
                    {
                        saidNo = true;
                    }
                }
            }

            waitingForInput = false;
            currentNode = currentNode.GetNextChild(selectedChoice);
            selectedChoice = 0;
            DisplayNextSentence();
            userInputFlag = userInputFlag & 0xC0;
        }

        if (!inTutorial && !inEntrance && !inEndGame)
        {
            if (Input.GetKeyDown(KeyCode.Q) && canOpenComm)
            {
                if (isWristCommActive)
                {
                    dialogChoicesTransform.gameObject.SetActive(false);
                    isWristCommActive = false;
                    canOpenComm = true;

                    // Animation Logic
                    armAnimator.SetBool("InComms", false);

                    if (selectedChoice != -1)
                    {
                        canOpenComm = false;
                        currentNode = explorationChoices[selectedChoice].Item1;
                        explorationChoices.RemoveAt(selectedChoice);

                        string[] newChoices = new string[explorationChoices.Count];
                        for (int i = 0; i < explorationChoices.Count; i++)
                        {
                            newChoices[i] = explorationChoices[i].Item2;
                        }

                        choices = newChoices;
                        UpdateChoices(-1, choices);

                        DisplayNextSentence();
                        selectedChoice = 0;
                    }
                }
                else
                {
                    dialogChoicesTransform.gameObject.SetActive(true);
                    closeText.gameObject.SetActive(true);
                    isWristCommActive = true;
                    selectedChoice = -1;

                    // Animation Logic
                    armAnimator.SetBool("InComms", true);

                    UpdateSelectionBackground();
                }
            }

            if (Input.mouseScrollDelta.y > 0 && isWristCommActive)
            {
                if (choices.Length == 0)
                {
                    selectedChoice = -1;
                    UpdateSelectionBackground();
                }
                else if (selectedChoice == -1)
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
            else if (Input.mouseScrollDelta.y < 0 && isWristCommActive)
            {
                if (choices.Length == 0 || selectedChoice + 1 == choices.Length)
                {
                    selectedChoice = -1;
                    UpdateSelectionBackground();
                }
                else
                {
                    selectedChoice = selectedChoice + 1;
                    UpdateSelectionBackground();
                }
            }
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

    void UpdateChoices(int index, params string[] input)
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

        selectedChoice = index;

        UpdateSelectionBackground();
    }

    void UpdateSelectionBackground()
    {
        choicesText.ForceMeshUpdate();
        Canvas.ForceUpdateCanvases();

        int verticalPadding = 0;

        float diff = 0;
        if (closeText.gameObject.activeSelf)
        {
            diff = closeText.GetComponent<RectTransform>().localPosition.y - choicesText.GetComponent<RectTransform>().localPosition.y;
        }

        if (selectedChoice == -1)
        {
            selectedChoiceBackground.transform.localPosition = new Vector3(choicesText.GetComponent<RectTransform>().anchoredPosition.x - selectionBoxPadding, closeText.GetComponent<RectTransform>().localPosition.y, 0);

            selectedChoiceBackground.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (int)((closeText.GetComponent<TextMeshProUGUI>().text.Length * charSize) + 2 * selectionBoxPadding + 5));
            selectedChoiceBackground.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, closeText.GetComponent<TextMeshProUGUI>().textInfo.lineInfo[0].lineHeight + verticalPadding + 5);

            //scrollBar.transform.localPosition = new Vector3(scrollBar.GetComponent<RectTransform>().anchoredPosition.x, choicesText.GetComponent<RectTransform>().localPosition.y + diff, 0);
            //scrollBar.transform.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, scrollBar.transform.GetComponent<RectTransform>().sizeDelta.x + diff);
            
            return;
        }

        int tempIndex = currentChoices[selectedChoice];
        float tempHeight = choicesText.textInfo.lineInfo[tempIndex].lineHeight + verticalPadding;
        int curLength = choices[selectedChoice].Length - choicesText.textInfo.lineInfo[tempIndex].characterCount;
        int maxLength = choices[selectedChoice].Length;

        float yOffset = -verticalPadding;
        int index;
        float scrollHeight = 0;
        for (index = 0; index < tempIndex; index++)
        { 
            yOffset += choicesText.textInfo.lineInfo[index].lineHeight;
            scrollHeight += choicesText.textInfo.lineInfo[index].lineHeight;
        }

        while (curLength > 2)
        {
            maxLength = maxCharsPerLine;

            curLength -= choicesText.textInfo.lineInfo[tempIndex].characterCount;
            tempHeight += choicesText.textInfo.lineInfo[tempIndex++].lineHeight;
        }

        int tempWidth = (int)(choicesText.GetComponent<RectTransform>().rect.width + 2 * selectionBoxPadding);//(int)((maxLength * charSize) + 2 * selectionBoxPadding);

        selectedChoiceBackground.transform.localPosition = new Vector3(choicesText.GetComponent<RectTransform>().anchoredPosition.x - selectionBoxPadding, choicesText.GetComponent<RectTransform>().localPosition.y - (yOffset) - test * selectedChoice, 0);

        selectedChoiceBackground.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, tempWidth);
        selectedChoiceBackground.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, tempHeight);

        for (; index < choicesText.textInfo.lineInfo[index].length; index++)
        {
            scrollHeight += choicesText.textInfo.lineInfo[index].lineHeight;
        }

        //scrollBar.transform.localPosition = new Vector3(scrollBar.GetComponent<RectTransform>().anchoredPosition.x, choicesText.GetComponent<RectTransform>().localPosition.y + verticalPadding, 0);
        //scrollBar.transform.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, scrollHeight + choicesText.textInfo.lineCount * verticalPadding);

        //iTween.MoveTo(textBackground, new Vector3(375, -1 * backgroundHeight * currentChoices[selectedChoiceIndex] + 240, 0), 1);
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

                // Animation Logic
                armAnimator.SetBool("InComms", false);
            }
            else
            {
                if (choices.Length == 1 && choices[0] == "")
                {
                    dialogChoicesTransform.gameObject.SetActive(false);
                    isWristCommActive = false;
                    subtitlesText.text = "";
                    gameDisplayText.text = "";

                    // Animation Logic
                    armAnimator.SetBool("InComms", false);
                }
                else
                {
                    dialogChoicesTransform.gameObject.SetActive(true);
                    UpdateChoices(choices);
                    isWristCommActive = true;

                    // Animation Logic
                    armAnimator.SetBool("InComms", true);
                }
            }
        }
        else if (currentNode is DialogueNode)
        {
            gameDisplayText.text = "";
            dialogChoicesTransform.gameObject.SetActive(false);
            isWristCommActive = false;

            // Animation Logic
            armAnimator.SetBool("InComms", false);

            DialogueNode node = (DialogueNode)currentNode;

            Tuple<string, AudioClip> temp = node.NextDialogue();

            if (temp == null)
            {
                currentNode = currentNode.GetNextChild(selectedChoice);

                if (currentNode is TimerNode)
                {
                    DisplayNextSentence();
                }
            }
            else
            {
                subtitlesText.text = temp.Item1;
                audioSource.clip = temp.Item2;
                audioSource.Play();
                timer = temp.Item2.length;
            }
        }
        else if (currentNode is TimerNode)
        {
            subtitlesText.text = "";

            TimerNode node = (TimerNode)currentNode;

            Tuple<string, float> temp = node.GetDisplayTime();
            gameDisplayText.text = temp.Item1;
            nodeTimer = temp.Item2;
            timer = temp.Item2;
            selectedChoice = -1;
            UpdateSelectionBackground();
        }
        else
        {
            subtitlesText.text = "";

            ChoiceNode node = (ChoiceNode)currentNode;

            TreeNode[] array = node.GetChildren();
            string[] newChoices = node.GetChoices();

            for (int i = array.Length - 1; i >= 0; i--)
            {
                explorationChoices.Insert(0, new Tuple<TreeNode, string>(array[i], newChoices[i]));
            }

            newChoices = new string[explorationChoices.Count];
            for (int i = 0; i < explorationChoices.Count; i++)
            {
                newChoices[i] = explorationChoices[i].Item2;
            }

            currentNode = null;

            choices = newChoices;
            UpdateChoices(-1, choices);
        }
    }

    public void TriggerSiteEntrance()
    {
        explorationChoices.Clear();
        dialogChoicesTransform.gameObject.SetActive(false);
        inEntrance = true;
        inTutorial = false;
        waitingForInput = false;
        selectedChoice = 0;
        currentNode = CreateSiteEntranceDialogueTree();

        // Play the looping sandstorm audio
        AudioSource source3 = gameObject.AddComponent<AudioSource>();
        source3.volume = 0.05f;
        source3.loop = true;
        source3.clip = DialogueAssets.clip_sandstormLoop;
        source3.outputAudioMixerGroup = masterMixer;
        source3.Play();

        // Animation Logic
        armAnimator.SetBool("InComms", false);
    }

    public void TriggerCaveEntrance()
    {
        explorationChoices.Clear();
        dialogChoicesTransform.gameObject.SetActive(false);
        waitingForInput = false;
        canOpenComm = false;
        inEntrance = false;
        inCave = true;
        selectedChoice = 0;
        subtitlesText.text = "";
        gameDisplayText.text = "";
        audioSource.Stop();
        timer = 0;
        currentNode = CreateCaveEntranceDialogueTree();

        // Animation Logic
        armAnimator.SetBool("InComms", false);
    }

    public void TriggerArtifactDialogue()
    {
        explorationChoices.Clear();
        dialogChoicesTransform.gameObject.SetActive(false);
        waitingForInput = false;
        canOpenComm = false;
        selectedChoice = 0;
        subtitlesText.text = "";
        gameDisplayText.text = "";
        audioSource.Stop();
        timer = 0;
        inEndGame = true;
        closeText.gameObject.SetActive(false);
        currentNode = CreateArtifactPictureDialogueTree();

        // Animation Logic
        armAnimator.SetBool("InComms", false);
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

    static TreeNode CreateSiteEntranceDialogueTree()
    {
        DialogueNode subtitle_entrance_node_1 = new DialogueNode(DialogueAssets.subtitle_entrance_1, DialogueAssets.clip_entrance_1, DialogueAssets.clip_entrance_2);
        DialogueNode subtitle_entrance_node_2 = new DialogueNode(DialogueAssets.subtitle_entrance_2, DialogueAssets.clip_entrance_3, DialogueAssets.clip_entrance_4, DialogueAssets.clip_entrance_5);
        DialogueNode subtitle_entrance_node_3 = new DialogueNode(DialogueAssets.subtitle_entrance_3, DialogueAssets.clip_entrance_6, DialogueAssets.clip_entrance_7, DialogueAssets.clip_entrance_8);
        DialogueNode subtitle_entrance_node_4 = new DialogueNode(DialogueAssets.subtitle_entrance_4, DialogueAssets.clip_entrance_9, DialogueAssets.clip_entrance_10);
        DialogueNode subtitle_entrance_node_5 = new DialogueNode(DialogueAssets.subtitle_entrance_5, DialogueAssets.clip_entrance_11, DialogueAssets.clip_entrance_12);
        DialogueNode subtitle_entrance_node_6 = new DialogueNode(DialogueAssets.subtitle_entrance_6, DialogueAssets.clip_entrance_13, DialogueAssets.clip_entrance_14);
        TimerNode gameDisplay_entrance_1_node = new TimerNode(DialogueAssets.gameDisplay_entrance_1, 4);

        subtitle_entrance_node_1.SetChildren(subtitle_entrance_node_2);
        subtitle_entrance_node_2.SetChildren(subtitle_entrance_node_3);
        subtitle_entrance_node_3.SetChildren(subtitle_entrance_node_4);
        subtitle_entrance_node_4.SetChildren(subtitle_entrance_node_5);
        subtitle_entrance_node_5.SetChildren(subtitle_entrance_node_6);
        subtitle_entrance_node_6.SetChildren(gameDisplay_entrance_1_node);

        return subtitle_entrance_node_1;
    }

    static TreeNode CreateExplorationDialogueTree()
    {
        ChoiceNode choice_tree_1_node = new ChoiceNode(DialogueAssets.choice_exploration_tree_1);

        // Choice - Relic Guardians?
        DialogueNode subtitle_tree_1_c1_node = new DialogueNode(DialogueAssets.subtitle_exploration_tree_1_c1, DialogueAssets.clip_exploration_tree_1_choice_1, DialogueAssets.clip_exploration_tree_1_c1_a, DialogueAssets.clip_exploration_tree_1_c1_b);

        // Choice - Can I document these buildings?
        DialogueNode subtitle_tree_1_c2_a_node = new DialogueNode(DialogueAssets.subtitle_exploration_tree_1_c2_a, DialogueAssets.clip_exploration_tree_1_choice_2, DialogueAssets.clip_exploration_tree_1_c2_a);
        DialogueNode subtitle_tree_1_c2_b_node = new DialogueNode(DialogueAssets.subtitle_exploration_tree_1_c2_b, DialogueAssets.clip_exploration_tree_1_c2_b, DialogueAssets.clip_exploration_tree_1_c2_c, DialogueAssets.clip_exploration_tree_1_c2_d, DialogueAssets.clip_exploration_tree_1_c2_e, DialogueAssets.clip_exploration_tree_1_c2_f);
        DialogueNode subtitle_tree_1_c2_c_node = new DialogueNode(DialogueAssets.subtitle_exploration_tree_1_c2_c, DialogueAssets.clip_exploration_tree_1_c2_g);

        ChoiceNode choice_tree_2_node = new ChoiceNode(DialogueAssets.choice_exploration_tree_2);

        // Choice - Are they...dangerous?
        DialogueNode subtitle_tree_2_c1_node = new DialogueNode(DialogueAssets.subtitle_exploration_tree_2_c1, DialogueAssets.clip_exploration_tree_2_choice_1, DialogueAssets.clip_exploration_tree_2_c1_a, DialogueAssets.clip_exploration_tree_2_c1_b, DialogueAssets.clip_exploration_tree_2_c1_c);

        // Choice - Is there anyway to shut them down or disable them?
        DialogueNode subtitle_tree_2_c2_a_node = new DialogueNode(DialogueAssets.subtitle_exploration_tree_2_c2_a, DialogueAssets.clip_exploration_tree_2_choice_2, DialogueAssets.clip_exploration_tree_2_c2_a);
        DialogueNode subtitle_tree_2_c2_b_node = new DialogueNode(DialogueAssets.subtitle_exploration_tree_2_c2_b, DialogueAssets.clip_exploration_tree_2_c2_b, DialogueAssets.clip_exploration_tree_2_c2_c, DialogueAssets.clip_exploration_tree_2_c2_d);

        // Choice - Camera Locked?
        DialogueNode subtitle_tree_1_c3_a_node = new DialogueNode(DialogueAssets.subtitle_exploration_tree_1_c3_a, DialogueAssets.clip_exploration_tree_1_choice_3_a, DialogueAssets.clip_exploration_tree_1_choice_3_b);
        PromptNode display1 = new PromptNode(DialogueAssets.subtitle_exploration_tree_1_c3_display_1, F_INPUT);
        DialogueNode subtitle_tree_1_c3_b_node = new DialogueNode(DialogueAssets.subtitle_exploration_tree_1_c3_b, DialogueAssets.clip_exploration_tree_1_choice_3_c);
        PromptNode display2 = new PromptNode(DialogueAssets.subtitle_exploration_tree_1_c3_display_2, PICTURE_INPUT);
        DialogueNode subtitle_tree_1_c3_c_node = new DialogueNode(DialogueAssets.subtitle_exploration_tree_1_c3_c, DialogueAssets.clip_exploration_tree_1_choice_3_d);

        choice_tree_1_node.SetChildren(subtitle_tree_1_c1_node, subtitle_tree_1_c2_a_node, subtitle_tree_1_c3_a_node);
        subtitle_tree_1_c1_node.SetChildren(choice_tree_2_node);
        subtitle_tree_1_c2_a_node.SetChildren(subtitle_tree_1_c2_b_node);
        subtitle_tree_1_c2_b_node.SetChildren(subtitle_tree_1_c2_c_node);
        choice_tree_2_node.SetChildren(subtitle_tree_2_c1_node, subtitle_tree_2_c2_a_node);
        subtitle_tree_2_c2_a_node.SetChildren(subtitle_tree_2_c2_b_node);
        subtitle_tree_1_c3_a_node.SetChildren(display1);
        display1.SetChildren(subtitle_tree_1_c3_b_node);
        subtitle_tree_1_c3_b_node.SetChildren(display2);
        display2.SetChildren(subtitle_tree_1_c3_c_node);

        return choice_tree_1_node;
    }

    static TreeNode CreateCaveEntranceDialogueTree()
    {
        DialogueNode initial_node = new DialogueNode(DialogueAssets.subtitle_cave_1, DialogueAssets.clip_cave_1, DialogueAssets.clip_cave_2, DialogueAssets.clip_cave_3);
        DialogueNode initial_node_2 = new DialogueNode(DialogueAssets.subtitle_cave_2, DialogueAssets.clip_cave_4, DialogueAssets.clip_cave_5a, DialogueAssets.clip_cave_5b, DialogueAssets.clip_cave_5c);

        ChoiceNode choice_node = new ChoiceNode(DialogueAssets.choice_cave_tree_1);

        DialogueNode tree_1_choice_1_node = new DialogueNode(DialogueAssets.subtitle_cave_tree_1_c1, DialogueAssets.clip_cave_tree_1_choice_1a, DialogueAssets.clip_cave_tree_1_choice_1b, DialogueAssets.clip_cave_tree_1_choice_1c, DialogueAssets.clip_cave_tree_1_choice_1d);
        DialogueNode tree_1_choice_2_node_a = new DialogueNode(DialogueAssets.subtitle_cave_tree_1_c2a, DialogueAssets.clip_cave_tree_1_choice_2a, DialogueAssets.clip_cave_tree_1_choice_2b, DialogueAssets.clip_cave_tree_1_choice_2c, DialogueAssets.clip_cave_tree_1_choice_2d);
        DialogueNode tree_1_choice_2_node_b = new DialogueNode(DialogueAssets.subtitle_cave_tree_1_c2b, DialogueAssets.clip_cave_tree_1_choice_2e, DialogueAssets.clip_cave_tree_1_choice_2f, DialogueAssets.clip_cave_tree_1_choice_2g, DialogueAssets.clip_cave_tree_1_choice_2h, DialogueAssets.clip_cave_tree_1_choice_2i);

        initial_node.SetChildren(initial_node_2);
        initial_node_2.SetChildren(choice_node);
        choice_node.SetChildren(tree_1_choice_1_node, tree_1_choice_2_node_a);
        tree_1_choice_2_node_a.SetChildren(tree_1_choice_2_node_b);

        return initial_node;
    }

    static TreeNode CreateArtifactPictureDialogueTree()
    {
        DialogueNode initial_node_1 = new DialogueNode(DialogueAssets.subtitle_artifact_initial_1, DialogueAssets.clip_artifact_1, DialogueAssets.clip_artifact_2);
        DialogueNode initial_node_2 = new DialogueNode(DialogueAssets.subtitle_artifact_initial_2, DialogueAssets.clip_artifact_3, DialogueAssets.clip_artifact_4);
        DialogueNode initial_node_3 = new DialogueNode(DialogueAssets.subtitle_artifact_initial_3, DialogueAssets.clip_artifact_5, DialogueAssets.clip_artifact_6);
        DialogueNode initial_node_4 = new DialogueNode(DialogueAssets.subtitle_artifact_initial_4, DialogueAssets.clip_artifact_7, DialogueAssets.clip_artifact_8);

        PromptNode tree_1_choice_node = new PromptNode(DialogueAssets.choice_artifact_tree_1, Q_INPUT);

        DialogueNode t1_c1_node_a = new DialogueNode(DialogueAssets.subtitle_artifact_tree_1_c_1, DialogueAssets.clip_artifact_t1_c1);
        DialogueNode t1_c1_node_b = new DialogueNode(DialogueAssets.subtitle_artifact_tree_1_c1_a, DialogueAssets.clip_artifact_t1_c1_a, DialogueAssets.clip_artifact_t1_c1_b);
        PromptNode t1_c1_choice_node = new PromptNode(new string[] { "" }, 0); // TODO: Change input to picking up object
        DialogueNode t1_c1_node_c = new DialogueNode(DialogueAssets.subtitle_artifact_tree_2, DialogueAssets.clip_artifact_t2);

        DialogueNode t1_c2_node_a = new DialogueNode(DialogueAssets.subtitle_artifact_tree_1_c_2, DialogueAssets.clip_artifact_t1_c2);
        DialogueNode t1_c2_node_b = new DialogueNode(DialogueAssets.subtitle_artifact_tree_1_c2_a, DialogueAssets.clip_artifact_t1_c2_a);
        PromptNode t1_c2_choice_node = new PromptNode(new string[] { "" }, 0); // TODO: Change input to picking up artifact
        DialogueNode t1_c2_node_c = new DialogueNode(DialogueAssets.substitle_artifact_tree_1_c2_b, DialogueAssets.clip_artifact_t1_c2_b);

        DialogueNode post_artifact = new DialogueNode(DialogueAssets.subtitle_artifact_retrieved, DialogueAssets.clip_artifact_retrieved_1, DialogueAssets.clip_artifact_retrieved_2, DialogueAssets.clip_artifact_retrieved_3);

        PromptNode news = new PromptNode(DialogueAssets.choice_artifact_tree_3, Q_INPUT);

        DialogueNode news_choice_1 = new DialogueNode(DialogueAssets.subtitle_artifact_tree_3_c_1, DialogueAssets.clip_artifact_t3_c1);
        DialogueNode news_choice_1_a = new DialogueNode(DialogueAssets.subtitle_artifact_tree_3_no_good_news, DialogueAssets.clip_artifact_no_good_news);
        DialogueNode news_choice_1_b = new DialogueNode(DialogueAssets.subtitle_artifact_tree_3_c_1_a, DialogueAssets.clip_artifact_t3_c1_a);
        DialogueNode news_choice_1_c = new DialogueNode(DialogueAssets.subtitle_artifact_tree_3_bad_news, DialogueAssets.clip_artifact_bad_news_a, DialogueAssets.clip_artifact_bad_news_b, DialogueAssets.clip_artifact_bad_news_c);
        DialogueNode news_choice_1_d = new DialogueNode(DialogueAssets.subtitle_artifact_tree_3_on_the_way, DialogueAssets.clip_artifact_on_the_way);

        DialogueNode news_choice_2 = new DialogueNode(DialogueAssets.subtitle_artifact_tree_3_c_2, DialogueAssets.clip_artifact_t3_c2);
        DialogueNode news_choice_2_a = new DialogueNode(DialogueAssets.subtitle_artifact_tree_3_bad_news, DialogueAssets.clip_artifact_bad_news_a, DialogueAssets.clip_artifact_bad_news_b, DialogueAssets.clip_artifact_bad_news_c);
        DialogueNode news_choice_2_b = new DialogueNode(DialogueAssets.subtitle_artifact_tree_3_c_2_a, DialogueAssets.clip_artifact_t3_c2_a);
        DialogueNode news_choice_2_c = new DialogueNode(DialogueAssets.subtitle_artifact_tree_3_no_good_news, DialogueAssets.clip_artifact_no_good_news);
        DialogueNode news_choice_2_d = new DialogueNode(DialogueAssets.subtitle_artifact_tree_3_on_the_way, DialogueAssets.clip_artifact_on_the_way);

        initial_node_1.SetChildren(initial_node_2);
        initial_node_2.SetChildren(initial_node_3);
        initial_node_3.SetChildren(initial_node_4);
        initial_node_4.SetChildren(tree_1_choice_node);

        tree_1_choice_node.SetChildren(t1_c1_node_a, t1_c2_node_a);

        t1_c1_node_a.SetChildren(t1_c1_node_b);
        t1_c1_node_b.SetChildren(t1_c1_choice_node);
        t1_c1_choice_node.SetChildren(t1_c1_node_c);
        t1_c1_node_c.SetChildren(post_artifact);

        t1_c2_node_a.SetChildren(t1_c2_node_b);
        t1_c2_node_b.SetChildren(t1_c2_choice_node);
        t1_c2_choice_node.SetChildren(t1_c2_node_c);
        t1_c2_node_c.SetChildren(post_artifact);

        post_artifact.SetChildren(news);
        news.SetChildren(news_choice_1, news_choice_2);

        news_choice_1.SetChildren(news_choice_1_a);
        news_choice_1_a.SetChildren(news_choice_1_b);
        news_choice_1_b.SetChildren(news_choice_1_c);
        news_choice_1_c.SetChildren(news_choice_1_d);

        news_choice_2.SetChildren(news_choice_2_a);
        news_choice_2_a.SetChildren(news_choice_2_b);
        news_choice_2_b.SetChildren(news_choice_2_c);
        news_choice_2_c.SetChildren(news_choice_2_d);

        return initial_node_1;
    }
}

abstract class TreeNode
{
    protected TreeNode[] children;

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
        if (sentences.Length != clips.Length)
        {
            Debug.LogError("Dialogue needs number of sentences to equal number of clips");
            Debug.Log("Sentences: " + sentences.Length + ", Clips: " + clips.Length);
        }

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

class TimerNode : TreeNode
{
    private string displayText;
    private float timeBeforeDisappear;

    public TimerNode(string text, float time)
    {
        displayText = text;
        timeBeforeDisappear = time;
    }

    public Tuple<string, float> GetDisplayTime()
    {
        return new Tuple<string, float>(displayText, timeBeforeDisappear);
    }
}

class ChoiceNode : TreeNode
{
    private string[] choices;

    public ChoiceNode(params string[] input)
    {
        choices = input;
    }

    public string[] GetChoices()
    {
        return choices;
    }

    public TreeNode[] GetChildren()
    {
        return children;
    }
}