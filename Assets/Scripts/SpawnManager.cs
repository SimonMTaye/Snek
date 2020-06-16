﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject blockPrefab;
    [SerializeField]
    private GameObject growthPellet;
    [SerializeField]
    private GridManager gridManager;
    // Start is called before the first frame update
    void Start() {
    }
    public void SpawnPellet(){
        GameObject pellet = Instantiate(growthPellet, GenerateRandomPosition(), Quaternion.identity);
        pellet.transform.localScale = gridManager.blockScale / 2f;
    }
    public void SpawnBlock(){
        GameObject block = Instantiate(blockPrefab, GenerateRandomPosition(), Quaternion.identity);
        block.transform.localScale = gridManager.blockScale;
    }   
    public Vector3 GenerateRandomPosition(){
        int xPos = Random.Range(1, (int)gridManager.columns);
        int yPos = Random.Range(1, (int)gridManager.rows);
        Vector3 spawnPoint =  gridManager.GridToWorldPos(xPos, yPos);
        if (Physics2D.OverlapCircle(spawnPoint, 1f)){
            return GenerateRandomPosition();
        } else {
            return spawnPoint;
        }
    }
}
