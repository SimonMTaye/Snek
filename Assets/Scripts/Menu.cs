using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject settingsPanel;
    private Scrollbar difficultySlider;
    private Scrollbar gridSizeSlider;

    private void Start() {
        //TODO Set scrollbar values based on play prefs
        //TODO Figoure out how to round up values to ints
        print((4 * 0.17));
    }

    public void ShowSettings(){
        settingsPanel.SetActive(true);
    }
    public void HideSettings(){
        settingsPanel.SetActive(false);
    }
    public void StartGame(){
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
    public void SetDifficulty(){
        //int difficulty = (int) (difficultySlider.value * difficultySlider.steps);
        int difficulty = 1;
        PlayerPrefs.SetInt("Difficulty", difficulty);
    }
    public void SetGridSize(){
        int size = 1;
        PlayerPrefs.SetInt("Grid Size", size);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)){
            HideSettings();
        }
    }
}
