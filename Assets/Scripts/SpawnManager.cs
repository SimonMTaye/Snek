using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject blockPrefab;
    [SerializeField]
    private GameObject growthPellet;
    float rightBound = 8.42f;
    float leftBound = -8.45f;
    float upperBound = 4.58f;
    float lowerBound = -4.56f;
    // Start is called before the first frame update
    public void SpawnPellet(){
        Instantiate(growthPellet, GenerateRandomVector3(), Quaternion.identity);
    }
    public void SpawnBlock(){
        Instantiate(blockPrefab, GenerateRandomVector3(), Quaternion.identity);
    }
    public Vector3 GenerateRandomVector3(){
        float xPos = Random.Range(leftBound, rightBound);
        float yPos = Random.Range(lowerBound, upperBound);
        Vector3 generatedPos = new Vector3(xPos, yPos, 0);
        if (!Physics.CheckSphere(generatedPos, 2f)){
            return generatedPos;
        } else {
            return GenerateRandomVector3();
        }
    }
}
