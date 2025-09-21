using System.Collections;
using UnityEngine;

public class FadableItem : MonoBehaviour
{
    private static readonly int DissolveAmount = Shader.PropertyToID("_dissolveAmt");

    private Renderer rend;
    private Material mat;           // cached instance
    private Coroutine running;
    public float speed = 1f;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend == null) { Debug.LogError("No Renderer on this GameObject."); return; }

        mat = rend.material; // creates an instance once and caches it
        if (!mat.HasProperty(DissolveAmount))
            Debug.LogWarning($"{name}: material doesn't have property _DissolveAmount");
    }

    public void FadeIn()
    {
        if (running != null) StopCoroutine(running);
        running = StartCoroutine(FadeTo(1f));
    }

    public void FadeOut()
    {
        if (running != null) StopCoroutine(running);
        running = StartCoroutine(FadeTo(0f));
    }

    private IEnumerator FadeTo(float target)
    {
        float start = mat.GetFloat(DissolveAmount);
        float elapsed = 0f;
        float duration = Mathf.Max(0.0001f, Mathf.Abs(target - start) / Mathf.Max(0.0001f, speed));

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float value = Mathf.Lerp(start, target, t);
            mat.SetFloat(DissolveAmount, value);
            yield return null; // frame-by-frame
        }

        mat.SetFloat(DissolveAmount, target);
        running = null;
    }
}