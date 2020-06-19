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
    private GameObject grid;
    private GridManager gridManager;

    // Start is called before the first frame update
    void Start() {
    }
    public void SpawnPellet(){
        GameObject pellet = Instantiate(growthPellet, new Vector3(1000, 1000), Quaternion.identity, grid.transform);
        pellet.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        pellet.transform.localPosition = GenerateRandomPosition();
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
            return gridManager.WorldPosToGrid(spawnPoint);
        }
    }
}
