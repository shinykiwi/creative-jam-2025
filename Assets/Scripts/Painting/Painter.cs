using System;
using UnityEngine;

public class Painter : MonoBehaviour
{
    public Camera cam;
    public float brushSize = 10f;

    public void Awake()
    {
        if (cam == null)
            cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Paint(true);
        }

        if (Input.GetMouseButton(1))
        {
            Paint(false);
        }
    }

    private void Paint(bool add)
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            RealityPaintMask rpm = hit.collider.GetComponent<RealityPaintMask>();
                
            if (rpm)
                rpm.PaintAtUV(hit.textureCoord, brushSize, add);
        }
    }
}