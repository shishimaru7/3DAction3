using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerSecond : MonoBehaviour
{
    [SerializeField]
    private Camera maincamera;

    [SerializeField]
    private Camera FPScamera;

    private bool maincameraON = true;

    [SerializeField]
    private AudioListener mainListener;

    [SerializeField]
    private AudioListener FPSListener;


    void Start()
    {
        maincamera.enabled = true;

        FPScamera.enabled = false;

        mainListener.enabled = true;

        FPSListener.enabled = false;
    }

    void Update()
    {
        ChangeCamera();
    }

    void ChangeCamera()
    {
        if (Input.GetKeyDown(KeyCode.C) && maincameraON == true)
        {
            maincamera.enabled = false;

            FPScamera.enabled = true;

            maincameraON = false;

            mainListener.enabled = false;

            FPSListener.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && maincameraON == false)
        {
            maincamera.enabled = true;

            FPScamera.enabled = false;

            maincameraON = true;

            mainListener.enabled = true;

            FPSListener.enabled = false;
        }
    }
}
