using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSegment : MonoBehaviour
{
    [SerializeField]
    protected SnakeSegment child;
    public void Move (Vector3 pos){
        // Move to parent position and move child to this segment's old position
        Vector3 oldPos = this.transform.position;
        this.transform.position = pos;
        if (child){
            child.Move(oldPos);
        }
    }

    protected void AddChild () {
        //Add child segment. If segement already has child, call is passed on until a childless sement is found
        if (child){
            child.AddChild();
        } else {
            child = Instantiate(this, this.transform.position, Quaternion.identity);
            child.ApplyScale(this.transform.localScale);
        }
    }
    protected void DestroySegment(){
        // * Unused function. For future powerups
        // Remove the last child node from the snake
        if(child){
            child.DestroySegment();
        } else {
            Destroy(this.gameObject);
        }
    }

    protected void ApplyScale(Vector3 scale) {
        this.transform.localScale = scale;
        if (child){
            child.ApplyScale(scale);
        }
    }
}
