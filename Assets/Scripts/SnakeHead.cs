using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : SnakeSegment {

    private float blockWidth, blockHeight;
    private enum Directions {
        UP = 1,
        DOWN = -1,
        LEFT = 2,
        RIGHT = -2
    }
    private Directions _direction = Directions.LEFT;
    private Directions _prevDirection = Directions.LEFT;
    [SerializeField]
    private float speed = 6.0f;
    private float timer;

    private float rightBound, upperBound;

    private void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gridManager = GameObject.Find("GridManager").GetComponent<GridManager>();
        timer = 0f;
        this.transform.localScale = gridManager.blockScale;
        this.transform.position = gridManager.GridToWorldPos(1, 1);
        blockHeight = this.transform.localScale.y;
        blockWidth = this.transform.localScale.x;
        CalculateBounds();
    }
    private void Update() {
        if(Input.GetAxis("Horizontal") > 0){
            ChangeDirection(Directions.LEFT);
        } else if ((Input.GetAxis("Horizontal") < 0)){
            ChangeDirection(Directions.RIGHT);
        } else if((Input.GetAxis("Vertical") > 0)){
            ChangeDirection(Directions.UP);
        } else if ((Input.GetAxis("Vertical") < 0)){
            ChangeDirection(Directions.DOWN);
        }
        timer += Time.deltaTime;
        if (timer >= 1 / speed)
        {
            Move();
        }
    }
    
    private void ChangeDirection(Directions direction){
        if( (int)direction * -1 != (int)_prevDirection ){
            _direction = direction;
        }
    }

    public void Move() {
        Wrap();
        timer = 0f;
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
            child.Move(oldPos);
        }
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

    public void SetChild (GameObject segment) {
        this.child = segment.GetComponent<SnakeSegment>();
    }   

    private void CalculateBounds(){
        upperBound = Camera.main.orthographicSize - (0.5f * this.transform.localScale.x);
        rightBound = (Camera.main.orthographicSize * Camera.main.aspect) - (0.5f * this.transform.localScale.y);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Pellet"){
            gameManager.PelletCollected();
            AddChild();
            Destroy(other.gameObject);
        } else {
            print(other.name);
            gameManager.GameOver();
        }
    }
}