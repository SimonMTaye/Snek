using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    //Number of colums and rows the grid will have. 
    //Rows will be a multiple of 10 while columns are determined based on aspect ratio
    public int columns, rows;
    //Scale for each grid cell. Required to fill screen and avoid incomplete blocks
    private Vector3 blockScale;
    //World point for 0,0 on grid (offscreen). Used to determine location of other grid cells
    private Vector3 gridOrigin;
    //Determines how many sprites a single world unit can bold.
    //Settings this to a larger number will decrease the size of the sprites (increases the number of grid sqaures)
    private float spritesToWorldUnit;
    //Used to check device orientation. Avoiding Screen.orientation for performace reasons
    private float aspect;
    //Used to recalculate grid if device is rotated
    private int gridSize;
    private void Awake() {
        gridSize = PlayerPrefs.GetInt("Grid Size", 2);
        spritesToWorldUnit = 1f + (0.5f * gridSize);
        InitGrid(Camera.main.aspect > 1);
    }
    private void InitGrid(bool landScape)
    {
        aspect = Camera.main.aspect;
        if (!landScape)
        {
            rows =  Mathf.RoundToInt((Camera.main.orthographicSize * 2 * spritesToWorldUnit) / Camera.main.aspect);
            columns = (int) (Camera.main.orthographicSize * 2 * spritesToWorldUnit);
        } else {
            rows = (int)(Camera.main.orthographicSize * 2 * spritesToWorldUnit);
            columns = Mathf.FloorToInt((Camera.main.orthographicSize * 2 * spritesToWorldUnit) * Camera.main.aspect);
        }

        blockScale = new Vector3(
            (Camera.main.aspect * Camera.main.orthographicSize * 2) / columns,
            (Camera.main.orthographicSize * 2) / rows, 
            1f);
        gridOrigin = new Vector3(
             (-(columns / 2) * blockScale.x) - (blockScale.x * 2),
             (-(rows / 2) * blockScale.y) - (blockScale.y * 2), 
            1
            );
        this.transform.localScale = blockScale;
    }
    public Vector3 GridToWorldPos(int gridX, int gridY)
    {
        return gridOrigin + new Vector3((float)gridX * blockScale.x, (float)gridY * blockScale.y);
    }
    private void RotateGrid()
    {
        InitGrid(Camera.main.aspect > 1);
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.GetComponent<SnakeHead>()){
                child.GetComponent<SnakeHead>().Rotate();
            }
            child.transform.position = new Vector3(-child.transform.position.y, child.transform.position.x);
        }
    }
    private void Update() 
    {
        if(aspect != Camera.main.aspect){
            RotateGrid();
        }
    }
}
