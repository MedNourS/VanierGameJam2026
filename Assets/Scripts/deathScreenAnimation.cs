using System.Collections;
using UnityEngine;

public class deathScreenAnimation : MonoBehaviour
{
    public CanvasGroup background;
    public float duration;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)) StartCoroutine(FadeCanvasGroup(background, 0, 1, duration));
    }

    public void startAnimation()
    {
        StartCoroutine(FadeCanvasGroup(background, 0, 1, duration));
    }


    IEnumerator FadeCanvasGroup(CanvasGroup group, float startAlpha, float endAlpha, float duration)
    {
        float startTime = Time.time;
        float endTime = startTime + duration;
        float currentAlpha = startAlpha;

        while (Time.time <= endTime)
        {
            float t = (Time.time - startTime) / duration;
            // Use an ease function for smoother transition (optional)
            currentAlpha = Mathf.Lerp(startAlpha, endAlpha, t); 
            group.alpha = currentAlpha;
            yield return null; // Wait for the next frame
        }

        group.alpha = endAlpha; // Ensure the final alpha is set correctly
    }
}
