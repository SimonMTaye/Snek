using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRotator : MonoBehaviour
{
    [SerializeField]
    private Canvas portraitCanvas;
    [SerializeField]
    private Canvas landscapeCanvas;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        print(Input.deviceOrientation);
        switch(Input.deviceOrientation){
            case DeviceOrientation.LandscapeLeft:
                portraitCanvas.gameObject.SetActive(false);
                landscapeCanvas.gameObject.SetActive(true);
                landscapeCanvas.gameObject.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0f, 0f, 0f);
                break;
            case DeviceOrientation.LandscapeRight:
                portraitCanvas.gameObject.SetActive(false);
                landscapeCanvas.gameObject.SetActive(true);
                landscapeCanvas.gameObject.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0f, 0f, 180f);
                break;
            case DeviceOrientation.Portrait:
                landscapeCanvas.gameObject.SetActive(false);
                portraitCanvas.gameObject.SetActive(true);
                portraitCanvas.gameObject.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0f, 0f, 0f);
                break;
            case DeviceOrientation.PortraitUpsideDown:
                landscapeCanvas.gameObject.SetActive(false);
                portraitCanvas.gameObject.SetActive(true);
                portraitCanvas.gameObject.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0f, 0f, 180f);
                break;
        }
    }
}
