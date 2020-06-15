using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private SpawnManager spawnManager;
    private GridManager gridManager; 
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

    public void SetDiffculty(Difficulties difficulty){
        this._difficulty = difficulty;
    }
    public void PelletCollected(){
        StartCoroutine(PelletSpawner());
    }
    IEnumerator PelletSpawner(){
        yield return new WaitForSeconds(5f);
        spawnManager.SpawnPellet();
    }
    public void GameOver(){
        Time.timeScale = 0;
    }
}
