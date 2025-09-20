using UnityEngine;

public class Painter : MonoBehaviour
{
    public Camera cam;
    public float brushSize = 10f;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                RealityPaintMask rpm = hit.collider.GetComponent<RealityPaintMask>();
                
                if (rpm)
                    rpm.PaintAtUV(hit.textureCoord, brushSize);
            }
        }
    }
}