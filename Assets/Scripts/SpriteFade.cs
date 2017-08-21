using UnityEngine;
using System.Collections;
using System;

public static class SpriteRendererExtension
{
    public static void FadeSprite(this SpriteRenderer renderer, MonoBehaviour mono, float duration, Action<SpriteRenderer> callback = null)
    {
        mono.StartCoroutine(SpriteCoroutine(renderer, duration, callback));
    }

    private static IEnumerator SpriteCoroutine(SpriteRenderer renderer, float duration, Action<SpriteRenderer> callback)
    {
        float start = Time.time;
        while(Time.time <= start + duration)
        {
            Color color = renderer.color;
            color.a = 1f - Mathf.Clamp01((Time.time - start) / duration);
            renderer.color = color;
            yield return new WaitForEndOfFrame();
        }

        if(callback != null)
        {
            callback(renderer);
        }

    }


}
