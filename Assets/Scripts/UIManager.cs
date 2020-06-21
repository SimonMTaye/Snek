using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private Text scoreDisplay;
    private bool paused = false;
    [SerializeField]
    private AudioClip buttonPress;
    [SerializeField]
    private AudioClip gameOver;
    private void Start() {
        UpdateScore(0);
    }

    public void Play(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        AudioSource.PlayClipAtPoint(buttonPress, Camera.main.transform.position);
        paused = false;
    }
    public void Pause(){
        pauseMenu.SetActive(true);
        AudioSource.PlayClipAtPoint(buttonPress, Camera.main.transform.position);
        Time.timeScale = 0;
        paused = true;
    }
    public void GameOver(int score){
        StartCoroutine(GameOverSequence(score));
    }
    private IEnumerator GameOverSequence(int score){
        AudioSource.PlayClipAtPoint(gameOver, Camera.main.transform.position);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.5f);
        PlayerPrefs.SetInt("Last Score", score);
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Home", LoadSceneMode.Single);
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
