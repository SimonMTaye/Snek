using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : SnakeSegment
{

    //Determines how far snake will move on each Move call
    private GameManager gameManager;
    private GridManager gridManager;
    //Used for determing movment direction and parsing user input
    public enum Directions
    {
        UP = 1,
        DOWN = -1,
        LEFT = -2,
        RIGHT = 2
    }
    public Grid gridMan;
    private Directions _direction = Directions.RIGHT;
    //Used to validate movment direction
    private Directions _prevDirection = Directions.RIGHT;
    public float speed = 6.0f;
    [SerializeField]
    private AudioClip pelletConsumend;
    // World bounds for wraping
    private void Start()
    {
        StartCoroutine(MoveSnake());
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gridManager = GameObject.Find("Grid").GetComponent<GridManager>();
        //Apply scale based on grid size from gridManager
        this.transform.localPosition = new Vector3(-((gridManager.columns / 2f) - 0.5f), -((gridManager.rows / 2f) - 0.5f));
    }
    private void Update()
    {
        //Recieve user Input to change direction
        if (Input.GetAxis("Horizontal") < 0)
        {
            ChangeDirection(Directions.LEFT);
        }
        else if ((Input.GetAxis("Horizontal") > 0))
        {
            ChangeDirection(Directions.RIGHT);
        }
        else if ((Input.GetAxis("Vertical") > 0))
        {
            ChangeDirection(Directions.UP);
        }
        else if ((Input.GetAxis("Vertical") < 0))
        {
            ChangeDirection(Directions.DOWN);
        }
    }

    public void ChangeDirection(Directions direction)
    {
        // * Opposite directions have negative values of each other i.e right = 1 and left = -1
        // * This is used to validate that the direction switched to isn't to the opposite of the current direction
        //Change direction for next Move call based on user input
        if ((int)direction * -1 != (int)_prevDirection)
        {
            _direction = direction;
        }
    }
    public void Rotate()
    {
        switch (_direction)
        {
            case Directions.RIGHT:
                ChangeDirection(Directions.UP);
                break;
            case Directions.LEFT:
                ChangeDirection(Directions.DOWN);
                break;
            case Directions.UP:
                ChangeDirection(Directions.RIGHT);
                break;
            case Directions.DOWN:
                ChangeDirection(Directions.LEFT);
                break;
        }
    }

    public Directions GetDirection()
    {
        return _direction;
    }
    public IEnumerator MoveSnake()
    {
        yield return new WaitForSeconds(1 / speed);
        if (Wrap())
        {
            StartCoroutine(MoveSnake());
            yield break;
        }
        Vector3 oldPos = this.transform.localPosition;
        switch (_direction)
        {
            case Directions.RIGHT:
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
                this.transform.localPosition += Vector3.right;
                break;
            case Directions.LEFT:
                this.transform.rotation = Quaternion.Euler(0, 0, 180);
                this.transform.localPosition += Vector3.left;
                break;
            case Directions.UP:
                this.transform.rotation = Quaternion.Euler(0, 0, 90);
                this.transform.localPosition += Vector3.up;
                break;
            case Directions.DOWN:
                this.transform.rotation = Quaternion.Euler(0, 0, 270);
                this.transform.localPosition += Vector3.down;
                break;
        }
        _prevDirection = _direction;
        if (child)
        {
            child.Move(oldPos);
        }
        StartCoroutine(MoveSnake());

    }
    private bool Wrap()
    {
        float rightBound = (gridManager.columns / 2f);
        float upperBound = (gridManager.rows / 2f);
        if (this.transform.localPosition.x > rightBound)
        {
            this.transform.localPosition = new Vector3(-(rightBound - 0.5f), this.transform.localPosition.y, 0);
            return true;
        }
        else if (this.transform.localPosition.x < -rightBound)
        {
            this.transform.localPosition = new Vector3(rightBound - 0.5f, this.transform.localPosition.y, 0);
            return true;
        }
        if (this.transform.localPosition.y > upperBound)
        {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, -(upperBound - 0.5f), 0);
            return true;
        }
        else if (this.transform.localPosition.y < -upperBound)
        {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, upperBound - 0.5f, 0);
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Grow when pellet is consumed, else lose on collision
        if (other.tag == "Pellet")
        {
            AudioSource.PlayClipAtPoint(pelletConsumend, Camera.main.transform.position);
            gameManager.PelletCollected();
            AddChild();
            Destroy(other.gameObject);
        }
        else
        {
            gameManager.GameOver();
        }
    }
}