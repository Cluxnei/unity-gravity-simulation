using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontOverflow : MonoBehaviour
{
    public static Vector3 positionBoundsX;
    public static Vector3 positionBoundsY;
    public static bool computeBounds = true;

    void Start()
    {
        if (DontOverflow.computeBounds)
        {
            DontOverflow.positionBoundsX = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.width, Camera.main.farClipPlane / 2));
            DontOverflow.positionBoundsY = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, Camera.main.farClipPlane / 2));
            DontOverflow.computeBounds = false;
        }
    }

    void FixedUpdate()
    {
        if (this.transform.position.x <= DontOverflow.positionBoundsX.x)
        {
            Destroy(this.gameObject);
            return;
        }
        if (this.transform.position.x >= DontOverflow.positionBoundsX.y)
        {
            Destroy(this.gameObject);
            return;
        }
        if (this.transform.position.y <= DontOverflow.positionBoundsY.x)
        {
            Destroy(this.gameObject);
            return;
        }
        if (this.transform.position.y >= DontOverflow.positionBoundsY.y)
        {
            Destroy(this.gameObject);
            return;
        }
    }
}
