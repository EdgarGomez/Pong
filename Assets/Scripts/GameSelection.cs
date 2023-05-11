using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSelection : MonoBehaviour
{

    private bool isAbout = false;
    public GameObject Buttons;
    public GameObject AboutText;

    public TMP_Dropdown paddle1ColorDropdown;
    public TMP_Dropdown paddle2ColorDropdown;
    public TMP_Dropdown gameModeDropdown;
    public TMP_Dropdown numberGoalsDropdown;
    public TMP_Dropdown difficultyDropdown;

    private void Start()
    {
        paddle1ColorDropdown.value = PlayerPrefs.HasKey("Paddle1Color") ? PlayerPrefs.GetInt("Paddle1Color") : 0;
        paddle2ColorDropdown.value = PlayerPrefs.HasKey("Paddle2Color") ? PlayerPrefs.GetInt("Paddle2Color") : 0;
        gameModeDropdown.value = PlayerPrefs.HasKey("GameMode") ? PlayerPrefs.GetInt("GameMode") : 0;
        numberGoalsDropdown.value = PlayerPrefs.HasKey("NumberGoals") ? PlayerPrefs.GetInt("NumberGoals") : 0;
        difficultyDropdown.value = PlayerPrefs.HasKey("Difficulty") ? PlayerPrefs.GetInt("Difficulty") : 0;
    }


    public void paddle1Color(int mode)
    {
        PlayerPrefs.SetInt("Paddle1Color", mode);
    }

    public void paddle2Color(int mode)
    {
        PlayerPrefs.SetInt("Paddle2Color", mode);
    }

    public void gameMode(int mode)
    {
        PlayerPrefs.SetInt("GameMode", mode);
    }

    public void numberGoals(int mode)
    {
        PlayerPrefs.SetInt("NumberGoals", mode);
    }
    public void difficultyLevel(int mode)
    {
        PlayerPrefs.SetInt("Difficulty", mode);
    }


    public void StartGame()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene("Pong");
    }

    public void IsAbout()
    {
        isAbout = !isAbout;
        if (isAbout)
        {
            Buttons.SetActive(false);
            AboutText.SetActive(true);
        }
        else
        {
            Buttons.SetActive(true);
            AboutText.SetActive(false);
        }
    }
}

