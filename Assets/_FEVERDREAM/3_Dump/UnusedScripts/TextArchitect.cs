using System.Collections;
using UnityEngine;
using TMPro;

public class TextArchitect : MonoBehaviour
{
    #region TMPro config

    private TextMeshProUGUI textUI; // UI version of tmpro
    private TextMeshPro textWorld; // Worldspace version of tmpro
    public TMP_Text tmpro => textUI != null ? textUI : textWorld; // Assigns the correct tmpro type depending on which is being used


    public string CurrentText => tmpro.text;
    public string TargetText { get; private set; } = ""; // Default text is empty
    public string PreText { get; private set; } = ""; // Whatever text is currently there
    public int PreTextLength = 0;
    public string FullTargetText => PreText + TargetText; // The full text that needs to be built
    #endregion



    #region Text speed

    [SerializeField]
    private float textSpeed = .5f;

    public int CharactersPerCycle = 1;

    // Displays the entire text at once
    public void CompleteTextOnClick()
    {
        tmpro.text = FullTargetText;
        tmpro.ForceMeshUpdate();
        tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;
    }

    #endregion


    #region Aesthetics
    public Color TextColor { get { return tmpro.color; } set { tmpro.color = value; } } // Shortcut to change text colour
    #endregion


    // Allows UI specific or world space text to be used
    public TextArchitect(TextMeshProUGUI textUI)
    {
        this.textUI = textUI;
    }

    public TextArchitect(TextMeshPro textWorld)
    {
        this.textWorld = textWorld;
    }


    public Coroutine Build(string text)
    {
        PreText = ""; // Text should be empty at the start of a new line
        TargetText = text; // Target text is the text we're passing in

        StopBuilding(); // Makes sure nothing is currently being built

        BuildProcess = tmpro.StartCoroutine(BuildingTheText()); // Begins building the text

        return BuildProcess;
    }

    // Same thing for appending the text to what is already present in the text architect
    public Coroutine Append(string text)
    {
        PreText = tmpro.text;
        TargetText = text;

        StopBuilding();

        BuildProcess = tmpro.StartCoroutine(BuildingTheText());

        return BuildProcess;
    }

    private Coroutine BuildProcess = null; // Handles text generation
    public bool IsBuilding => BuildProcess != null;

    public void StopBuilding()
    {
        if (!IsBuilding)
        {
            return; // If we aren't building the text, there's nothing to stop
        }
        else
        {
            tmpro.StopCoroutine(BuildProcess); // Stop building the text
            BuildProcess = null; // No longer building
        }
    }


    public IEnumerator BuildingTheText()
    {
        PrepareTypewriterEffect();

        yield return TypewriterEffect();

        OnCompleteBuild();

    }

    private void OnCompleteBuild()
    {
        BuildProcess = null; // Makes sure the script doesn't think it's still building
    }

    private void PrepareTypewriterEffect()
    {
        tmpro.color = tmpro.color; // Makes sure the color is reset before beginning (in case we ever change the color of the text)
        tmpro.maxVisibleCharacters = 0; // No characters should be visible at the start of a new line
        tmpro.text = PreText;

        if(PreText != "")
        {
            tmpro.ForceMeshUpdate();
            tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;
        }

        tmpro.text += TargetText; // reveal characters one by one
        tmpro.ForceMeshUpdate();
    }

    private IEnumerator TypewriterEffect()
    {
        while(tmpro.maxVisibleCharacters < tmpro.textInfo.characterCount)
        {
            tmpro.maxVisibleCharacters += CharactersPerCycle;

            yield return new WaitForSeconds(0.015f / textSpeed); // Makes sure the change isn't instant
        }
    }
}
