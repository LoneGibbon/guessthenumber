using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text numberText, promptText, turnsLeftText, highScoreText;

    private int correctNumber;

    private int turnsLeft = 10;

    public InputField numberInputField;

    public Button checkNumberButton;

    private void Awake()
    {
        correctNumber = Random.Range(1, 20);

        turnsLeftText.text = "Turns left: " + turnsLeft;

        Debug.Log("Correct number is: " + correctNumber);

        if(!PlayerPrefs.HasKey("highScore")){
            PlayerPrefs.SetInt("highScore", 0);
        }

        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("highScore");
    }

    public void CheckNumber(){
        if(numberInputField.text == ""){
            promptText.text = "Please input a number before checking!";
        }
        else if(int.Parse(numberInputField.text) < correctNumber){
            promptText.text = "Guess is too low!";
            turnsLeft--;

            if(turnsLeft == 0)
                GameOver();
            else
                turnsLeftText.text = "Turns left: " + turnsLeft;
        }
        else if(int.Parse(numberInputField.text) > correctNumber){
            promptText.text = "Guess is too high!";
            turnsLeft--;

            if(turnsLeft == 0)
                GameOver();
            else
                turnsLeftText.text = "Turns left: " + turnsLeft;
        }
        else if(int.Parse(numberInputField.text) == correctNumber){
            promptText.text = "That's the correct guess! You win!";
            numberText.text = correctNumber.ToString();
            Camera.main.backgroundColor = new Color32(69, 196, 69, 255);
            if(PlayerPrefs.GetInt("highScore") < turnsLeft){
                PlayerPrefs.SetInt("highScore", turnsLeft);
            }
            highScoreText.text = "High Score: " + PlayerPrefs.GetInt("highScore"); 
        }
    }

    public void GameOver(){
        promptText.text = "Sorry, you were not able to guess the number! Press restart to try again!";
        turnsLeftText.gameObject.SetActive(false);
        Camera.main.backgroundColor = new Color32(196, 69, 69, 255);
        checkNumberButton.interactable = false;
        numberText.text = correctNumber.ToString();
    }

    public void ReloadGame(){
        SceneManager.LoadScene(0);
    }
}
