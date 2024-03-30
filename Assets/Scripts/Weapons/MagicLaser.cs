using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicLaser : MonoBehaviour
{
    private float lifsSpan = .2f;
    private float range;
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D capsuleCollider;
    private bool isGrowing = true;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>(); 
    }
    private void Start()
    {
        LaserFaceMouse();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Indestructible>() && !collision.isTrigger)
        {
            isGrowing = false;
        }
    }
    public void UpdateRange(float range)
    {
        this.range = range;
        StartCoroutine(InscreaseLaserLengthRoutine());
    }
    private IEnumerator InscreaseLaserLengthRoutine()
    {
        float timePassed = 0f;

        while (spriteRenderer.size.x < range && isGrowing) {
            timePassed += Time.deltaTime;
            float linearT = timePassed/lifsSpan;
            spriteRenderer.size = new Vector2(Mathf.Lerp(1, range, linearT), 1f);
            capsuleCollider.size = new Vector2(Mathf.Lerp(1, range, linearT), capsuleCollider.size.y);
            capsuleCollider.offset = new Vector2(Mathf.Lerp(1, range, linearT) / 2, capsuleCollider.size.y);
            yield return null;
        }
        StartCoroutine(GetComponent<SpriteFade>().FadeRoutine());
    }
    private void LaserFaceMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = transform.position - mousePos;
        transform.right = -dir;
    }
}
