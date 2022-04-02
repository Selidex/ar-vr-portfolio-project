using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SpeechParser : MonoBehaviour
{
    public Text Beginning;
    public Text Middle;
    public Text End;


    void 
    /// <summary>
    /// Gets name of speech from player prefs and calls method to parse speech
    /// </summary>
    Start()
    {
        /*GetSpeech("Macbeth");
        GetSpeech("Declaration of Independence");
        GetSpeech("I Have a Dream");*/
        string speechName = PlayerPrefs.GetString("speechName");
        GetSpeech(speechName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void 
    /// <summary>
    /// When given name of speech, searches Speech folder for script,
    /// parses into beginning, middle, and end, and assigns to appropriate variables
    /// </summary>
    GetSpeech(string SpeechName)
    {
        // Get speech text
        SpeechName = SpeechName.Replace(" ", "_");
        string allText = File.ReadAllText("Assets/Speeches/" + SpeechName);
        
        // Seperate by break points
        string[] parts = allText.Split('Ð');

        foreach (string item in parts)
        {
            Debug.Log(item);
        }

        // Assign to canvas text
        string error = "Error loading speech";
        try
        {
            Beginning.text = parts[0];
        }
        catch
        {
            Beginning.text = error;
        }
        try
        {
            Middle.text = parts[1];
        }
        catch
        {
            Beginning.text = error;
        }
        try
        {
            End.text = parts[2];
        }
        catch
        {
            Beginning.text = error;
        }
    }
}
