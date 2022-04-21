using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignWebCamTexture : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        WebCamTexture webcamTexture = new WebCamTexture();
        webcamTexture.deviceName = devices[1].name;
        webcamTexture.Play();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
