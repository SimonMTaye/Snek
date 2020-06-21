using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private UIManager uIManager;
    private SpawnManager spawnManager;
    private GridManager gridManager;
    private SnakeHead player;
    //Determines how often blocks will be spawned
    private float blockSpawnDelay;
    private float pelletSpawnDelay = 5f;
    private float blockTimer = 0f;
    private float scoreMultiplier;
    private int score;
    private int scorePerPellet;
    private float speedIncreaseRate;
    public enum Difficulties
    {
        EASY = 0,
        MEDIUM,
        HARD,
        VERY_HARD
    }
    private Difficulties _difficulty;
    // Set difficulty based on player settings and initilaize score to 0;
    private void Awake() {
        score = 0;
        _difficulty = (Difficulties)PlayerPrefs.GetInt("Difficulty", 0);
        SetValuesBasedOnDifficulty();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //Restart time incase game was quitted while paused
        Time.timeScale = 1;
        //Spawn initial blocks
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        player = GameObject.Find("Snek").GetComponent<SnakeHead>();
        uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        spawnManager.SpawnPellet(false);
        spawnManager.SpawnBlock();
        //Start Coroutines
        StartCoroutine(BlockSpawner());
        StartCoroutine(SpeedIncreaser());
        StartCoroutine(ScoreIncreaser());
    }

    // Update is called once per frame
    void Update()
    {
   
    }
    // Called when player consumes pellet. 
    public void PelletCollected(){
        IncreaseScore(scorePerPellet);
        StartCoroutine(PelletSpawner());
    }
    //Spawns a new pellet after a fixed delay
    IEnumerator PelletSpawner(){
        yield return new WaitForSeconds(pelletSpawnDelay);
        spawnManager.SpawnPellet();
    }
    //Spawns a blocks ever n seconds. 
    //Delay is determined by diffculty settings
    IEnumerator BlockSpawner(){
        yield return new WaitForSeconds(blockSpawnDelay);
        spawnManager.SpawnBlock();
        StartCoroutine(BlockSpawner());
    }
    //Increases score every second
    IEnumerator ScoreIncreaser(){
        yield return new WaitForSeconds(1f);
        IncreaseScore(10);
        StartCoroutine(ScoreIncreaser());
    }
    //Increases player speed by n ever second
    //Determined by diffculty settings
    IEnumerator SpeedIncreaser(){
        yield return new WaitForSeconds(1f);
        player.speed += speedIncreaseRate;
        StartCoroutine(SpeedIncreaser());
    }
    //Called when player collides with self or block
    public void GameOver(){
        uIManager.GameOver(score);
    }

    private void IncreaseScore(int increaseValue){
        score += (int)(increaseValue * scoreMultiplier);
        uIManager.UpdateScore(score);
    }
    //Adjust game values based on selected difficulty values
    private void SetValuesBasedOnDifficulty(){
            switch (_difficulty) {
            case Difficulties.EASY:
                blockSpawnDelay = 10f;
                scoreMultiplier = 1f;
                scorePerPellet = 200;
                speedIncreaseRate = 0f;
                break;
            case Difficulties.MEDIUM:
                blockSpawnDelay = 8f;
                scoreMultiplier = 1.5f;
                scorePerPellet = 180;
                pelletSpawnDelay = 4f;
                speedIncreaseRate = 0.05f;
                break;
            case Difficulties.HARD:
                blockSpawnDelay = 6f;
                scoreMultiplier = 2f;
                scorePerPellet = 160;
                pelletSpawnDelay = 4f;
                speedIncreaseRate = 0.1f;
                break;
            case Difficulties.VERY_HARD:
                blockSpawnDelay = 4f;
                scoreMultiplier = 3f;
                scorePerPellet = 150;
                pelletSpawnDelay = 3f;
                speedIncreaseRate = 0.2f;
                break;
        }
    }
}
