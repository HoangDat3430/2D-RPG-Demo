using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
    [SerializeField] public float movementSpeed = 2f;

    private Rigidbody2D rigidBody;
    private Vector2 moveDir;
    private KnockBack knockBack;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        knockBack = GetComponent<KnockBack>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        if (knockBack.knockedBack) return;
        rigidBody.position += moveDir * movementSpeed * Time.fixedDeltaTime;
        if(moveDir.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
    public void Move(Vector2 Dir)
    {
        moveDir = Dir;
    }
    public void StopMove()
    {
        moveDir = Vector2.zero;
    }
}
