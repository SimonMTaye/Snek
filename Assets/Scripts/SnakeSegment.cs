using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSegment : MonoBehaviour
{
    [SerializeField]
    protected SnakeSegment child;
    protected GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    

    public void Move (Vector3 pos){
        Vector3 oldPos = this.transform.position;
        this.transform.position = pos;
        if (child){
            child.Move(oldPos);
        }
    }

    public void AddChild () {
        if (child){
            child.AddChild();
        } else {
            child = Instantiate(this, this.transform.position, Quaternion.identity);
        }
    }
    public void DestroySegment(){
        if(child){
            child.DestroySegment();
        } else {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Pellet"){
            gameManager.PelletCollected();
            AddChild();
            Destroy(other.gameObject);
        } else if (other.tag =="Block"){
            gameManager.GameOver();
        }
    }
}
