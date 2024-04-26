using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HandTracking : MonoBehaviour
{
    public UDPReceive udpReceive;
    public GameObject handPrefab1;
    public GameObject handPrefab2;
    public GameObject[] handPoints1;
    public GameObject[] handPoints2;

    private float LastDistance1 = 1;
    private float MaxDistance1 = 0;
    private float MinDistance1 = 100;

    private float LastDistance2 = 1;
    private float MaxDistance2 = 0;
    private float MinDistance2 = 100;

    private string data;

    public float scaleMultiplier = 1f;
    public float depthMultiplier = 1f;

    void Start()
    {
        // Find the hand points from the prefabs
        handPoints1 = FindHandPoints(handPrefab1, "Points");
        handPoints2 = FindHandPoints(handPrefab2, "Points");

        data = "1140, 264, 0, 1020, 302, -30, 927, 390, -46, 910, 502, -65, 970, 568, -73, 953, 523, 15, 906, 605, -47, 925, 508, -74, 955, 470, -82, 1029, 542, 4, 983, 625, -62, 998, 498, -64, 1028, 468, -44, 1106, 536, -18, 1064, 604, -86, 1066, 478, -53, 1095, 445, -8, 1194, 516, -45, 1141, 555, -75, 1129, 473, -46, 1146, 451, -11";
    }

    private GameObject[] FindHandPoints(GameObject prefab, string parentName)
    {
        GameObject parentObject = prefab.transform.Find(parentName).gameObject;
        return parentObject.GetComponentsInChildren<Transform>()
            .Where(t => t != parentObject.transform)
            .Select(t => t.gameObject)
            .ToArray();
    }

    void Update()
    {
        data = udpReceive.data;

        if (data == null || data == "")
        {
            data = "1140, 264, 0, 1020, 302, -30, 927, 390, -46, 910, 502, -65, 970, 568, -73, 953, 523, 15, 906, 605, -47, 925, 508, -74, 955, 470, -82, 1029, 542, 4, 983, 625, -62, 998, 498, -64, 1028, 468, -44, 1106, 536, -18, 1064, 604, -86, 1066, 478, -53, 1095, 445, -8, 1194, 516, -45, 1141, 555, -75, 1129, 473, -46, 1146, 451, -11";
        }

        data = data.Remove(0, 1);
        data = data.Remove(data.Length - 1, 1);

        string[] points = data.Split(',');

        int numPoints = points.Length / 3;
        int numHands = numPoints / 21;

        for (int i = 0; i < numHands; i++)
        {
            GameObject[] handPoints = (i == 0) ? handPoints1 : handPoints2;
            float lastDistance = (i == 0) ? LastDistance1 : LastDistance2;
            float maxDistance = (i == 0) ? MaxDistance1 : MaxDistance2;
            float minDistance = (i == 0) ? MinDistance1 : MinDistance2;

            for (int j = 0; j < 21; j++)
            {
                int index = i * 21 * 3 + j * 3;
                if (index + 2 < points.Length)
                {
                    float x = 7 - float.Parse(points[index]) / 100;
                    float y = float.Parse(points[index + 1]) / 100;
                    float z = float.Parse(points[index + 2]) / 100 * lastDistance;

                    // Apply scale multiplier
                    x *= scaleMultiplier;
                    y *= scaleMultiplier;
                    z *= scaleMultiplier;

                    // Apply depth multiplier
                    z *= depthMultiplier;

                    handPoints[j].transform.localPosition = new Vector3(x, y, z);
                }
            }

            if (handPoints.Length >= 6)
            {
                lastDistance = Vector3.Distance(handPoints[0].transform.localPosition, handPoints[5].transform.localPosition);

                if (lastDistance > maxDistance)
                {
                    maxDistance = lastDistance;
                }
                if (lastDistance < minDistance)
                {
                    minDistance = lastDistance;
                }
            }

            if (i == 0)
            {
                LastDistance1 = lastDistance;
                MaxDistance1 = maxDistance;
                MinDistance1 = minDistance;
            }
            else
            {
                LastDistance2 = lastDistance;
                MaxDistance2 = maxDistance;
                MinDistance2 = minDistance;
            }

            Debug.Log($"Hand {i + 1}: lastDistance: {lastDistance}, min: {minDistance}, max:{maxDistance}");
        }
    }
}