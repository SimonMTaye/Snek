using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : SnakeSegment {

    //Determines how far snake will move on each Move call
    private float blockWidth, blockHeight;
    private GameManager gameManager;
    private GridManager gridManager;
    //Used for determing movment direction and parsing user input
    private enum Directions {
        UP = 1,
        DOWN = -1,
        LEFT = 2,
        RIGHT = -2
    }
    private Directions _direction = Directions.LEFT;
    //Used to validate movment direction
    private Directions _prevDirection = Directions.LEFT;
    [SerializeField]
    private float speed = 6.0f;
    // World bounds for wraping
    private float rightBound, upperBound;
    [SerializeField]
    private SnakeSegment child;

    private void Start() {
        StartCoroutine(MoveSnake());
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gridManager = GameObject.Find("GridManager").GetComponent<GridManager>();
        //Apply scale based on grid size from gridManager
        this.ApplyScale(gridManager.blockScale);
        //Start at 1,1 on the grid
        this.transform.position = gridManager.GridToWorldPos(1, 1);
        //Determine how much distance will be covered every move call (1 grid cell) 
        blockHeight = this.transform.localScale.y;
        blockWidth = this.transform.localScale.x;
        CalculateBounds();
    }
    private void Update() {
        //Recieve user Input to change direction
        if(Input.GetAxis("Horizontal") > 0){
            ChangeDirection(Directions.LEFT);
        } else if ((Input.GetAxis("Horizontal") < 0)){
            ChangeDirection(Directions.RIGHT);
        } else if((Input.GetAxis("Vertical") > 0)){
            ChangeDirection(Directions.UP);
        } else if ((Input.GetAxis("Vertical") < 0)){
            ChangeDirection(Directions.DOWN);
        }
    }
    
    private void ChangeDirection(Directions direction){
        // * Opposite directions have negative values of each other i.e right = 1 and left = -1
        // * This is used to validate that the direction switched too isn't to the opposite of the current direction
        //Change direction for next Move call based on user input
        if( (int)direction * -1 != (int)_prevDirection ){
            _direction = direction;
        }
    }

    public IEnumerator MoveSnake() {
        yield return new WaitForSeconds(1 / speed);
        Wrap();
        Vector3 oldPos = this.transform.position;
        switch(_direction){
            case Directions.LEFT:
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
                this.transform.position += new Vector3(blockWidth, 0, 0);
                break;
            case Directions.RIGHT:
                this.transform.rotation = Quaternion.Euler(0, 0, 180);
                this.transform.position += new Vector3(-blockWidth, 0, 0);
                break;
            case Directions.UP:
                this.transform.rotation = Quaternion.Euler(0, 0, 90);
                this.transform.position += new Vector3(0, blockHeight, 0);
                break;
            case Directions.DOWN:
                this.transform.rotation = Quaternion.Euler(0, 0, 270);
                this.transform.position += new Vector3(0, -blockHeight, 0);
                break;
        }
        _prevDirection = _direction;
        if(child){
            print("moving child");
            child.Move(oldPos);
        }
        StartCoroutine(MoveSnake());
    }
    private void Wrap(){
        if (this.transform.position.x > rightBound){
            this.transform.position = new Vector3(-rightBound, this.transform.position.y, 0);
        } else if (this.transform.position.x < -rightBound){
            this.transform.position = new Vector3(rightBound, this.transform.position.y, 0);
        }
        if(this.transform.position.y >upperBound ){
            this.transform.position = new Vector3(this.transform.position.x, -upperBound, 0);
        } else if(this.transform.position.y < -upperBound){
            this.transform.position = new Vector3(this.transform.position.x, upperBound, 0);
        }
    }

    private void CalculateBounds(){
        //Determine size of world for wraping when off screen
        upperBound = Camera.main.orthographicSize - (0.5f * this.transform.localScale.x);
        rightBound = (Camera.main.orthographicSize * Camera.main.aspect) - (0.5f * this.transform.localScale.y);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        // Grow when pellet is consumed, else lose on collision
        if (other.tag == "Pellet"){
            gameManager.PelletCollected();
            AddChild();
            Destroy(other.gameObject);
        } else {
            gameManager.GameOver();
        }
    }
}