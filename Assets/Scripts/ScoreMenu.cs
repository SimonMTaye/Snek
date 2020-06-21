using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ScoreMenu : MonoBehaviour
{
    [SerializeField]
    private Text highScoredDisplay;
    [SerializeField]
    private Text scoreDisplay;
    [SerializeField]
    private AudioClip buttonPress;
    private int score;
    void Start()
    {
        score = PlayerPrefs.GetInt("Last Score");
        DisplayAndSetHighScore();
        DisplayScore();
    }

    public void MainMenu(){
        AudioSource.PlayClipAtPoint(buttonPress, Camera.main.transform.position);
        SceneManager.LoadScene("Home", LoadSceneMode.Single);
    }

    public void RestartGame(){
        AudioSource.PlayClipAtPoint(buttonPress, Camera.main.transform.position);
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    private void DisplayAndSetHighScore(){
        if (PlayerPrefs.HasKey("High Score")){
            int highScore = PlayerPrefs.GetInt("High Score");
            if(highScore > score) {
                highScoredDisplay.text = "High Score: " + highScore;
            } else {
                highScoredDisplay.text = "Congratulations! You set a new high score!";
                PlayerPrefs.SetInt("High Score", score);
            }
        } else {
            highScoredDisplay.gameObject.SetActive(false);
            PlayerPrefs.SetInt("High Score", score);
        }
    }

    private void DisplayScore(){
        scoreDisplay.text = "" + score;
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)){
            RestartGame();
        }
    }
}
