using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteFlash : MonoBehaviour
{
    [SerializeField] private Material flashMat;

    private SpriteRenderer enemySpr;
    private Material defaultMat;
    private void Awake()
    {
        enemySpr = GetComponent<SpriteRenderer>();
        defaultMat = enemySpr.material;
    }

    public IEnumerator FlashRoutine()
    {
        enemySpr.material = flashMat;
        yield return new WaitForSeconds(.1f);
        enemySpr.material = defaultMat;
    }
}
