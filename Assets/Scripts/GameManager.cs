using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private SpawnManager spawnManager;
    private GridManager gridManager; 
    //Determines how often blocks will be spawned
    public float blockSpawnDelay = 10f;
    private float blockTimer = 0f;

    public enum Difficulties
    {
        EASY,
        MEDIUM,
        HARD,
        VERY_HARD
    }
    private Difficulties _difficulty;
    
    // Start is called before the first frame update
    void Start()
    {
        //Spawn initial blocks
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        spawnManager.SpawnPellet();
        spawnManager.SpawnBlock();
    }

    // Update is called once per frame
    void Update()
    {
        blockTimer += Time.deltaTime;
        if (blockTimer >= blockSpawnDelay){
            blockTimer = 0;
            spawnManager.SpawnBlock();
        }
    }
    // Called when player consumes pellet. 
    public void PelletCollected(){
        StartCoroutine(PelletSpawner());
    }
    //Spawns a new pellet after a fixed delay
    IEnumerator PelletSpawner(){
        yield return new WaitForSeconds(5f);
        spawnManager.SpawnPellet();
    }
    //Called when player collides with self or block
    public void GameOver(){
        Time.timeScale = 0;
    }
}
