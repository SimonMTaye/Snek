using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject settingsPanel;
    [SerializeField]
    private Scrollbar difficultySlider;
    [SerializeField]
    private Scrollbar gridSizeSlider;
    [SerializeField]
    private Scrollbar touchControlSlider;
    [SerializeField]
    private AudioClip buttonPress;

    private void Start() {
        if(PlayerPrefs.GetInt("First Run", 0) == 0){
            PlayerPrefs.SetInt("Difficulty", 1);
            PlayerPrefs.SetInt("Grid Size", 2);
            PlayerPrefs.SetInt("First Run", 1);
            PlayerPrefs.SetInt("Touch Control", 2);
        }
        difficultySlider.value = (float)PlayerPrefs.GetInt("Difficulty") / (difficultySlider.numberOfSteps - 1);
        gridSizeSlider.value = ((float)PlayerPrefs.GetInt("Grid Size")) / (gridSizeSlider.numberOfSteps -1);        
        touchControlSlider.value = ((float)PlayerPrefs.GetInt("Touch Control")) / (touchControlSlider.numberOfSteps -1);
    }

    public void ShowSettings(){
        AudioSource.PlayClipAtPoint(buttonPress, Camera.main.transform.position);
        settingsPanel.SetActive(true);
    }
    public void HideSettings(){
        AudioSource.PlayClipAtPoint(buttonPress, Camera.main.transform.position);
        settingsPanel.SetActive(false);
    }
    public void StartGame(){
        AudioSource.PlayClipAtPoint(buttonPress, Camera.main.transform.position);
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
    public void SetDifficulty(){
        int difficulty = Mathf.RoundToInt(difficultySlider.value * (difficultySlider.numberOfSteps -1));
        PlayerPrefs.SetInt("Difficulty", difficulty);
    }
    public void SetGridSize(){
        int size = Mathf.RoundToInt(gridSizeSlider.value * (gridSizeSlider.numberOfSteps -1 ));
        PlayerPrefs.SetInt("Grid Size", size);
    }

    public void SetTouchControl(){
        int touchChoice = Mathf.RoundToInt(touchControlSlider.value * (touchControlSlider.numberOfSteps -1 ));
        PlayerPrefs.SetInt("Touch Control", touchChoice);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)){
            HideSettings();
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            StartGame();
        }
    }
}
