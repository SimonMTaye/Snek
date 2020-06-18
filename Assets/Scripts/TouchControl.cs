using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SnakeHead;

public class TouchControl : MonoBehaviour
{

    private SnakeHead player;
    private Vector3 fingerUpPos, fingerDownPos;
    [SerializeField]
    private float swipeThreshold = 30f;
    private float touchTimer;
    private bool swipeMove;
    private bool clickMove;
    // Start is called before the first frame update
    void Start()
    {
        touchTimer = 0f;
        player = this.gameObject.GetComponent<SnakeHead>();
        clickMove = PlayerPrefs.GetInt("Touch Control", 0) == 1;
        swipeMove = PlayerPrefs.GetInt("Touch Control", 0) == 2;
    }

    // Update is called once per frame
    void Update()
    {
        touchTimer += Time.deltaTime;
        foreach (Touch touch  in Input.touches)
        {
            if(touch.phase == TouchPhase.Began){
                fingerDownPos = touch.position;
                touchTimer = 0f;
            }
            if (touch.phase == TouchPhase.Ended){
                fingerUpPos = touch.position;
                if (touchTimer < 1f && clickMove )
                {
                    ClickMove(fingerDownPos);
                } else if (touchTimer < 2f && swipeMove){
                    SwipeMove(fingerUpPos, fingerDownPos);
                }
            }
        }
    }
    private void SwipeMove(Vector3 upPos, Vector3 downPos){
        float xOffset = upPos.x - downPos.x;
        float yOffset = upPos.y - downPos.y;
        if(Mathf.Abs(xOffset) > swipeThreshold && Mathf.Abs(xOffset / yOffset) > 1){
            if(xOffset < 0){
                print("Going Left");
                player.ChangeDirection(Directions.LEFT);
            } else {
                print("Going Right");
                player.ChangeDirection(Directions.RIGHT);
                return;
            }
        } else if (Mathf.Abs(yOffset) > swipeThreshold && Mathf.Abs(yOffset / xOffset) > 1){
            if(yOffset < 0){
                print("Going Down");
                player.ChangeDirection(Directions.DOWN);
            } else {
                print("Going Up");
                player.ChangeDirection(Directions.UP);
                return;
            }
        }
    }
    private void ClickMove(Vector2 touchPos){
        //Determine which part of the screen was clicked
        //Adjust direction
        
        bool leftSideClicked = touchPos.x < (Screen.width / 2);
        switch (player.GetDirection()){
            case Directions.LEFT:
                if(!leftSideClicked){
                    player.ChangeDirection(Directions.UP);
                } else {
                    player.ChangeDirection(Directions.DOWN);
                }
                break;
            case Directions.RIGHT:
                if(!leftSideClicked){
                    player.ChangeDirection(Directions.DOWN);
                } else {
                    player.ChangeDirection(Directions.UP);
                }
                break;
            case Directions.UP:
                if(!leftSideClicked){
                    player.ChangeDirection(Directions.RIGHT);
                } else {
                    player.ChangeDirection(Directions.LEFT);
                }
                break;
            case Directions.DOWN:
                if(!leftSideClicked){
                    player.ChangeDirection(Directions.LEFT);
                } else {
                    player.ChangeDirection(Directions.RIGHT);
                }
                break;
            }

    }
}
