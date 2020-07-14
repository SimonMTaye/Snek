using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    //Number of colums and rows the grid will have. 
    //Rows will be a multiple of 10 while columns are determined based on aspect ratio
    [SerializeField]
    private SnakeHead player;
    public int columns, rows;
    //Store a list of all free grid positions
    private List<Vector2> freeCells;
    //World point for 0,0 on grid (offscreen). Used to determine location of other grid cells
    private Vector2 gridOrigin;
    //Determines how many sprites a single world unit can bold.
    //Settings this to a larger number will decrease the size of the sprites (increases the number of grid sqaures)
    private void Awake() {
        player = GameObject.Find("Snek").GetComponent<SnakeHead>();
        InitGrid(Camera.main.aspect > 1);
    }
    private void InitGrid(bool landScape)
    {   
        int gridSize = PlayerPrefs.GetInt("Grid Size", 2);
        float spritesToWorldUnit = 0.5f + gridSize;
        Screen.orientation = Screen.orientation;
        if (!landScape)
        {
            rows =  Mathf.RoundToInt((Camera.main.orthographicSize * 2 * spritesToWorldUnit) / Camera.main.aspect);
            columns = (int) (Camera.main.orthographicSize * 2 * spritesToWorldUnit);
        } else {
            rows = (int)(Camera.main.orthographicSize * 2 * spritesToWorldUnit);
            columns = Mathf.FloorToInt((Camera.main.orthographicSize * 2 * spritesToWorldUnit) * Camera.main.aspect);
        }
        
        gridOrigin = new Vector2(
            ( ( -( (float)columns / 2f) - 0.5f)),
            ( ( -( (float)rows / 2f) - 0.5f))
            );
        freeCells = new List<Vector2>();
        for (int i = 1, index = 0; i <= columns; i++)
        {
            for (int j = 1; j <= rows; j ++, index ++){
                freeCells.Add(GridNumberToPos(i, j));
            }
        }
        this.transform.localScale = new Vector3(
                (Camera.main.aspect * Camera.main.orthographicSize * 2) / columns,
                (Camera.main.orthographicSize * 2) / rows,
                1f);
        player.transform.localPosition = GridNumberToPos(2, 1);
    }
    public Vector2 GridNumberToPos(int gridX, int gridY)
    {
        return gridOrigin + new Vector2(gridX, gridY);
    }
    public Vector2 GetRandomGridPos(){
        if (freeCells.Count <= player.length){
            GameObject.Find("GameManager").GetComponent<GameManager>().UltimateVictory();
        }
        int posIndex;
        Vector2 pos;
        do
        {
            posIndex = Random.Range(0, freeCells.Count);
            pos = freeCells[posIndex];
        }
        while (Physics2D.OverlapCircle(pos, 0.3f) && Physics2D.OverlapCircle(pos, 0.3f).tag == "Player");
        freeCells.RemoveAt(posIndex);
        return pos;
    }
    public void CellFreed(Vector2 pos){
        freeCells.Add(pos);
    }
}
