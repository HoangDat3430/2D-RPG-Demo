using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : Singleton<UIFade>
{
    [SerializeField] private UnityEngine.UI.Image fadeImg;
    [SerializeField] private float fadeSpeed = 1f;

    private IEnumerator fadeRoutine;

    public void FadeToBlack()
    {
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
        }
        fadeRoutine = FadeRoutine(1);
        StartCoroutine(fadeRoutine);
    }
    public void FadeToWhite()
    {
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
        }
        fadeRoutine = FadeRoutine(0);
        StartCoroutine(fadeRoutine);
    }
    private IEnumerator FadeRoutine(float alpha)
    {
        while(!Mathf.Approximately(fadeImg.color.a, alpha))
        {
            float alp = Mathf.MoveTowards(fadeImg.color.a, alpha, fadeSpeed * Time.deltaTime);
            fadeImg.color = new Color(fadeImg.color.r, fadeImg.color.g, fadeImg.color.b, alp);
            yield return null;
        }
    }
}
