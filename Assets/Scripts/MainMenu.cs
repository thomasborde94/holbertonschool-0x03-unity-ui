using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    #region Show in Inspector

    [SerializeField] Material trapMat;
    [SerializeField] Material goalMat;
    [SerializeField] Toggle colorblindMode;

    #endregion

    private void Start()
    {
        LoadColorblindToggleState(); // Sets toggle state according to its saved state
    }

    #region OnCLick Events
    public void PlayMaze()
    {
        if (colorblindMode.isOn)
        {
            trapMat.color = new Color32(255, 112, 0, 1);
            goalMat.color = Color.blue;
        }
        else
        {
            trapMat.color = originalTrapColor;
            goalMat.color = originalGoalColor;
        }

        SaveColorblindToggleState(); // Saves toggle state before loading new scene
        SceneManager.LoadScene("maze");
    }

    public void QuitMaze()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    #endregion


    private void SaveColorblindToggleState()
    {
        int toggleState = colorblindMode.isOn ? 1 : 0;
        PlayerPrefs.SetInt(toggleId, toggleState);
        PlayerPrefs.Save();
    }

    public void LoadColorblindToggleState()
    {
        int toggleState = PlayerPrefs.GetInt(toggleId, 0);
        if (toggleState == 1)
            colorblindMode.isOn = true;
        else
            colorblindMode.isOn = false;
    }

    #region Private

    private Color32 originalTrapColor = new Color32(255, 0, 0, 1);
    private Color32 originalGoalColor = new Color32(0, 255, 0, 1);
    private string toggleId = "colorblindModeId";

    #endregion
}
