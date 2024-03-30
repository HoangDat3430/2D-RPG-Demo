using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [SerializeField] private float knockBackTime = .3f;

    private Rigidbody2D rb;
    public bool knockedBack {  get; private set; }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void GettingKnockBack(Transform dmgSource, float force)
    {
        knockedBack = true;
        Vector2 knockbackDir = (transform.position - dmgSource.position).normalized * force * rb.mass;
        rb.AddForce(knockbackDir, ForceMode2D.Impulse);
        StartCoroutine(KnockBackRoutine());
    }
    private IEnumerator KnockBackRoutine()
    {
        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
        knockedBack = false;
    }
}
