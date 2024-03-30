using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float roamingChangeDirTime = 2f;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private MonoBehaviour enemyType;
    [SerializeField] private float atkCD;
    [SerializeField] private bool stopMove = false;

    private bool canAtk = true;
    private enum State
    {
        Roaming,
        Attack,
    }
    private Vector2 roamPos;
    private float roamingTime = 0f;

    private EnemyPathFinding enemyPathFinding;
    private State currentState;

    private void Awake()
    {
        currentState = State.Roaming;
        enemyPathFinding = GetComponent<EnemyPathFinding>();
    }
    private void Start()
    {
        roamPos = GetRoamingPosition();
    }
    private void Update()
    {
        MoveStateControl();
    }
    private void MoveStateControl()
    {
        switch (currentState)
        {
            default:
            case State.Roaming:
                Roaming();
                break;
            case State.Attack:
                Attack();
                break;
        }
    }
    private void Roaming()
    {
        roamingTime += Time.deltaTime;
        enemyPathFinding.Move(roamPos);
        if(Vector2.Distance(gameObject.transform.position, PlayerController.Instance.transform.position) < attackRange)
        {
            currentState = State.Attack;
        }
        if(roamingTime > roamingChangeDirTime)
        {
            roamPos = GetRoamingPosition();
        }
    }
    private void Attack()
    {
        if(Vector2.Distance(transform.position, PlayerController.Instance.transform.position) > attackRange) { 
            currentState = State.Roaming;
        }
        if (attackRange != 0 && canAtk)
        {
            canAtk = false;
            if(enemyType) (enemyType as IEnemy).Attack();

            if (stopMove)
            {
                enemyPathFinding.StopMove();
            }
            else
            {
                enemyPathFinding.Move(roamPos);
            }
            StartCoroutine(AttackCooldownRoutine());
        }
    }
    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(atkCD);
        canAtk = true;
    }
    private Vector2 GetRoamingPosition()
    {
        roamingTime = 0f;
        return new Vector2(Random.Range(-1f,1f), Random.Range(-1f,1f)).normalized; 
    }
}
