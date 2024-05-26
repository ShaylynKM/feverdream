using Ink.Runtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InkTest : MonoBehaviour
{
    public TextAsset inkJSON;
    private Story _story;

    public TextMeshProUGUI StoryText;
    public Button ChoiceButton;

    public TextArchitect Architect;


    private void Awake()
    {
        _story = new Story(inkJSON.text); // Creates the story object
        Architect = GetComponent<TextArchitect>();

    }

    private void Start()
    {
        RefreshUI();
    }

    void RefreshUI()
    {
        EraseUI(); // Clears any previous UI elements
        StoryText.text = LoadStoryChunk(); // Loads the next part of the story
        foreach (Choice choice in _story.currentChoices)
        {
            // Instantiates new buttons for each choice option, sets the transform to be the child of this object. IMPORTANT. This object is the object that should have a vertical layout group.
            Button choiceButton = Instantiate(ChoiceButton) as Button;
            choiceButton.transform.SetParent(this.transform, false);

            // Sets the text on the buttons to the choices availible
            TextMeshProUGUI choiceText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
            choiceText.text = choice.text;

            // Removes any previous listeners
            choiceButton.onClick.RemoveAllListeners();

            // Adds new listeners (using another local variable. Just using "choice" caused some weird behaviour)
            Choice localChoice = choice;
            choiceButton.onClick.AddListener(delegate {
                ChooseStoryChoice(localChoice);
            });
        }
    }

    void EraseUI()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }
    }

    private void ChooseStoryChoice(Choice choice)
    {
        _story.ChooseChoiceIndex(choice.index);
        RefreshUI();
    }

    string LoadStoryChunk()
    {
        string text = "";

        if (_story.canContinue)
        {
            text = _story.ContinueMaximally();
        }

        return text;
    }

}
