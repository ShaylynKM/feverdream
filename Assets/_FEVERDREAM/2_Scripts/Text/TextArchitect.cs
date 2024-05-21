using System.Collections;
using UnityEngine;
using TMPro;

public class TextArchitect
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
    public float textSpeed { get { return textSpeedDefault * textSpeedUserDefined; } set { textSpeedUserDefined = value; } }
    private const float textSpeedDefault = 1;
    private float textSpeedUserDefined = 1;

    public bool CompleteTextOnClick = false;
    #endregion



    #region Aesthetics
    public Color TextColor { get { return tmpro.color; } set { tmpro.color = value; } } // Shortcut to change text colour
    #endregion

}
