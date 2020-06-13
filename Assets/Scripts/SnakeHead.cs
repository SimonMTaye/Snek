using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : SnakeSegment {

    [SerializeField]
    private float blockWidth = 0.97f;
    private enum Directions {
        UP = 1,
        DOWN = -1,
        LEFT = 2,
        RIGHT = -2
    }
    private Directions _direction = Directions.LEFT;
    [SerializeField]
    private float speed = 1.0f;
    private float timer;
    [SerializeField]
    private float directionChangeDelay = 0.1f;
    private float directionChangeTimer = 0f;
    private void Start() {
        timer = 0f;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
        directionChangeTimer += Time.deltaTime;
        timer += Time.deltaTime;
        if (timer >= 1 / speed)
        {
            Move();
        }
    }
    
    private void ChangeDirection(Directions direction){
        if( (int)direction * -1 != (int)_direction  && directionChangeTimer >= directionChangeDelay){
            directionChangeTimer = 0;
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
                this.transform.position += new Vector3(0, blockWidth, 0);
                break;
            case Directions.DOWN:
                this.transform.rotation = Quaternion.Euler(0, 0, 270);
                this.transform.position += new Vector3(0, - blockWidth, 0);
                break;
        }
        if(child){
            child.Move(oldPos);
        }
    }
    private void Wrap(){
        float rightBound = 8.42f;
        float leftBound = -8.45f;
        float upperBound = 4.58f;
        float lowerBound = -4.56f;
        if (this.transform.position.x > rightBound){
            this.transform.position = new Vector3(leftBound, this.transform.position.y, 0);
        } else if (this.transform.position.x < leftBound){
            this.transform.position = new Vector3(rightBound, this.transform.position.y, 0);
        }
        if(this.transform.position.y >upperBound ){
            this.transform.position = new Vector3(this.transform.position.x, lowerBound, 0);
        } else if(this.transform.position.y < lowerBound){
            this.transform.position = new Vector3(this.transform.position.x, upperBound, 0);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Pellet"){
            gameManager.PelletCollected();
            AddChild();
            Destroy(other.gameObject);
        } else {
            gameManager.GameOver();
        }
    }
}