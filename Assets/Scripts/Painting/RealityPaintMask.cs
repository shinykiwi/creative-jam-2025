using System;
using System.Collections;
using System.Data;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshCollider))]
    public class RealityPaintMask : MonoBehaviour
    {
        private static readonly int RealityMask = Shader.PropertyToID("_RealityMask");
        private static readonly int DissolvingIn = Shader.PropertyToID("_dissolvingIn");
        private static readonly int DissolveAmount = Shader.PropertyToID("_dissolveAmount");
        private Texture2D maskTex;
        private Texture2D whiteTexture;
        private Texture2D blackTexture;
        private Renderer rend;
        private MeshCollider meshCollider;
        public bool isVisible;
        public int maskResolution = 512;
        private int paintedPixels = 0;

        private bool canAdd = true;
        private bool canRemove = true;

        public UnityEvent OnAppear;
        public UnityEvent OnDisappear;

        
        private float PaintedPercent => 2*paintedPixels / (float)(maskResolution*maskResolution);

        [Range(0f, 1f)] public float thresholdPaint = 1;
        [Range(0f, 1f)] public float thresholdDisappear = 0;


        public void Reset()
        {
            
            if (isVisible)
            {
                gameObject.layer = LayerMask.NameToLayer("Default");
            }
            else
            {
                gameObject.layer = LayerMask.NameToLayer("FakeLayer");
            }
        }

        void Start()
        {
            rend = GetComponent<Renderer>();
            maskTex = new Texture2D(maskResolution, maskResolution, TextureFormat.RGBA32, false);
            meshCollider = GetComponent<MeshCollider>();
            Color[] cols = new Color[maskResolution * maskResolution];
            for (int i = 0; i < cols.Length; i++) cols[i] = isVisible? Color.white : Color.black;
            maskTex.SetPixels(cols);
            maskTex.Apply();

            whiteTexture = new Texture2D(maskResolution, maskResolution, TextureFormat.RGBA32, false);
            Color[] cols1 = new Color[maskResolution * maskResolution];
            for (int i = 0; i < cols1.Length; i++) cols1[i] =Color.white;
            whiteTexture.SetPixels(cols1);
            
            blackTexture = new Texture2D(maskResolution, maskResolution, TextureFormat.RGBA32, false);
            Color[] cols2 = new Color[maskResolution * maskResolution];
            for (int i = 0; i < cols2.Length; i++) cols2[i] =Color.white;
            blackTexture.SetPixels(cols2);

            
            
            
            rend.material.SetTexture(RealityMask, maskTex);

            OnAppear.AddListener(Solidify);
            OnDisappear.AddListener(Disappear);
        }

        public void PaintAtUV(Vector2 uv, float brushSize, bool add = true, float strength = 0.1f)
        {

            if (add && !canAdd || !add & !canRemove)
            {
                return;
            } 
                    
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
                            float targetValue = add ? 1.3f : -0.3f;

                            // Smooth transition using Lerp
                            float newValue = Mathf.Lerp(current.r, targetValue, strength);
                            
                            // Update paintedPixels count only when crossing thresholds
                            if (add && current.r <= 0.1f && newValue >0.1f)
                            {
                                paintedPixels++;
                                paintedPixels = Mathf.Min(paintedPixels, maskResolution * maskResolution);
                            }
                            else if (!add && current.r >= 0.9f && newValue < 0.9f)
                            {
                                paintedPixels--;
                                paintedPixels = Mathf.Max(0,  paintedPixels);
                            }
                            
                            maskTex.SetPixel(px, py, new Color(newValue, newValue, newValue, 1f));
                        }
                    }
                }
            }

            maskTex.Apply();
//            Debug.Log(PaintedPercent);
            //Debug.Log(PaintedPercent);
            if (PaintedPercent > thresholdPaint && !isVisible)
            {
                isVisible = true;
                OnAppear?.Invoke();
                
            }
            
            else if (PaintedPercent < thresholdDisappear && isVisible)
            {
                isVisible = false;
                OnDisappear?.Invoke();
                
            }
            
        }

        public float fillSpeed = 1;
        
        IEnumerator FillIn()
        {
            canAdd = false; 
            rend.material.SetFloat(DissolveAmount, 0f);
            rend.material.SetFloat(DissolvingIn, 1f);
            
            float t = 0;
            while (t < 1f)
            {
                t +=  Time.deltaTime * fillSpeed;
                rend.material.SetFloat(DissolveAmount, t);
                
                yield return new WaitForEndOfFrame();
            }

            
            
            rend.material.SetFloat(DissolvingIn, 0f);
            
            Color[] cols = new Color[maskResolution * maskResolution];
            for (int i = 0; i < cols.Length; i++) cols[i] =Color.white;
            maskTex.SetPixels(cols);
            paintedPixels = (int)(maskResolution * maskResolution * 0.5f);
            canAdd = true;
            maskTex.Apply();
        }

        public void Solidify()
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
            StartCoroutine(FillIn());
        }
        
        
        
        

        public void Disappear()
        {
            gameObject.layer = LayerMask.NameToLayer("FakeLayer");
            StartCoroutine(FillOut());

        }
        
        IEnumerator FillOut()
        {
            canRemove = false; 
            rend.material.SetFloat(DissolveAmount, 0f);
            rend.material.SetFloat(DissolvingIn, -1f);
            
            float t = 0;
            while (t < 1f)
            {
                t +=  Time.deltaTime * fillSpeed;
                rend.material.SetFloat(DissolveAmount, t);
                
                yield return new WaitForEndOfFrame();
            }

            
            
            rend.material.SetFloat(DissolvingIn, 0f);
            
            Color[] cols = new Color[maskResolution * maskResolution];
            for (int i = 0; i < cols.Length; i++) cols[i] =Color.black;
            maskTex.SetPixels(cols);
            paintedPixels = 0;
            canRemove = true;
            maskTex.Apply();
        }
    }
