using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    //Number of colums and rows the grid will have. 
    //Rows will be a multiple of 10 while columns are determined based on aspect ratio
    public int columns, rows;
    //Scale for each grid cell. Required to fill screen and avoid incomplete blocks
    public Vector3 blockScale;
    //World point for 0,0 on grid (offscreen). Used to determine location of other grid cells
    public Vector3 gridOrigin;
    //Determines how many sprites a single world unit can bold.
    //Settings this to a larger number will decrease the size of the sprites (increases the number of grid sqaures)
    public int SpritesToWorldUnit = 1;
    private void Awake()
    {
        InitGrid();
    }
    private void InitGrid()
    {
        //Calculate colums, rows and scale based on screen aspect ratio
        rows = (int)Camera.main.orthographicSize * 2 * SpritesToWorldUnit;
        columns = Mathf.FloorToInt(rows * Camera.main.aspect );

        blockScale = new Vector3
            (Camera.main.aspect / (((float)columns / rows) * SpritesToWorldUnit),
            1f / SpritesToWorldUnit, 
            1f);
        gridOrigin = new Vector3(
            (-Camera.main.orthographicSize * Camera.main.aspect) - (0.5f * 1f / SpritesToWorldUnit), 
            -Camera.main.orthographicSize - (0.5f * 1f / SpritesToWorldUnit), 
            1
            );
        print(gridOrigin);
    }
    public Vector3 GridToWorldPos(int gridX, int gridY)
    {
        return gridOrigin + new Vector3((float)gridX * blockScale.x, (float)gridY * blockScale.y, 0);
    }

}
