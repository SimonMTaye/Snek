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
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            RotateUI();
        }
    }

    private void RotateUI(){
        timer = 0f;
        switch(Screen.orientation){
            case ScreenOrientation.LandscapeLeft:
                portraitCanvas.gameObject.SetActive(false);
                landscapeCanvas.gameObject.SetActive(true);
                break;
            case ScreenOrientation.LandscapeRight:
                portraitCanvas.gameObject.SetActive(false);
                landscapeCanvas.gameObject.SetActive(true);
                break;
            case ScreenOrientation.Portrait:
                landscapeCanvas.gameObject.SetActive(false);
                portraitCanvas.gameObject.SetActive(true);
                break;
        }
    }
}
