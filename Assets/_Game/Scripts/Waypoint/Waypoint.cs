using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Waypoint : MonoBehaviour
{
    public Image img;
    public Transform target;
    public TextMeshProUGUI level;

    // Update is called once per frame
    void Update()
    {
        float minX = img.GetPixelAdjustedRect().width / 2 + 100;
        float maxX = Screen.width - minX;

        float minY = img.GetPixelAdjustedRect().height / 2 + 100;
        float maxY = Screen.height - minY;

        Vector2 calculatePos = Camera.main.WorldToScreenPoint(target.position);
        transform.position = calculatePos;

        Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);
        Vector2 pos = calculatePos;

        if (calculatePos.x < minX)
        {
            pos.x = minX;
            pos.y = ((minX - center.x) / (calculatePos.x - center.x)) * (calculatePos.y - center.y) + center.y;
        }
        else if (calculatePos.x > maxX)
        {
            pos.x = maxX;
            pos.y = ((maxX - center.x) / (calculatePos.x - center.x)) * (calculatePos.y - center.y) + center.y;
        }

        else if (calculatePos.y < minY)
        {
            pos.y = minY;
            pos.x = ((minY - center.y) / (calculatePos.y - center.y)) * (calculatePos.x - center.x) + center.x;
        }
        else if (calculatePos.y > maxY)
        {
            pos.y = maxY;
            pos.x = ((maxY - center.y) / (calculatePos.y - center.y)) * (calculatePos.x - center.x) + center.x;
        }


        img.transform.position = pos;

        img.transform.rotation = Quaternion.identity;

        if ((calculatePos - pos).sqrMagnitude < 0.1f) return;

        Vector2 dir = pos - center;
        float angleToRotate = Vector2.Angle(Vector2.down, dir);
        img.transform.Rotate(0f, 0f, dir.x > 0 ? angleToRotate : -angleToRotate);
    }

    public void SetupWaypoint(Transform waypoint)
    {
        target = waypoint;
    }
}
