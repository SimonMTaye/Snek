using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject blockPrefab;
    [SerializeField]
    private GameObject pelletPrefab;
    [SerializeField]
    private AudioClip pelletSpawnedAudio;
    [SerializeField]
    private GameObject grid;
    private GridManager gridManager;

    // Start is called before the first frame update
    
    private void Awake() {
        gridManager = grid.GetComponent<GridManager>();
    }
    public void SpawnPellet(bool playAudio = true)
    {
        GameObject pellet = Instantiate(pelletPrefab, new Vector3(1000, 1000), Quaternion.identity, grid.transform);
        if (PlayerPrefs.GetInt("Grid Size") <= 1)
        {
            pellet.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        }
        pellet.transform.localPosition = gridManager.GetRandomGridPos();
        if (playAudio)
        {
            AudioSource.PlayClipAtPoint(pelletSpawnedAudio, Camera.main.transform.position);
        }
    }
    public void SpawnBlock(){
        GameObject block = Instantiate(blockPrefab, new Vector3(1000, 1000), Quaternion.identity, grid.transform);
        block.transform.localPosition = gridManager.GetRandomGridPos();
    }   
}
