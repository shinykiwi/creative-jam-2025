using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
    public class RealityPaintMask : MonoBehaviour
    {
        private static readonly int RealityMask = Shader.PropertyToID("_RealityMask");
        private Texture2D maskTex;
        private Renderer rend;
        private MeshCollider meshCollider;
        public bool isVisible;
        public int maskResolution = 512;
        private int paintedPixels = 0;
        public Texture2D brushMask; // Assign in inspector (must be readable)
        private float PaintedPercent => 2*paintedPixels / (float)(maskResolution*maskResolution);

        [Range(0f, 1f)] public float threshold;
        

        void Start()
        {
            rend = GetComponent<Renderer>();
            maskTex = new Texture2D(maskResolution, maskResolution, TextureFormat.RGBA32, false);
            meshCollider = GetComponent<MeshCollider>();
            Color[] cols = new Color[maskResolution * maskResolution];
            for (int i = 0; i < cols.Length; i++) cols[i] = isVisible? Color.white : Color.black;
            maskTex.SetPixels(cols);
            maskTex.Apply();

            rend.material.SetTexture(RealityMask, maskTex);
        }

        public void PaintAtUV(Vector2 uv, float brushSize, bool add = true, float strength = 0.1f)
        {
            int x = (int)(uv.x * maskResolution);
            int y = (int)(uv.y * maskResolution);

            for (int i = -(int)brushSize; i < brushSize; i++)
            {
                for (int j = -(int)brushSize; j < brushSize; j++)
                {
                    int px = x + i;
                    int py = y + j;

                    if (px >= 0 && px < maskResolution && py >= 0 && py < maskResolution)
                    {
                        float dist = Vector2.Distance(new Vector2(x, y), new Vector2(px, py));
                        if (dist < brushSize)
                        {
                            Color current = maskTex.GetPixel(px, py);
                            float targetValue = add ? 1.3f : 0f;

                            // Smooth transition using Lerp
                            float newValue = Mathf.Lerp(current.r, targetValue, strength);

                            // Update paintedPixels count only when crossing thresholds
                            if (add && current.r < 1f && newValue >= 1f)
                                paintedPixels++;
                            else if (!add && current.r > 0f && newValue <= 0f)
                                paintedPixels--;

                            maskTex.SetPixel(px, py, new Color(newValue, newValue, newValue, 1f));
                        }
                    }
                }
            }

            maskTex.Apply();

            if (PaintedPercent > threshold && !isVisible)
            {
                Solidify();
            }
            else if (PaintedPercent < threshold && isVisible)
            {
                Disappear();
            }
        }


        public void Solidify()
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
            isVisible = true;
        }

        public void Disappear()
        {
            gameObject.layer = LayerMask.NameToLayer("FakeLayer");
            isVisible = false;
        }
    }
