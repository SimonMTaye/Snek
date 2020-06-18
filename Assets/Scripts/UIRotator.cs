using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRotator : MonoBehaviour
{
    [SerializeField]
    private Canvas portraitCanvas;
    [SerializeField]
    private Canvas landscapeCanvas;

    private float checkDelay = 1f;
    private float timer;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= checkDelay){
            RotateUI();
        }
    }

    private void RotateUI(){
        timer = 0f;
        switch(Input.deviceOrientation){
            case DeviceOrientation.LandscapeLeft:
                portraitCanvas.gameObject.SetActive(false);
                landscapeCanvas.gameObject.SetActive(true);
                break;
            case DeviceOrientation.LandscapeRight:
                portraitCanvas.gameObject.SetActive(false);
                landscapeCanvas.gameObject.SetActive(true);
                break;
            case DeviceOrientation.Portrait:
                landscapeCanvas.gameObject.SetActive(false);
                portraitCanvas.gameObject.SetActive(true);
                break;
        }
    }
}
