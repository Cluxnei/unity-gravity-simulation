using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomController : MonoBehaviour
{
    private float zoom = 100f;
    private Camera _camera;
    private float zoomLevel = 1.5f;

    void Start()
    {
        this._camera = this.GetComponent<Camera>();
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll == 0)
        {
            return;
        }
        if (scroll > 0 && this.zoom > 2)
        {
            this.zoom -= this.zoomLevel;
        }
        if (scroll < 0)
        {
            this.zoom += this.zoomLevel;
        }
        
        this._camera.orthographicSize = this.zoom;
    }
}
