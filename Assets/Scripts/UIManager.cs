using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private Text scoreDisplay;
    private bool paused = false;

    private void Start() {
        UpdateScore(0);
    }

    public void Play(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        paused = false;
    }
    public void Pause(){
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        paused = true;
    }
    public void GameOver(int score){
        PlayerPrefs.SetInt("Last Score", score);
    }

    public void MainMenu(){

    }

    public void UpdateScore(int score){
        scoreDisplay.text = "" + score;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(paused){
                Play();
            } else {
                Pause();
            }
        }
    }
}
