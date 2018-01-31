using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class NumberMotor : MonoBehaviour {

    public DeathMenu deathMenu;
    public static AdManager AdInstance { set; get; }
    public GameObject pauseButton;

    private int[] selectedNumbers = new int[9];
    public NumberButtonMotor[] nbms = new NumberButtonMotor[9];
    private int score = 0;
    public Text scoreText;

    private int desiredResult;
    public Text resultText;

    private int[] multiplicationTableResults = {10, 14, 15, 16, 18, 20, 21, 24, 27, 28, 30, 32, 35, 36, 40, 42, 45, 48, 54, 56, 60, 63, 64, 70, 72/*, 80 ,
        84, 90, 96, 105, 108, 112, 120, 126, 135, 140, 144, 160, 162, 168, 180, 189, 192, 210, 216, 224, 240, 252, 270, 288, 324, 336, 378 */};

    private int operand = 1;       // 1 = +; 2 = *
    public Text operandText;

    private float maxTimer = 15.0f;
    private float timeLeft = 15.0f;
    public Text timer;

    private int roundsPlayed = 0;
    private int maxRounds = 15;
    private bool isPaused = false;

	// Use this for initialization
	void Start () {
        ChooseResult();
        pauseButton.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        scoreText.text = "Score: " + score.ToString();

        if (!isPaused)
        {
            if ((roundsPlayed < maxRounds))
            {

                timeLeft -= Time.deltaTime;
                if (timeLeft < 0.0f)
                {
                    ChooseResult();
                    roundsPlayed++;
                }
            }
            else
            {
                EndGame();
            }
        }
        timer.text = ((int)timeLeft).ToString() + "s";
    }

    public void ChooseResult()
    {
        //choose operand
        operand = Random.Range(1,3);       // 1 = +; 2 = *

        // choose result
        if (operand == 1)
        {
            operandText.text = "+";
            desiredResult = Random.Range(10, 32);
        } else if (operand == 2)
        {
            operandText.text = "*";
            desiredResult = multiplicationTableResults[Random.Range(0, multiplicationTableResults.Length)];
        }

        resultText.text = desiredResult.ToString();

        ResetNumbers();
    }

    public void SetNumber()
    {
        int n = EventSystem.current.currentSelectedGameObject.GetComponent<NumberButtonMotor>().GetNumber();
        if (selectedNumbers[n-1] == 0)
        {
            selectedNumbers[n-1] = n;
            EventSystem.current.currentSelectedGameObject.GetComponent<NumberButtonMotor>().ChangeColorToSelected();
        } else
        {
            selectedNumbers[n-1] = 0;
            EventSystem.current.currentSelectedGameObject.GetComponent<NumberButtonMotor>().ChangeColorToUnselected();
        }
        
        CalculateResult();
    }

    public bool CalculateResult()
    {
        int result = 0;

        if (operand == 1)
        {
            result = 0;
            for (int i = 0; i < selectedNumbers.Length; i++)
            {
                result += selectedNumbers[i];
            }
        } else if (operand == 2)
        {
            result = 1;
            for (int i = 0; i < selectedNumbers.Length; i++)
            {
                if (selectedNumbers[i] != 0)
                    result *= selectedNumbers[i];
            }
        }

        if (result == desiredResult)
        {
            score++;
            ChooseResult();
            roundsPlayed++;
            return true;
        }

        return false;
    } 

    public void ResetNumbers()
    {
        foreach (NumberButtonMotor nbm in nbms)
        {
            nbm.ChangeColorToUnselected();           
        }

        for (int i = 0; i < selectedNumbers.Length; i++)
        {
            selectedNumbers[i] = 0;
        }

        timeLeft = maxTimer;
    }

    public void PauseGame()
    {
        if (!isPaused)
        {
            deathMenu.ToggleEndMenu(score);
            isPaused = true;
        } else
        {
            deathMenu.UnToggleEndMenu();
            isPaused = false;
        }
    }

    private void EndGame()
    {
        pauseButton.SetActive(false);
        AdManager.AdInstance.ShowAd();
        deathMenu.ToggleEndMenu(score);
    }

}
