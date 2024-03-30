using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFade : MonoBehaviour
{
    private float fadeTime = .4f;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public IEnumerator FadeRoutine()
    {
        float elapseTime = 0f;
        float startValue = spriteRenderer.color.a;

        while (elapseTime < fadeTime){
            elapseTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startValue, 0, elapseTime / fadeTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.b, spriteRenderer.color.g, alpha);
            yield return null;
        }
        Destroy(gameObject);
    }
}
