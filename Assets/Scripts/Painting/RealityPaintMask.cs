using UnityEngine;

    public class RealityPaintMask : MonoBehaviour
    {
        private static readonly int RealityMask = Shader.PropertyToID("_RealityMask");
        public Texture2D maskTex;
        public Renderer rend;
        public int maskResolution = 512;

        void Start()
        {
            rend = GetComponent<Renderer>();
            maskTex = new Texture2D(maskResolution, maskResolution, TextureFormat.RGBA32, false);
            Color[] cols = new Color[maskResolution * maskResolution];
            for (int i = 0; i < cols.Length; i++) cols[i] = Color.black;
            maskTex.SetPixels(cols);
            maskTex.Apply();

            rend.material.SetTexture(RealityMask, maskTex);
        }

        // Called by player script
        public void PaintAtUV(Vector2 uv, float brushSize)
        {
            int x = (int)(uv.x * maskResolution);
            int y = (int)(uv.y * maskResolution);
            
            Debug.Log(uv);

            for (int i = - (int)brushSize; i < brushSize; i++)
            {
                for (int j = - (int)brushSize; j < brushSize; j++)
                {
                    int px = x + i;
                    int py = y + j;
                    if (px >= 0 && px < maskResolution && py >= 0 && py < maskResolution)
                    {
                        float dist = Vector2.Distance(new Vector2(x, y), new Vector2(px, py));
                        if (dist < brushSize)
                            maskTex.SetPixel(px, py, Color.white);
                    }
                }
            }

            maskTex.Apply();
        }
    }
