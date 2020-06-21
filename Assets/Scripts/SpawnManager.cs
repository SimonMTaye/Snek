using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject blockPrefab;
    [SerializeField]
    private GameObject growthPellet;
    [SerializeField]
    private AudioClip pelletSpawnedAudio;
    [SerializeField]
    private GameObject grid;
    private GridManager gridManager;

    // Start is called before the first frame update
    void Start() {
    }
    public void SpawnPellet(bool audio = true){
        GameObject pellet = Instantiate(growthPellet, new Vector3(1000, 1000), Quaternion.identity, grid.transform);
        if (PlayerPrefs.GetInt("Grid Size") <= 1)
        {
            pellet.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        }
        pellet.transform.localPosition = GenerateRandomPosition();
        if (audio)
        {
            AudioSource.PlayClipAtPoint(pelletSpawnedAudio, Camera.main.transform.position);
        }
    }
    public void SpawnBlock(){
        GameObject block = Instantiate(blockPrefab, new Vector3(1000, 1000), Quaternion.identity, grid.transform);
        block.transform.localPosition = GenerateRandomPosition();
    }   
    public Vector3 GenerateRandomPosition(){
        gridManager = grid.GetComponent<GridManager>();
        int xPos = Random.Range(1, gridManager.columns);
        int yPos = Random.Range(1, gridManager.rows);
        Vector3 spawnPoint =  gridManager.GridToWorldPos(xPos, yPos);
        if (Physics2D.OverlapCircle(spawnPoint, 1f)){
            return GenerateRandomPosition();
        } else {
            return new Vector3(
                -((gridManager.columns / 2f) -0.5f) + (xPos - 1),
                -((gridManager.rows / 2f) -0.5f) + (yPos -1)
            );
        }
    }
}
