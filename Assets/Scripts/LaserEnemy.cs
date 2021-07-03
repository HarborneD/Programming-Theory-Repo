using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEnemy : Enemy
{
    [SerializeField] float attackDistancealongPath = 0.5f;
    [SerializeField] float speedAfterAttack = 150;

    EnemyMountedLazer lazer;
    [SerializeField] float aimAheadMagnitude = 9;

    float baseSpeed;
    bool hasAttacked = false;

    protected override void Start()
    {
        lazer = GetComponent<EnemyMountedLazer>();
        
        base.Start();

        baseSpeed = pathFollower.speed;

        pathFollower.endOfPath.AddListener(HandleFlightPathStart);

    }

    protected override void HandleMovement()
    {
        base.HandleMovement();

        switch (state)
        {
            case AiState.flyby:
                if (!hasAttacked && pathFollower.distanceTravelledRatio >= attackDistancealongPath)
                {
                    state = AiState.attacking;
                }
                break;


            case AiState.attacking:
                hasAttacked = true;
                Attack();
                state = AiState.flyby;
                pathFollower.speed = speedAfterAttack;
                break;
        }
    }

    protected void HandleFlightPathStart()
    {
        pathFollower.speed = baseSpeed;
        hasAttacked = false;
    }

    protected override void Attack()
    {
        Debug.Log("Laser Attack");

        lazer.MoveAim(player.transform.position + player.transform.forward.normalized * aimAheadMagnitude);
        lazer.FireGun();
    }
}
