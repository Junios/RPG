using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterFSM : FSMBase
{
    public float walkSpeed = 1.5f;
    public float runSpeed = 3.0f;
    public float turnSpeed = 180.0f;
    public float attackRange = 1.5f;
    public float restTime = 1.5f;
    public float attack = 10.0f;
    public int maxHP = 10;
    public int currentHP = 10;
    public int gainExp = 10;
    public int gainGold = 10;
    public int level = 1;

    public Transform waypoint;
    [HideInInspector]
    public Transform[] waypoints;

    public PlayerFSM playerFSM;
    public Transform player;

    public Camera sight;

    public override void Awake()
    {
        base.Awake();

        agent.speed = walkSpeed;
        agent.angularSpeed = turnSpeed;
        agent.acceleration = 2000.0f;

        waypoints = waypoint.GetComponentsInChildren<Transform>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerFSM = player.GetComponent<PlayerFSM>();

        sight = GetComponentInChildren<Camera>();
    }

    public bool IsDetectPlayer()
    {
        Plane[] ps = GeometryUtility.CalculateFrustumPlanes(sight);

        return GeometryUtility.TestPlanesAABB(ps, playerFSM.myRenderer.bounds);
    }

    public override void OnEnable()
    {
        SetState(CharacterState.Idle);
        currentHP = maxHP;
        GetComponent<CharacterController>().enabled = true;
        agent.enabled = true;
        transform.position = waypoints[0].position;

        base.OnEnable();
    }

    public override IEnumerator Idle()
    {
        //Enter
        float totalTime = 0;
        agent.SetDestination(transform.position);

        while (state == CharacterState.Idle)
        {
            yield return null;
            //Stay

            totalTime += Time.deltaTime;

            if (totalTime > restTime)
            {
                SetState(CharacterState.Walk);
                break;
            }

            if (IsDetectPlayer() && !playerFSM.IsDead())
            {
                SetState(CharacterState.Run);
                break;
            }
        }
        //Exit
    }

    IEnumerator Run()
    {
        //Enter
        agent.stoppingDistance = attackRange;
        agent.speed = runSpeed;
        while (state == CharacterState.Run)
        {
            yield return null;
            //Stay

            agent.SetDestination(player.position);

            if (agent.remainingDistance <= attackRange)
            {
                SetState(CharacterState.Attack);
                break;
            }

            if (!IsDetectPlayer())
            {
                SetState(CharacterState.Idle);
                break;
            }

        }
        //Exit
    }

    public virtual IEnumerator Walk()
    {
        //Enter
        Transform target = waypoints[Random.Range(0, waypoints.Length)];
        agent.SetDestination(target.position);
        agent.speed = walkSpeed;
        agent.stoppingDistance = 0;

        while (state == CharacterState.Walk)
        {
            yield return null;
            //Stay

            if (agent.remainingDistance == 0)
            {
                SetState(CharacterState.Idle);
                break;
            }

            if (IsDetectPlayer() && !playerFSM.IsDead())
            {
                SetState(CharacterState.Run);
                break;
            }
        }
        //Exit
    }

    IEnumerator Attack()
    {
        //Enter

        while (state == CharacterState.Attack)
        {
            yield return null;
            //Stay
            MoveUtil.RotateBurst(transform, player);

            if (Vector3.Distance(transform.position, player.position) > attackRange)
            {
                SetState(CharacterState.Run);
                break;
            }

            if (playerFSM.IsDead())
            {
                SetState(CharacterState.Idle);
                break;
            }

        }
        //Exit
    }


    IEnumerator Dead()
    {
        playerFSM.GainGold(gainGold);
        playerFSM.GainExp(gainExp);

        GetComponent<CharacterController>().enabled = false;
        agent.enabled = false;

        yield return new WaitForSeconds(3.0f);

        gameObject.SetActive(false);

        Invoke("Respawn", 3.0f);
    }

    void Respawn()
    {
        gameObject.SetActive(true);
    }


    public void ProcessDamage(float damage)
    {
        currentHP -= (int)damage;

        if (currentHP <= 0)
        {
            SetState(CharacterState.Dead);
            currentHP = 0;
        }
        else
        {
            SetState(CharacterState.Run);
            MoveUtil.RotateBurst(transform, player);
        }
    }

    public void OnMonsterAttack()
    {
        playerFSM.ProcessDamage(attack);
    }
}
