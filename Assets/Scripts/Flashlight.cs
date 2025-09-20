using System.Collections;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Light spotlight;
    public Color AddColor;
    public Color NormalColor;
    public Color RemoveColor;

    public float Timer;
    
    public void Normalize()
    {
        Colorize(NormalColor);
    }
    
    public void Add()
    {
        Colorize(AddColor);
    }

    public void Remove()
    {
        Colorize(RemoveColor);
    }
    
    
    private void Colorize(Color newColor)
    {
        StopAllCoroutines();
        StartCoroutine(_Colorize(newColor));
    }
    
    private IEnumerator _Colorize(Color _color)
    {
        float t = 0;
        while (t < Timer)
        {
            spotlight.color = Color.Lerp(spotlight.color, _color, t);
            t+=Time.deltaTime;
            yield return null;   
        }
    }
}
