using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private float initMoveSpeed = 5f;
    [SerializeField] private float dashSpeed = 3f;
    [SerializeField] private float dashTime = .3f;
    [SerializeField] private float dashCD = .5f;

    private PlayerInput input;
    private Rigidbody2D rigidBody;
    private Vector2 move;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private TrailRenderer trailRenderer;
    private float moveSpeed;
    private Transform pivotPoint;
    private KnockBack knockBack;
    public bool FacingLeft
    {
        get { return facingLeft; }
    }
    private bool facingLeft = false;
    private bool isDasing = false;
    protected override void Awake()
    {
        base.Awake();
        input = new PlayerInput();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        trailRenderer = transform.Find("Trail").GetComponent<TrailRenderer>();
        moveSpeed = initMoveSpeed;
        pivotPoint = transform.Find("PivotPoint");
        knockBack = GetComponent<KnockBack>();  
    }
    private void Start()
    {
        input.Combat.Dash.started += _ => Dash();
        InventoryController.Instance.EquipStartingWeapon();
    }
    private void OnEnable()
    {
        input.Enable();
    }
    private void Update()
    {
        Move();
    }
    private void FixedUpdate()
    {
        rigidBody.position += move * moveSpeed * Time.fixedDeltaTime;
    }
    private void Dash()
    {
        if(!isDasing && !knockBack.knockedBack)
        {
            isDasing = true;
            moveSpeed *= dashSpeed;
            trailRenderer.emitting = true;
            StartCoroutine(DashRoutine());
        }
    }
    private void Move()
    {
        move = input.Movement.Move.ReadValue<Vector2>();
        if (move != Vector2.zero)
        {
            if (knockBack.knockedBack) { return; }// cant move while in knockback cooldown time
            animator.SetBool("isWalking", true);// change state of animator controller
            PlayerFacingMove();//adjust the player facing direction
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
    private IEnumerator DashRoutine()
    {
        yield return new WaitForSeconds(dashTime);
        moveSpeed = initMoveSpeed;
        trailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDasing = false;
    }
    private void PlayerFacingMove()
    {
        if (move.x < 0)
        {
            //left
            spriteRenderer.flipX = true;
            facingLeft = true;
        }
        else
        {
            //right
            spriteRenderer.flipX = false;
            facingLeft = false;
        }
    }
}
