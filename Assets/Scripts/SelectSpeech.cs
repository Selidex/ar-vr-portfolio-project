using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectSpeech : MonoBehaviour
{
    public GameObject DropdownMenu;
    public string DefaultSpeech;
    private bool modified = false;

    public void
    /// <summary>
    /// Given input from dropdown, updates player prefs of speech
    /// </summary>
    SetSpeech()
    {
        string speechName = DropdownMenu.GetComponent<Text>().text;
        Debug.Log(speechName);

        PlayerPrefs.SetString("speechName", speechName);
        modified = true;
    }

    public void 
    /// <summary>
    /// Loads game
    /// </summary>
    OpenGame()
    {
        if (!modified)
            PlayerPrefs.SetString("speechName", DefaultSpeech);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetString("speechName"));
        SceneManager.LoadScene("VR Room");
    }
}
