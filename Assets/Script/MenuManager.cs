﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public Text scoreText;
    private int score;

    private void Start()
    {

        score = PlayerPrefs.GetInt("PlayerScore");
        scoreText.text = "Highscore: " + score.ToString();
    }

    public void ToColorGame()
    {
        SceneManager.LoadScene("ColorGame");
    }

    public void ToNumberGame()
    {
        SceneManager.LoadScene("NumberGame");
    }
}
