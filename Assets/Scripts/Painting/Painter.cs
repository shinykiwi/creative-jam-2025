using System;
using UnityEngine;
using UnityEngine.Events;

public class Painter : MonoBehaviour
{
    public Camera cam;
    public float brushSize = 10f;

    
    public UnityEvent OnAdd;
    public UnityEvent OnRemove;
    public UnityEvent OnNormal;
    public float strength = 0.01f;
    public void Awake()
    {
        if (cam == null)
            cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnAdd?.Invoke();
        }

        if (Input.GetMouseButtonDown(1))
        {
            OnRemove?.Invoke();
        }

        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            OnNormal?.Invoke();
        }
        
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
                rpm.PaintAtUV(hit.textureCoord, brushSize, add, strength);
        }
    }
}