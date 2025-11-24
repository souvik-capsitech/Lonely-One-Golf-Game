using System.Collections;
using UnityEngine;

public class DayNightManager : MonoBehaviour
{
    public Camera mainCam;

    public Color dayColor = new Color(0.5f, 0.8f, 1f);
    public Color eveningColor = new Color(1f, 0.6f, 0.3f); 
    public Color nightColor = new Color(0.02f, 0.05f, 0.1f);

    public float transitionDuration = 1.5f;

    void Awake()
    {
        if (mainCam == null)
            mainCam = Camera.main;
    }

    
    public void SetTimeOfDay(string timeOfDay)
    {
        StopAllCoroutines();

        switch (timeOfDay.ToLower())
        {
            case "day":
                StartCoroutine(FadeToColor(dayColor));
                break;
            case "evening":
                StartCoroutine(FadeToColor(eveningColor));
                break;
            case "night":
                StartCoroutine(FadeToColor(nightColor));
                break;
        }
    }

    IEnumerator FadeToColor(Color targetColor)
    {
        float t = 0;
        Color start = mainCam.backgroundColor;

        while (t < 1)
        {
            t += Time.deltaTime / transitionDuration;
            mainCam.backgroundColor = Color.Lerp(start, targetColor, t);
            yield return null;
        }
    }
}
 