using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTracking : MonoBehaviour
{
    // Start is called before the first frame update
    public UDPReceive udpReceive;
    public GameObject[] handPoints;

    private string data;
    void Start()
    {
        data = "1140, 264, 0, 1020, 302, -30, 927, 390, -46, 910, 502, -65, 970, 568, -73, 953, 523, 15, 906, 605, -47, 925, 508, -74, 955, 470, -82, 1029, 542, 4, 983, 625, -62, 998, 498, -64, 1028, 468, -44, 1106, 536, -18, 1064, 604, -86, 1066, 478, -53, 1095, 445, -8, 1194, 516, -45, 1141, 555, -75, 1129, 473, -46, 1146, 451, -11";
    }

    // Update is called once per frame
    void Update() 
    {
        data = udpReceive.data;

        //In case no data put some default data in
        if (data == null || data == "") {
            data = "1140, 264, 0, 1020, 302, -30, 927, 390, -46, 910, 502, -65, 970, 568, -73, 953, 523, 15, 906, 605, -47, 925, 508, -74, 955, 470, -82, 1029, 542, 4, 983, 625, -62, 998, 498, -64, 1028, 468, -44, 1106, 536, -18, 1064, 604, -86, 1066, 478, -53, 1095, 445, -8, 1194, 516, -45, 1141, 555, -75, 1129, 473, -46, 1146, 451, -11";
        }
        //Debug.Log($"st: {data}");

        data = data.Remove(0, 1);
        data = data.Remove(data.Length-1, 1);
        //print(data);
        string[] points = data.Split(',');
        //Debug.Log($" po: {points[0]}");

        //0        1*3      2*3
        //x1,y1,z1,x2,y2,z2,x3,y3,z3

        for ( int i = 0; i<21; i++)
        {

            float x = 7-float.Parse(points[i * 3])/100;
            float y = float.Parse(points[i * 3 + 1]) / 100;
            float z = float.Parse(points[i * 3 + 2]) / 100;

            handPoints[i].transform.localPosition = new Vector3(x, y, z);

        }


    }
}