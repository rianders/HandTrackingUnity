using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayWebCam : MonoBehaviour
{
    public int WebCamNum;
    private WebCamDevice[] devices;
    private WebCamTexture tex;


    void Start ()
    {
        devices = WebCamTexture.devices;

        // for debugging purposes, prints available devices to the console
        for(int i = 0; i < devices.Length; i++)
        {
            print($"Webcam:{i} available: {devices[i].name} " );
        }

        Renderer rend = this.GetComponentInChildren<Renderer>();

        // assuming the first available WebCam is desired
        tex = new WebCamTexture(devices[WebCamNum].name);
        rend.material.mainTexture = tex;
        tex.Play();
    }

    public void SetCamera(int CamNum) {
        
        Renderer rend = this.GetComponentInChildren<Renderer>();
        tex.Stop();
        tex = new WebCamTexture(devices[CamNum].name);
        rend.material.mainTexture = tex;
        tex.Play();
    }
}
