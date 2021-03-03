using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Dialogue : MonoBehaviour
{
    public string[] sentences; // For testing

    public TextMeshProUGUI textDisplay;
    private int selectedChoiceIndex;
    public GameObject textBackground;
    public int yOffset;
    public Scrollbar scrollBar;

    private string[] choices;

    private int numLinesOffset;

    /// <summary>
    /// A dictionary whose keys are the indexes of the choices and whose values are the number of lines before it
    /// </summary>
    private Dictionary<int, int> currentChoices;

    private int maxCharsPerLine;
    private int backgroundHeight;

    private void Awake()
    {
        currentChoices = new Dictionary<int, int>();
        backgroundHeight = 32; // Change depending on font size
    }

    private void Start()
    {
        // Get the maximum chars per line based on the font size and textbox width
        float maxCharWidth = textDisplay.fontSize * (4 / 3);
        float minCharWidth = textDisplay.fontSize * (1 / 4);
        float charPixels = (maxCharWidth + minCharWidth) / 2;

        maxCharsPerLine = (int)(textDisplay.GetComponent<RectTransform>().rect.width / charPixels);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //Changes the selected sentence based on the player input (either up or down)
            if (selectedChoiceIndex == 0)
            {
                selectedChoiceIndex = choices.Length - 1;
            }
            else
            {
                selectedChoiceIndex--;
            }

            UpdateTextBackground();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //Changes the selected sentence based on the player input (either up or down)
            selectedChoiceIndex = (selectedChoiceIndex + 1) % choices.Length;

            UpdateTextBackground();
        }
    }

    /// <summary>
    /// Update the choices based on up to four inputted strings
    /// </summary>
    /// <param name="input_choices">the inputted choices</param>
    public void UpdateChoices(params string[] input_choices)
    {
        choices = new string[input_choices.Length];
        currentChoices.Clear();
        textDisplay.text = string.Empty;

        numLinesOffset = 0;
        for (int i = 0; i < choices.Length; i++)
        {
            string decision = input_choices[i];

            // Adds the decision/choice to the textbox
            textDisplay.text += decision + "\n";

            // Adds the decision to the array
            choices[i] = decision;

            currentChoices.Add(i, numLinesOffset);

            if (decision.Length > maxCharsPerLine)
            {
                numLinesOffset += 2;
            }
            else
            {
                numLinesOffset += 1;
            }
        }

        selectedChoiceIndex = 0;
        UpdateTextBackground();

        // Scales the background component of the ScrollBar
        scrollBar.transform.localScale = new Vector3(numLinesOffset * 0.2f + 0.2f, 1, 1);
    }

    /// <summary>
    /// Update the text background based on the selected choice
    /// </summary>
    private void UpdateTextBackground()
    {
        int tempHeight = backgroundHeight;
        int tempWidth = 0;
        int offset = 0;

        // Set the height, width, and offset based on whether the choice has one or two lines
        if (choices[selectedChoiceIndex].Length > maxCharsPerLine)
        {
            tempHeight *= 2;
            tempWidth = (int)(textDisplay.GetComponent<RectTransform>().rect.width + 30);

            offset = yOffset;

            // Scales the inner component of the ScrollBar
            scrollBar.size = 0.225f * 8 / numLinesOffset;
        }
        else
        {
            tempWidth = (int)(choices[selectedChoiceIndex].Length * textDisplay.fontSize * (4 / 3) / 2) + 30;

            offset = -2;

            // Scales the inner component of the ScrollBar
            scrollBar.size = 0.1125f * 8 / numLinesOffset;
        }

        //iTween.MoveTo(textBackground, new Vector3(375, -1 * backgroundHeight * currentChoices[selectedChoiceIndex] + 240, 0), 1);
        textBackground.transform.localPosition = new Vector3(-115, -1 * backgroundHeight * currentChoices[selectedChoiceIndex] + offset, 0);

        textBackground.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, tempWidth);
        textBackground.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, tempHeight);

        // Change the location of the ScrollBar
        scrollBar.value = (currentChoices[selectedChoiceIndex] * 0.34f / 2) * 8 / numLinesOffset;
    }

    public int GetSelectedChoiceIndex()
    {
        return selectedChoiceIndex;
    }

}