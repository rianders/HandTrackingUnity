using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebCamOnRawImage : MonoBehaviour
{
    public RawImage rawimage;

    //device list only avialable at run time
    public WebCamDevice[] devices;
    public int DeviceNumber;
    
    void Start () 
    {   
        //set the device listens
        devices = WebCamTexture.devices;

        // for debugging purposes, prints available devices to the console
        for(int i = 0; i < devices.Length; i++)
        {
            print($"Webcam:{i} available: {devices[i].name} " );
        }


        // assuming the first available WebCam is desired
        WebCamTexture webcamTexture = new WebCamTexture(devices[DeviceNumber].name);
        rawimage.texture = webcamTexture;
        rawimage.material.mainTexture = webcamTexture;
        webcamTexture.Play();
    }
}



